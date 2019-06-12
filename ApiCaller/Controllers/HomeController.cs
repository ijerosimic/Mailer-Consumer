using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using ApiCaller.Models;
using Microsoft.AspNetCore.Authorization;
using System.Net.Mail;

namespace ApiCaller.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View(new MessageAttachment());
        }

        //[HttpPost]
        //public async Task Index(Attachments model)
        //{
        //    var attachment = model.Attachment;
        //    var fileName = Path.GetFileName(model.Attachment.FileName);
        //    var contentType = model.Attachment.ContentType;
        //}
    }
}
