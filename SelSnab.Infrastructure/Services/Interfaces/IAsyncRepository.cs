using SelSnab.Domain.Commons.Request;
using SelSnab.Domain.Commons.Response;
using SelSnab.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelSnab.Infrastructure.Services.Interfaces;

public interface IAsyncRepository<TDTO, DModel> where TDTO : class where DModel : class
{
    public Task<TDTO> GetAsync(int id);
    public Task<int> CreateAsync(DModel model);
    public Task<int> UpdateAsync(DModel model);
    public Task<int> DeleteAsync(int id);
}
