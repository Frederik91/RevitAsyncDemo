using API.Models.Elements;
using Contracts.Models;
using Contracts.Requests;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class ElementController
    {
        private readonly IElementService m_elementService;

        public ElementController(IElementService elementService)
        {
            m_elementService = elementService;
        }

        public async Task<CW_Element> Get(string documentTitle, string uniqueId)
        {
            var result = await m_elementService.Get(documentTitle, uniqueId);
            return result.FirstOrDefault();
        }

        public async Task<CW_Element> Get(string documentTitle, int id)
        {
            var result = await m_elementService.Get(documentTitle, id.ToString());
            return result.FirstOrDefault();
        }

        public async Task<IEnumerable<CW_Element>> Get(string documentTitle)
        {
            var result = await m_elementService.Get(documentTitle, string.Empty);
            return result;
        }

        public async Task Delete(string uniqueId)
        {
            var request = new DeleteElementRequest { ElementId = uniqueId };
            await m_elementService.Delete(new List<DeleteElementRequest> { request });
        }

        public async Task Delete(int id)
        {
            var request = new DeleteElementRequest { ElementId = id.ToString() };
            await m_elementService.Delete(new List<DeleteElementRequest> { request });
        }

        public async Task Delete(DeleteElementRequest request)
        {
            await m_elementService.Delete(new List<DeleteElementRequest> { request });
        }

        public async Task Delete(IEnumerable<DeleteElementRequest> requests)
        {
            await m_elementService.Delete(requests);
        }
    }
}

