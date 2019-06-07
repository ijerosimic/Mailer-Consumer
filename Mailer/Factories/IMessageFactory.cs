using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AprossMailer.Factories
{
    public interface IMessageFactory
    {
        Message CreateMessage(Message messageContent);
    }
}
