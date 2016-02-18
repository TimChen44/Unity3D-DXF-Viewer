using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Loader;

namespace DXFConvert
{
    //http://docs.autodesk.com/ACD/2011/CHS/filesDXF/WS1a9193826455f5ff18cb41610ec0a2e719-7a3f.htm
    public class ENDBLK : Entity
    {
        public ENDBLK() { }

        public ENDBLK(ILoader dxfData, Property prop)
            : base(dxfData, prop)
        {

        }


        protected override bool ReadProperty(Property prop)
        {
            if (prop.Code == 0 && prop.Value == "BLOCK")
            {
                return true;
            }
            else if (prop.Code == 0 && prop.Value == "ENDSEC")
            {
                return true;
            }
            else
            {
                SaveProperty(prop);
                return false;
            }
        }
    }
}

