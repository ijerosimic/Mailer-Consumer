using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AprossMailer.Validation
{
    public interface IContentValidator
    {
        Task<bool> IsValidContent(Message message);
    }
}
