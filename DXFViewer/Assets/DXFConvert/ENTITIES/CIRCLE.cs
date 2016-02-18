using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Loader;

namespace DXFConvert
{
    //http://docs.autodesk.com/ACD/2011/CHS/filesDXF/WS1a9193826455f5ff18cb41610ec0a2e719-7a2d.htm
    public class CIRCLE : ENTITIE
    {
        public CIRCLE() { }

        //100	子类标记 (AcDbCircle)
        //39	厚度（可选；默认值 = 0）
        public double C10 { get; set; }//10	中心点（在 OCS 中）	DXF：X 值；APP：三维点
        public double C20 { get; set; }//20    DXF：中心点的 Y 值和 Z 值（在 OCS 中）
        public double C30 { get; set; }//30	
        public double C40 { get; set; }//40	半径
        //210	拉伸方向（可选；默认值 = 0, 0, 1）	DXF：X 值；APP：三维矢量
        //220	DXF：拉伸方向的 Y 值和 Z 值（可选）
        //230

        public CIRCLE(ILoader dxfData, Property prop)
            : base(dxfData, prop)
        {

        }

        protected override bool ReadProperty(Property prop)
        {
            switch (prop.Code)
            {
                case 10: C10 = prop.Value.ToDouble(); break;
                case 20: C20 = prop.Value.ToDouble(); break;
                case 30: C30 = prop.Value.ToDouble(); break;
                case 40: C40 = prop.Value.ToDouble(); break;
                default:
                    return base.ReadProperty(prop);
            }
            return false;
        }
    }
}
