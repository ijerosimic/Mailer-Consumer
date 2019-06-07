using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Consumer.Data
{
    public interface IRepository
    {
        Task<int> FlagMessageAsSentAsync(int mailID);
    }
}
