
using Demo.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BL.Interface
{
    public interface IDepartmentRep
    {
        Task<IEnumerable<Department>>  Get();
        Task<Department> GetById(int id);
        Task Create(Department obj);
        Task Update(Department obj);
        Task Delete(int id);

    }
}
