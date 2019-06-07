using AprossMailer.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AprossMailer.Repository
{
    public interface IApiRepository
    {
        Task<Message> SaveMessageToDatabase(Message messageContent, List<MessageAttachment> attachments);
        Task<bool> IsValidGUID(Guid guid);
    }
}
