using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebSaleMvc.Data;
using WebSaleMvc.Models;
using Microsoft.EntityFrameworkCore;

namespace WebSaleMvc.Services
{
    public class DepartmentService
    {
        private readonly WebSaleMvcContext _contex;

        public DepartmentService(WebSaleMvcContext context)
        {
            _contex = context;
        }

        public async Task<List<Department>> FindAllAsync()
        {
            return await _contex.Department.OrderBy(x => x.Name).ToListAsync();
        }
    }
}
