using Contracts.Requests;
using System.Collections.Generic;

namespace API.Models.Elements
{
    public class DeleteElementCommand
    {
        public IEnumerable<DeleteElementRequest> Requests { get; set; }
    }
}
