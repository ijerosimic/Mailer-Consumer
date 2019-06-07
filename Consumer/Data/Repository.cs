using Consumer.Factories;
using Consumer.Factories.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consumer.Data
{
    public class Repository : IRepository
    {
        public IContextFactory _contextFactory { get; }
        public Repository(IContextFactory contextFactory) => _contextFactory = contextFactory;

        public async Task<int> FlagMessageAsSentAsync(int mailID)
        {
            try
            {
                using (var context = _contextFactory.CreateContext())
                {
                    var sentMail = await context.Messages.Where(m => m.ID == mailID).FirstOrDefaultAsync();
                    sentMail.IsSent = true;
                    return await context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                throw new DbUpdateException($"Error updating database entry for message ID = {mailID}", e);
            }
        }
    }
}
