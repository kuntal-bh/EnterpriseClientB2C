using System.Threading.Tasks;

namespace EnterpriseClientB2C.Services
{
    public interface IReceipeClient
    {
        Task<string[]> GetValuesforNews();
        Task SetTokenforNews();
    }
}