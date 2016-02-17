using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace TimCommon.DXFConvert
{
    //http://docs.autodesk.com/ACD/2011/CHS/filesDXF/WS1a9193826455f5ff18cb41610ec0a2e719-79fe.htm
    public class LINE : ENTITIE
    {
        public LINE() { }

        public string C100 { get; set; }//子类标记 (AcDbLine) 

        public double  C39 { get; set; }//厚度（可选；默认值 = 0）

        public double C10 { get; set; }//起点（在 WCS 中）DXF：X 值；APP：三维点 
        public double C20 { get; set; }//
        public double C30 { get; set; }//DXF：起点的 Y 值和 Z 值（在 WCS 中） 


        public double C11 { get; set; }//端点（在 WCS 中）DXF：X 值；APP：三维点 
        public double C21 { get; set; }//
        public double C31 { get; set; }//DXF：端点的 Y 值和 Z 值（在 WCS 中） 


        public double C210 { get; set; }//拉伸方向（可选；默认值 = 0, 0, 1）DXF：X 值；APP：三维矢量 
        public double C220 { get; set; }//
        public double C230 { get; set; }//DXF：拉伸方向的 Y 值和 Z 值（可选） 



        public LINE(DXFImage dxfImage, Property prop)
            : base(dxfImage, prop)
        {

        }

        protected override bool ReadProperty(Property prop)
        {
            switch (prop.Code)
            {
                case 100: C100 = prop.Value; break;
                case 39: C39 = prop.Value.ToDouble(); break;

                case 10: C10 = prop.Value.ToDouble(); break;
                case 20: C20 = prop.Value.ToDouble(); break;
                case 30: C30 = prop.Value.ToDouble(); break;

                case 11: C11 = prop.Value.ToDouble(); break;
                case 21: C21 = prop.Value.ToDouble(); break;
                case 31: C31 = prop.Value.ToDouble(); break;

                case 210: C210 = prop.Value.ToDouble(); break;
                case 220: C220 = prop.Value.ToDouble(); break;
                case 230: C230 = prop.Value.ToDouble(); break;
                default:
                    return base.ReadProperty(prop);
            }
            return false;
        }
    }
}


 
