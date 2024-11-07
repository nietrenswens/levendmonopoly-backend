using LevendMonopoly.Api.Models;

namespace LevendMonopoly.Api.Interfaces.Services
{
    public interface IRoleService
    {
        Task<IEnumerable<Role>> GetRolesAsync();
    }
}
