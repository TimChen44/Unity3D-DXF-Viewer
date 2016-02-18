using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Loader;

namespace DXFConvert
{
    //http://docs.autodesk.com/ACD/2011/CHS/filesDXF/WSfacf1429558a55de185c428100849a0ab7-5e1a.htm
    public class TABLES : SECTION
    {
        public TABLES() { }

        public List<TABLE> TABLEList { get; set; }

        public TABLES(ILoader dxfData, Property prop)
            : base(dxfData, prop)
        {
            TABLEList = new List<TABLE>();
        }

        protected override Property ReadSonClass(Property prop)
        {
            switch (prop.Value)
            {
                case "TABLE":
                    TABLE entity = new TABLE(DXFData, prop);
                    TABLEList.Add(entity);
                    return entity.ReadProperties();
                case "ENDTAB":
                    return DXFData.Next();
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
