using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace TimCommon.DXFConvert
{
    //http://docs.autodesk.com/ACD/2011/CHS/filesDXF/WSfacf1429558a55de185c428100849a0ab7-5e1a.htm
    public class TABLES : SECTION
    {
        public TABLES() { }

        public List<TABLE> TABLEList { get; set; }

        public TABLES(DXFImage dxfImage, Property prop)
            : base(dxfImage, prop)
        {
            TABLEList = new List<TABLE>();
        }

        protected override Property ReadSonClass(Property prop)
        {
            switch (prop.Value)
            {
                case "TABLE":
                    TABLE entity = new TABLE(DXFImage, prop);
                    TABLEList.Add(entity);
                    return entity.ReadProperties();
                case "ENDTAB":
                    return DXFImage.Next();
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
