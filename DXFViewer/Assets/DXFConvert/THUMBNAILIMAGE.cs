using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Loader;

namespace DXFConvert
{
    //http://docs.autodesk.com/ACD/2011/CHS/filesDXF/WSfacf1429558a55de185c428100849a0ab7-5d53.htm
    public class THUMBNAILIMAGE : SECTION
    {
        public THUMBNAILIMAGE() { }

        public THUMBNAILIMAGE(ILoader dxfData, Property prop)
            : base(dxfData, prop)
        {
        }

        protected override Property ReadSonClass(Property prop)
        {
            return base.ReadSonClass(prop);
        }

        protected override bool ReadProperty(Property prop)
        {
            return base.ReadProperty(prop);
        }
    }
}
