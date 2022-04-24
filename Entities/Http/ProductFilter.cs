using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.Http
{
    public class ProductFilter : BaseFilter
    {
        public string? ID{get; set;}

        protected override void SetParameters()
        {
            base.SetParameters();
            _parameters["ID"] = ID;
        }
    }
}