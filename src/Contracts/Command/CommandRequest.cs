using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Command
{
    public class CommandRequest
    {
        public Guid Id { get; }
        public dynamic Command { get; set; }
        public dynamic Handler { get; set; }

        public CommandRequest()
        {
            Id = Guid.NewGuid();
        }
    }
}
