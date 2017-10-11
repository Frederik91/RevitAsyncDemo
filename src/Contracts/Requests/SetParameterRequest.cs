
namespace Contracts.Requests
{
    public class SetParameterRequest
    {
        public string ElementId { get; set; }
        public string ParameterId { get; set; }
        public string Value { get; set; }
    }
}
