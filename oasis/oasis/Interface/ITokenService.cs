using oasis.Entities;
using System.Threading.Tasks;

namespace oasis.Interface
{
    public interface ITokenService
    {
        Task<string> CreateToken(AppUser usser);
    }
}
