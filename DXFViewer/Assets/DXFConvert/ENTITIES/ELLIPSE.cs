using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimCommon.DXFConvert
{
    public class ELLIPSE : ENTITIE
    {
        public ELLIPSE() { }

        //100	子类标记 (AcDbEllipse)
        public double C10 { get; set; }//10	中心点（在 WCS 中）	DXF：X 值；APP：三维点
        public double C20 { get; set; }//20	DXF：中心点的 Y 值和 Z 值（在 WCS 中）
        public double C30 { get; set; }//30	DXF：中心点的 Y 值和 Z 值（在 WCS 中）
        public double C11 { get; set; }//11	相对于中心的长轴端点（在 WCS 中）	DXF：X 值；APP：三维点
        public double C21 { get; set; }//21	DXF：相对于中心的长轴端点的 Y 值和 Z 值（在 WCS 中） 
        public double C31 { get; set; } //31
        //210	拉伸方向（可选；默认值 = 0, 0, 1）	DXF：X 值；APP：三维矢量
        //220	DXF：拉伸方向的 Y 值和 Z 值（可选）
        //230
        public double C40 { get; set; }//40	短轴与长轴的比例
        public double C41 { get; set; }//41	起点参数 （对于闭合椭圆，该值为 0.0）
        public double C42 { get; set; }//42	端点参数 （对于闭合椭圆，该值为 2pi）


        public ELLIPSE(DXFImage dxfImage, Property prop)
            : base(dxfImage, prop)
        {

        }

        protected override bool ReadProperty(Property prop)
        {
            switch (prop.Code)
            {
                case 10: C10 = prop.Value.ToDouble(); break;
                case 20: C20 = prop.Value.ToDouble(); break;
                case 30: C30 = prop.Value.ToDouble(); break;

                case 11: C11 = prop.Value.ToDouble(); break;
                case 21: C21 = prop.Value.ToDouble(); break;
                case 31: C31 = prop.Value.ToDouble(); break;

                case 40: C40 = prop.Value.ToDouble(); break;
                case 41: C41 = prop.Value.ToDouble(); break;
                case 42: C42 = prop.Value.ToDouble(); break;
                default:
                    return base.ReadProperty(prop);
            }
            return false;
        }
    }
}
