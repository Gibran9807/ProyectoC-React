using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.Http
{
    public class BaseFilter
    {
        protected Dictionary<string,string> _parameters;

        public Dictionary<string,string> Parameters
        {
            get
            {
                SetParameters();
                return _parameters;
            }
        }

        public BaseFilter()
        {
            _parameters = new Dictionary<string, string>();
        }

        public  BaseFilter(int pagNumber, int pageSize)
        {

        }

        protected virtual void SetParameters()
        {

        }
    }
}