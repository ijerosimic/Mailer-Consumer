using System;
using System.Collections.Generic;
using System.Text;

namespace Consumer.Validation
{
    public interface IEmailAddressValidator
    {
        List<string> ValidateAndProcessEmailAddress(string str);
        bool IsValidEmail(string address);
    }
}
