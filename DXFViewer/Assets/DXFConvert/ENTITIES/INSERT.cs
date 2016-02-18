using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Loader;
namespace DXFConvert
{
    //http://docs.autodesk.com/ACD/2011/CHS/filesDXF/WS1a9193826455f5ff18cb41610ec0a2e719-7a04.htm
    public class INSERT : ENTITIE
    {
        public INSERT() { }

        //100	子类标记 (AcDbBlockReference)
        //66	可变属性跟随标志（可选；默认值 = 0）；如果属性跟随标志的值为 1，则跟随插入的将是一系列属性图元，并以一个 seqend 图元终止
        public string C2 { get; set; } //2     块名
        public double C10 { get; set; } //10	插入点（在 OCS 中）	DXF：X 值；APP：三维点
        public double C20 { get; set; } //20	DXF：插入点的 Y 值和 Z 值（在 OCS 中）
        public double C30 { get; set; } //30
        public double C41{ get; set; } //41	X 缩放比例（可选；默认值 = 1）
        public double C42 { get; set; } //42	Y 缩放比例（可选；默认值 = 1）
        public double C43 { get; set; } //43	Z 缩放比例（可选；默认值 = 1）
        public double C50 { get; set; } //50	旋转角度（可选；默认值 = 0）
        public double C70{ get; set; } //70	列计数（可选；默认值 = 1）
        public double C71 { get; set; } //71	行计数（可选；默认值 = 1）
        public double C44 { get; set; } //44	列间距（可选；默认值 = 0）
        public double C45 { get; set; } //45	行间距（可选；默认值 = 0）
        //210	拉伸方向（可选；默认值 = 0, 0, 1）	DXF：X 值；APP：三维矢量
        //220	DXF：拉伸方向的 Y 值和 Z 值（可选）
        //230


        public INSERT(ILoader dxfData, Property prop)
            : base(dxfData, prop)
        {

        }

        protected override bool ReadProperty(Property prop)
        {
            switch (prop.Code)
            {
                case 2: C2 = prop.Value; break;

                case 10: C10 = prop.Value.ToDouble(); break;
                case 20: C20 = prop.Value.ToDouble(); break;
                case 30: C30 = prop.Value.ToDouble(); break;

                case 41: C41 = prop.Value.ToDouble(); break;
                case 42: C42 = prop.Value.ToDouble(); break;
                case 43: C43 = prop.Value.ToDouble(); break;

                case 50: C50 = prop.Value.ToDouble(); break;

                case 70: C70 = prop.Value.ToDouble(); break;
                case 71: C71 = prop.Value.ToDouble(); break;

                case 44: C44 = prop.Value.ToDouble(); break;
                case 45: C45 = prop.Value.ToDouble(); break;
                default:
                    return base.ReadProperty(prop);
            }
            return false;
        }
    }
}
