using Challenge.Service.Dtos;

namespace Challenge.Service.Interfaces
{
    public interface IProductionPlanService
    {
        List<Response> Calculate(Payload payload);
    }
}
