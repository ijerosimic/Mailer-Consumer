using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AprossMailer.Validation
{
    public class ContentValidator : IContentValidator
    {
        public async Task<bool> IsValidContent(Message message) =>
            message != null && string.IsNullOrWhiteSpace(message.MailTo) == false && message.MailTo != "null";
    }
}
