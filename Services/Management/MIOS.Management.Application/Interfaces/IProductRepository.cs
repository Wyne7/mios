using Common.Models;
using MIOS.Management.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIOS.Management.Application.Interfaces
{
    public interface IProductRepository
    {
        Task<CodeMessage> CreatePoduct(Product product);
    }
}
