using Demo.BL.Interface;

using Demo.DAL.Database;
using Demo.DAL.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BL.Repository
{
    public class DepartmentRep : IDepartmentRep
    {
        private readonly DemoContext db;

        public DepartmentRep(DemoContext db)
        {
            this.db = db;
        }
        public async Task Create(Department obj)
        {
            
            await db.Department.AddAsync(obj);
            await db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var obj = await db.Department.FindAsync(id);
            db.Department.Remove(obj);
            await db.SaveChangesAsync();
        }

        public async Task<IEnumerable<Department>> Get()
        {
            var data = await db.Department.ToListAsync();
            return data;
        }
        public async Task<Department> GetById(int id)
        {


            var data = await db.Department.Where(x => x.Id == id).FirstOrDefaultAsync();
            return data;
        }
        public async Task Update(Department obj)
        {
            db.Entry(obj).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }
    }
}
