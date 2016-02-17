using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;


namespace TimCommon.DXFConvert
{
    public class TABLE : Entity
    {
        public TABLE() { }

        public List<LAYER> LAYERList { get; set; }


        public TABLE(DXFImage dxfImage, Property prop)
            : base(dxfImage, prop)
        {
            LAYERList = new List<LAYER>();
        }

        protected override Property ReadSonClass(Property prop)
        {
            switch (prop.Value)
            {
                case "APPID":
                    return CreateSonClass(new VPORT(DXFImage, prop));
                case "BLOCK_RECORD":
                    return CreateSonClass(new BLOCK_RECORD(DXFImage, prop));
                case "DIMSTYLE":
                    return CreateSonClass(new DIMSTYLE(DXFImage, prop));
                case "LAYER":
                    var layer = new LAYER(DXFImage, prop);
                    LAYERList.Add(layer);
                    return layer.ReadProperties();
                case "LTYPE":
                    return CreateSonClass(new LTYPE(DXFImage, prop));
                case "STYLE":
                    return CreateSonClass(new STYLE(DXFImage, prop));
                case "UCS":
                    return CreateSonClass(new UCS(DXFImage, prop));
                case "VIEW":
                    return CreateSonClass(new VIEW(DXFImage, prop));
                case "VPORT":
                    return CreateSonClass(new VPORT(DXFImage, prop));
                default:
                    return base.ReadSonClass(prop);
            }
        }

        protected override bool ReadProperty(Property prop)
        {
            if (prop.Code == 0 && prop.Value == "ENDTAB")
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

    /// <summary>
    /// 表子类基类
    /// </summary>
    public class TABLESonBase : Entity
    {
        public TABLESonBase() { }

        public TABLESonBase(DXFImage dxfImage, Property prop)
            : base(dxfImage, prop)
        {
        }

        protected override Property ReadSonClass(Property prop)
        {
            //是属性，那么特别待遇了
            if (prop.Value.Length > 0 && prop.Value[0] == '{')
            {
                ACAD_XDICTIONARY hv = new ACAD_XDICTIONARY(DXFImage, prop);
                Sons.Add(hv);
                var lastProp = hv.ReadProperties();
                return lastProp;
            }
            else if (prop.Code == 102 && prop.Value == "}")
            {
                return DXFImage.Next();
            }
            else
            {
                return base.ReadSonClass(prop);
            }
        }

        protected override bool ReadProperty(Property prop)
        {
            return base.ReadProperty(prop);
        }

    }


    /// <summary>
    /// 
    /// </summary>
    public class ACAD_XDICTIONARY : Entity
    {
        public ACAD_XDICTIONARY(DXFImage dxfImage, Property prop)
            : base(dxfImage, prop) { }

        protected override bool ReadProperty(Property prop)
        {
            switch (prop.Code)
            {
                case 102:
                    return true;
                default:
                    return base.ReadProperty(prop);
            }
        }
    }
}