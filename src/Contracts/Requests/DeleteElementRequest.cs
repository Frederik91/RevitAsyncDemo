using System;

namespace Contracts.Requests
{
    public class DeleteElementRequest
    {
        public Guid Id { get; }
        public string ElementId { get; set; }

        public DeleteElementRequest()
        {
            Id = Guid.NewGuid();
        }
    }
}
