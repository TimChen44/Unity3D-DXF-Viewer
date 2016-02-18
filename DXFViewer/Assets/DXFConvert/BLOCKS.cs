using Loader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace DXFConvert
{
    //http://docs.autodesk.com/ACD/2011/CHS/filesDXF/WSfacf1429558a55de185c428100849a0ab7-5e01.htm
    public class BLOCKS : SECTION
    {
        public BLOCKS() { }

        public BLOCKS(ILoader dxfData, Property prop)
            : base(dxfData, prop)
        {
            BLOCKList = new List<BLOCK>();
            ENDBLKList = new List<ENDBLK>();
        }

        public List<BLOCK> BLOCKList { get; set; }
        public List<ENDBLK> ENDBLKList { get; set; }

        protected override Property ReadSonClass(Property prop)
        {
            switch (prop.Value)
            {
                case "BLOCK":
                    var block = new BLOCK(DXFData, prop);
                    BLOCKList.Add(block);
                    return block.ReadProperties();
                case "ENDBLK":
                    var endblk = new ENDBLK(DXFData, prop);
                    ENDBLKList.Add(endblk);
                    return endblk.ReadProperties();
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
