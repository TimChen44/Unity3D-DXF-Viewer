using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Loader
{
   public  interface ILoader
    {
         Property Next();
    }

    public class Property
    {
        public int Code { get; set; }//组码
        public string Value { get; set; }//值
    }
}
