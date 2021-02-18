using FunctionUnitTesting.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FunctionUnitTesting.Services
{
    public interface IContext
    {
        Task<Record> GetRecordById(string id);
    }
}