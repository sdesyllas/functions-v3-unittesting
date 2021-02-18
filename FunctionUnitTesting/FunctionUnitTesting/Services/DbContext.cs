using FunctionUnitTesting.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FunctionUnitTesting.Services
{

    public class DbContext : IDbContext
    {
        private Dictionary<string, Record> _records;

        public DbContext()
        {
            _records = new Dictionary<string, Record>();
            _records.Add("1", new Record { Id = "1", Name = $"Alpha" });
            _records.Add("2", new Record { Id = "2", Name = $"Beta" });
            _records.Add("3", new Record { Id = "3", Name = $"Gama" });
            _records.Add("4", new Record { Id = "4", Name = $"Delpta" });
            _records.Add("5", new Record { Id = "5", Name = $"Epsilon" });
        }


        public async Task<Record> GetRecordById(string id)
        {
            if (_records.ContainsKey(id))
                return _records[id];
            return null;
        }
    }
}
