using LevendMonopoly.Api.Models;

namespace LevendMonopoly.Api.Interfaces.Services
{
    public interface IPDFService
    {
        byte[] ExportBuildingsToPdf(List<Building> buildings);
    }
}
