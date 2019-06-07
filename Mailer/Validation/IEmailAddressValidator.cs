using System.Collections.Generic;
using System.Threading.Tasks;

namespace AprossMailer.Validation
{
    public interface IEmailAddressValidator
    {
        Task<string> ValidateInput(string str);
    }
}