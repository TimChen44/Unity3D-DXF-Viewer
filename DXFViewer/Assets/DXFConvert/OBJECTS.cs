using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Loader;

namespace DXFConvert
{
    //http://docs.autodesk.com/ACD/2011/CHS/filesDXF/WSfacf1429558a55de185c428100849a0ab7-5d8d.htm
   public  class OBJECTS: SECTION
    {
       public OBJECTS() { }

       public OBJECTS(ILoader dxfData, Property prop)
            : base(dxfData, prop)
        {
        }

       protected override Property ReadSonClass(Property prop)
       {
           if (prop.Code == 0 && prop.Value != "ENDSEC")
           {
               switch (prop.Value)
               {
                   default:
                       return CreateSonClass(new OBJECT(DXFData, prop));
               }
           }
           else
           {
               return base.ReadSonClass(prop);
           }
       }

        protected override bool ReadProperty(Property prop)
        {
            return base.ReadProperty(prop);
        }
    }
}
