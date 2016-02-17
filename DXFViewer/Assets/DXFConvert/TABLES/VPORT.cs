using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace TimCommon.DXFConvert
{
    //http://docs.autodesk.com/ACD/2011/CHS/filesDXF/WS1a9193826455f5ff18cb41610ec0a2e719-7a46.htm
    public class VPORT : TABLESonBase
    {
        public VPORT() { }

        public VPORT(DXFImage dxfImage, Property prop)
            : base(dxfImage, prop)
        {
        }
    }
}