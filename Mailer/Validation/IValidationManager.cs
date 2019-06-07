using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AprossMailer.Validation
{
    public interface IValidationManager
    {
        Task<bool> IsValidGUID(Guid guid);
        Task<bool> IsValidContent(Message message);
        Task<string> ValidateRecipientAddresses(string input);
    }
}
