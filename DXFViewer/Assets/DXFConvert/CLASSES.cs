using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Loader;

namespace DXFConvert
{
    //http://docs.autodesk.com/ACD/2011/CHS/filesDXF/WSfacf1429558a55de185c428100849a0ab7-5e21.htm
    public class CLASSES : SECTION
    {
        public CLASSES() { }

        public CLASSES(ILoader dxfData, Property prop)
            : base(dxfData, prop)
        {
        }

        protected override Property ReadSonClass(Property prop)
        {
            switch (prop.Value)
            {
                case "CLASS":
                    CLASS entity = new CLASS(DXFData, prop);
                    Sons.Add(entity);
                    var lastProp = entity.ReadProperties();
                    return lastProp;
                default:
                    return base.ReadSonClass(prop);
            }
        }

        protected override bool ReadProperty(Property prop)
        {
            return base.ReadProperty(prop);
        }
    }


}
