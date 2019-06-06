using System.Threading.Tasks;

namespace EnterpriseClientB2C.Services
{
    public interface INewsAPIClient
    {
        Task<string[]> GetValuesforNews();
        Task SetTokenforNews();
    }
}