using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.Dto
{
    public class ProductDto : Products
    {
        public Products Product { get; set; }
    }
}