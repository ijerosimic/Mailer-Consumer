using System;
using System.Collections.Generic;
using System.Text;

namespace Consumer.Validation
{
    public interface IInputValidator
    {
        Message ProcessMessage(string messageContent);
    }
}
