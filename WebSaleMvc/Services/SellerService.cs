using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebSaleMvc.Data;
using WebSaleMvc.Models;
using Microsoft.EntityFrameworkCore;
using WebSaleMvc.Services.Exceptions;

namespace WebSaleMvc.Services
{
    public class SellerService
    {
        private readonly WebSaleMvcContext  _contex;

        public SellerService(WebSaleMvcContext context)
        {
            _contex = context;
        }

        public async Task<List<Seller>> FindAllAsync() 
        {
            return await _contex.Sellers.ToListAsync();
        }

        public async Task InsertAsync(Seller obj)
        {
            _contex.Add(obj);
            _contex.SaveChangesAsync();
        }

        public async Task<Seller> FindByIdAsync(int id)
        {
            return await _contex.Sellers.Include(obj => obj.Department).FirstOrDefaultAsync(obj => obj.Id == id);
        }

        public async Task RemoveAsync(int id)
        {
            try
            {
                var obj = await _contex.Sellers.FindAsync(id);
                _contex.Sellers.Remove(obj);
                await _contex.SaveChangesAsync();
            }
            catch(DbConcurrencyException e)
            {
                throw new IntegrityException(e.Message);
            }

        }

        public async Task UpdateAsync(Seller obj)
        {
            bool hasAny = await _contex.Sellers.AnyAsync(x => x.Id == obj.Id);
            if (!hasAny)
            {
                throw new NotFoundException("Id not found");
            }
            try
            {
                _contex.Update(obj);
                await _contex.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }


    }
}
