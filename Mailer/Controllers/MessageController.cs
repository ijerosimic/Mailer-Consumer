using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AprossMailer.Helpers;
using AprossMailer.Models;
using AprossMailer.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Web;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;
using AprossMailer.Validation;

namespace AprossMailer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        IApiService _service { get; }
        IAttachmentCreator _attachmentCreator { get; }
        public IValidationManager _validationManager { get; }
        public MessageController(IApiService service, IAttachmentCreator attachmentCreator, IValidationManager validationManager)
        {
            _service = service;
            _attachmentCreator = attachmentCreator;
            _validationManager = validationManager;
        }

        [HttpGet]
        [EnableCors("AllowAllOrigins")]
        public void Get() { }

        [HttpPost]
        [EnableCors("AllowAllOrigins")]
        public async Task<IActionResult> Post([ModelBinder(BinderType = typeof(JsonModelBinder))] Message message, List<IFormFile> files)
        {
            if (await _validationManager.IsValidGUID(message.GUID) == false)
                return new StatusCodeResult(403);

            if (await _validationManager.IsValidContent(message) == false)
                return new StatusCodeResult(406);

            var recipients = await _validationManager.ValidateRecipientAddresses(message.MailTo);
            if (string.IsNullOrEmpty(recipients))
                return new StatusCodeResult(406);

            message.MailTo = recipients;

            var attachments = await _attachmentCreator.CreateAttachments(files);

            try
            {
                await _service.PublishMessage(message, attachments);
            }
            catch (Exception)
            {
                return new StatusCodeResult(503);
            }

            return new StatusCodeResult(200);
        }
    }
}