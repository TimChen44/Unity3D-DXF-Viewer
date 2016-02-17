using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace TimCommon.DXFConvert
{
    //http://docs.autodesk.com/ACD/2011/CHS/filesDXF/WS1a9193826455f5ff18cb41610ec0a2e719-79d1.htm
    public class TEXT : ENTITIE
    {
        public TEXT() { }

        //100	子类标记 (AcDbText)
        //39	厚度（可选；默认值 = 0）
        public double C10 { get; set; }       //10	第一对齐点（在 OCS 中）	DXF：X 值；APP：三维点
        public double C20 { get; set; }//20
        public double C30 { get; set; } //30	DXF：第一对齐点的 Y 值和 Z 值（在 OCS 中）
        public double C40 { get; set; } //40	文字高度  
        public string C1 { get; set; } //1	默认值（字符串本身）
        //50	文字旋转角度（可选；默认值 = 0）
        //41	相对 X 比例因子 — 宽度（可选；默认值 = 1）	使用拟合类型的文字时，该值也将进行调整。
        //51	倾斜角（可选；默认值 = 0）
        //7	文字样式名（可选；默认值 = 标准）
        //71	文字生成标志（可选；默认值 = 0）：	2 = 文字反向（在 X 轴方向镜像）	4 = 文字倒置（在 Y 轴方向镜像）
        //72	文字水平对正类型（可选；默认值 = 0）整数代码（非按位编码）	0 = 左对正；1 = 居中对正；2 = 右对正	3 = 对齐（如果垂直对齐 = 0）	4 = 中间（如果垂直对齐 = 0）	5 = 拟合（如果垂直对齐 = 0）	详细信息请参见组 72 和 73 整数代码表
        //11	第二对齐点（在 OCS 中）（可选）	DXF：X 值；APP：三维点	只有当 72 或 73 组的值非零时，该值才有意义（如果对正不是基线对正/左对正）
        //21, 31	DXF：第二对齐点的 Y 值和 Z 值（在 OCS 中）（可选）
        //210	拉伸方向（可选；默认值 = 0, 0, 1）	DXF：X 值；APP：三维矢量
        //220, 230	DXF：拉伸方向的 Y 值和 Z 值（可选）
        //100	子类标记 (AcDbText)
        //73	文字垂直对正类型（可选；默认值 = 0）整数代码（不是按位编码）	0 = 基线对正；1 = 底端对正；2 = 居中对正；3 = 顶端对正	详细信息请参见组 72 和 73 整数代码表

        public TEXT(DXFImage dxfImage, Property prop)
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
                case 40: C40 = prop.Value.ToDouble(); break;
                case 1: C1 = prop.Value; break;
                default:
                    return base.ReadProperty(prop);
            }
            return false;
        }
    }
}
