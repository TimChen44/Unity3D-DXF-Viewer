using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace TimCommon.DXFConvert
{
    //http://docs.autodesk.com/ACD/2011/CHS/filesDXF/WSfacf1429558a55de185c428100849a0ab7-5f05.htm
    public class HEADER : SECTION
    {
        public HEADER() { }

        public HEADER(DXFImage dxfImage, Property prop)
            : base(dxfImage, prop)
        {

        }

        protected override Property ReadSonClass(Property prop)
        {
            if (prop.Value.Length > 0 && prop.Value[0] == '$')
            {
                H_Variables hv = new H_Variables(DXFImage, prop);
                base.Sons.Add(hv);
                var lastProp = hv.ReadProperties();
                return lastProp;
            }
            else
            {
                return null;
            }
        }

        protected override bool ReadProperty(Property prop)
        {
            return base.ReadProperty(prop);
        }



    }


    
    
}
