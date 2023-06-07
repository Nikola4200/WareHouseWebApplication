using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace WarehouseWeb.Model.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        int commit();
    }
}
