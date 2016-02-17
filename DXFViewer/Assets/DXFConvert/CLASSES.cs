using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace TimCommon.DXFConvert
{
    //http://docs.autodesk.com/ACD/2011/CHS/filesDXF/WSfacf1429558a55de185c428100849a0ab7-5e21.htm
    public class CLASSES : SECTION
    {
        public CLASSES() { }

        public CLASSES(DXFImage dxfImage, Property prop)
            : base(dxfImage, prop)
        {
        }

        protected override Property ReadSonClass(Property prop)
        {
            switch (prop.Value)
            {
                case "CLASS":
                    CLASS entity = new CLASS(DXFImage, prop);
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
