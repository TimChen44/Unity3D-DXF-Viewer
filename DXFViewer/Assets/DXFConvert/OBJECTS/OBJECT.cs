using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Loader;

namespace DXFConvert
{
    //http://docs.autodesk.com/ACD/2011/CHS/filesDXF/WS1a9193826455f5ff18cb41610ec0a2e719-79be.htm
   public  class OBJECT: Entity
    {
       public OBJECT() { }

       public OBJECT(ILoader dxfData, Property prop)
            : base(dxfData, prop)
        {

        }

        protected override bool ReadProperty(Property prop)
        {
            return base.ReadProperty(prop);
        }
    }
}
