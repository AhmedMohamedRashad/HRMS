using Demo.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BL.Interface
{
    public interface IEmployeeRep
    {
        Task<IEnumerable<Employee>> Get(Expression<Func<Employee,bool>> filter = null); 
        Task<Employee> GetById(Expression<Func<Employee, bool>> filter);
        Task<Employee> Create(Employee obj);
        Task Update(Employee obj);
        Task Delete(int id);

    }
}
