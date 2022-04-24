using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Api.DataAccess.Interfaces
{
      public interface IData
    {
        IDbConnection DbConnection{get;}
    }
}