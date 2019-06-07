using AprossMailer.Data;
using AprossMailer.Factories;
using AprossMailer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace AprossMailer.Repository
{
    public class ApiRepository : IApiRepository
    {
        AppDbContext _context { get; }
        IMessageFactory _messageFactory { get; }
        public ApiRepository(AppDbContext context, IMessageFactory messageFactory)
        {
            _context = context;
            _messageFactory = messageFactory;
        }

        public async Task<Message> SaveMessageToDatabase(Message messageContent, List<MessageAttachment> attachments)
        {
            var message = _messageFactory.CreateMessage(messageContent);

            using (_context)
            {
                await _context.AddAsync(message);

                try
                {
                    await _context.SaveChangesAsync();
                    await SaveAttachmentToDatabase(message.ID, attachments);
                }
                catch (DbUpdateException e)
                {
                    throw new DbUpdateException("Error saving message to database!", e);
                }

                return message;
            }
        }

        public async Task SaveAttachmentToDatabase(int messageID, List<MessageAttachment> attachments)
        {
            foreach (var attachment in attachments)
            {
                attachment.MessageID = messageID;
                await _context.AddAsync(attachment);
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new DbUpdateException("Error saving message to database!", e);
            }
        }

        public async Task<bool> IsValidGUID(Guid guid)
        {
            try
            {
                return await _context.Identifiers.Where(i => i.GUID == guid).AnyAsync();
            }
            catch (DbUpdateException)
            {
                return false;
            }
        }
    }
}
