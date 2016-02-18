using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Loader;

namespace DXFConvert
{
    //http://docs.autodesk.com/ACD/2011/CHS/filesDXF/WS1a9193826455f5ff18cb41610ec0a2e719-79fc.htm
    public class LWPOLYLINE : ENTITIE
    {
        public LWPOLYLINE() { }

        public string C100 { get; set; }//子类标记 (AcDbPolyline)
        public int C90 { get; set; }//顶点数
        public int C70 { get; set; }//多段线标志（按位编码）；默认值为 0： 1 = 关闭；128 = Plinegen
        public string C43 { get; set; }//固定宽度（可选；默认值 = 0）。如果设定为可变宽度（代码 40 和/或 41），则不使用
        public double  C38 { get; set; }//标高（可选；默认值 = 0）
        public double C39 { get; set; }//厚度（可选；默认值 = 0）
        //public string C10 { get; set; }//顶点坐标（在 OCS 中），多个条目；每个顶点一个条目  DXF：X 值；APP：二维点
        //public string C20{ get; set; }//DXF：顶点坐标的 Y 值（在 OCS 中），多个条目；每个顶点一个条目

        //public string C91 { get; set; }//顶点标识符
        //public double C40 { get; set; }//起点宽度（多个条目；每个顶点一个条目）（可选；默认值 = 0；多个条目）。如果设定为固定宽度（代码 43），则不使用
        //public double C41 { get; set; }//端点宽度（多个条目；每个顶点一个条目）（可选；默认值 = 0；多个条目）。如果设定为固定宽度（代码 43），则不使用
        //public double C42 { get; set; }//凸度（多个条目；每个顶点一个条目）（可选；默认值 = 0）
        //public double C210 { get; set; }//拉伸方向（可选；默认值 = 0, 0, 1）DXF：X 值；APP：三维矢量
        //public double C220 { get; set; }//DXF：拉伸方向的 Y 值和 Z 值（可选）
        //public double C230 { get; set; }

        public List<P2D> P2D { get; set; }

        public LWPOLYLINE(ILoader dxfData, Property prop)
            : base(dxfData, prop)
        {
            P2D = new List<P2D>();
        }

        protected override bool ReadProperty(Property prop)
        {
            switch (prop.Code)
            {
                case 100: C100 = prop.Value; break;
                case 90: C90 = prop.Value.ToInt(); break;
                case 70: C70 = prop.Value.ToInt(); break;
                case 43: C43 = prop.Value; break;
                case 38: C38 = prop.Value.ToDouble(); break;
                case 39: C39 = prop.Value.ToDouble(); break;
                //case 91: C91 = prop.Value; break;
                case 10:
                    P2D.Add(new P2D()
                    {
                        X = prop.Value.ToDouble(),
                        Y = DXFData.Next().Value.ToDouble(),
                    });
                    break;
                default:
                    return base.ReadProperty(prop);
            }
            return false;
        }
    }
}
