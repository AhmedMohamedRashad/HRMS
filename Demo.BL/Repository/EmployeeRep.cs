using Demo.BL.Interface;
using Demo.DAL.Database;
using Demo.DAL.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BL.Repository
{
    public class EmployeeRep : IEmployeeRep
    {
        private readonly DemoContext db;

        public EmployeeRep(DemoContext db)
        {
            this.db = db;
        }
        public async Task<Employee> Create(Employee obj)
        {

            await db.Employee.AddAsync(obj);
            await db.SaveChangesAsync();

            return db.Employee.OrderByDescending(x => x.Id).FirstOrDefault();
        }

        public async Task Delete(int id)
        {
            var obj = await db.Employee.FindAsync(id);
            db.Employee.Remove(obj);
            await db.SaveChangesAsync();
        }

        public async Task<IEnumerable<Employee>> Get(Expression<Func<Employee,bool>> filter = null)
        {
            if (filter == null)
                return await db.Employee.Include("Department").Include("District").ToListAsync();
            else
                return await db.Employee.Include("Department").Include("District").Where(filter).ToListAsync();
        }
        public async Task<Employee> GetById(Expression<Func<Employee, bool>> filter )
        {
            var data = await db.Employee.Where(filter).Include("Department").FirstOrDefaultAsync();
            return data;
        }
        public async Task Update(Employee obj)
        {
                db.Entry(obj).State = EntityState.Modified;
                await db.SaveChangesAsync();
        }


    }
}
