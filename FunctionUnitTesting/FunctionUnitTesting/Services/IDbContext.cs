using FunctionUnitTesting.Domain;
using System.Threading.Tasks;

namespace FunctionUnitTesting.Services
{
    public interface IDbContext
    {
        Task<Record> GetRecordById(string id);
    }
}