using AprossMailer.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AprossMailer.Helpers
{
    public interface IAttachmentCreator
    {
        Task<List<MessageAttachment>> CreateAttachments(List<IFormFile> files);
    }
}
