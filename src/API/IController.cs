using API.Controllers;
using Contracts.Events;

namespace API
{
    public interface IController
    {
        CategoryController CategoryController { get; }
        DocumentController DocumentController { get; }
        ElementController ElementController { get; }
        FilterController FilterController { get; }
        ParameterController ParameterController { get; }
        DocumentChangedEvent DocumentChangedEvent { get; }
    }
}
