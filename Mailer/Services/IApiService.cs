using AprossMailer.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace AprossMailer.Services
{
    public interface IApiService
    {
        Task PublishMessage(Message messageContent, List<MessageAttachment> filesList);
    }
}
