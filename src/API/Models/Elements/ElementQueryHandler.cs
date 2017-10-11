using Contracts.Models;
using Contracts.Query;
using Contracts.RevitInteractors;
using System.Collections.Generic;

namespace API.Models.Elements
{
    public class ElementQueryHandler : IQueryHandler<ElementQuery, IEnumerable<CW_Element>>
    {
        private readonly IElementRevitInteractor m_elementRevitInteractor;

        public ElementQueryHandler(IElementRevitInteractor elementRevitInteractor)
        {
            m_elementRevitInteractor = elementRevitInteractor;
        }

        public IEnumerable<CW_Element> Handle(ElementQuery query)
        {
            var result = m_elementRevitInteractor.Get(query.DocumentTitle, query.UniqueId);
            return result;
        }
    }
}
