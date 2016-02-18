using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Loader;

namespace DXFConvert
{
    //http://docs.autodesk.com/ACD/2011/CHS/filesDXF/WS1a9193826455f5ff18cb41610ec0a2e719-7a4b.htm
    public class UCS : TABLESonBase
    {
        public UCS() { }

        public UCS(ILoader dxfData, Property prop)
            : base(dxfData, prop)
        {
        }
    }
}