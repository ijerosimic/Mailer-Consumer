using AprossMailer.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace AprossMailer.Helpers
{
    public class AttachmentCreator : IAttachmentCreator
    {
        public List<MessageAttachment> _attachments { get; }
        public AttachmentCreator()
        {
            _attachments = new List<MessageAttachment>();
        }
        public async Task<List<MessageAttachment>> CreateAttachments(List<IFormFile> files)
        {
            if (files == null && files.Count < 1)
                return _attachments;

            foreach (var file in files)
            {
                using (var ms = new MemoryStream())
                {
                    await file.CopyToAsync(ms);

                    _attachments.Add(new MessageAttachment()
                    {
                        Name = file.FileName,
                        Type = file.ContentType,
                        Body = ms.ToArray()
                    });
                }
            }

            return _attachments;
        }
    }
}
