namespace Pharmacy.Web.Extensions.EndPoints;

public static class EndPointsExtension
{
    public static void MapEndPoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapUserEndPoints();
        endpoints.MapProductEndPoints();
    }
}