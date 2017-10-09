using Contracts.Models;
using Contracts.Query;

namespace API.Models.Documents
{
    public class DocumentQuery : IQuery<CW_Document>
    {
        public string Title { get; set; }
    }
}
