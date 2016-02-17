using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace TimCommon.DXFConvert
{
    //http://docs.autodesk.com/ACD/2011/CHS/filesDXF/WS1a9193826455f5ff18cb41610ec0a2e719-7a55.htm
   public  class BLOCK_RECORD: TABLESonBase
    {
       public BLOCK_RECORD() { }

       public BLOCK_RECORD(DXFImage dxfImage, Property prop)
            : base(dxfImage, prop)
        {
        }
    }
}