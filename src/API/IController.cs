using API.Controllers;

namespace API
{
    public interface IController
    {
        CategoryController CategoryController { get; }
        DocumentController DocumentController { get; }
        ElementController ElementController { get; }
        FilterController FilterController { get; }
        ParameterController ParameterController { get; }
    }
}
