using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace TimCommon.DXFConvert
{
    //http://docs.autodesk.com/ACD/2011/CHS/filesDXF/WS1a9193826455f5ff18cb41610ec0a2e719-7a51.htm
    public class LAYER : TABLESonBase
    {
        public LAYER() { }

//100	子类标记 (AcDbLayerTableRecord)
        public string C2 { get; set; }//2	图层名
//70	标准标记（按位编码值）：	1 = 冻结图层，否则解冻图层	2 = 默认情况下在新视口中冻结图层	4 = 锁定图层	16 = 如果设定了此位，则表条目外部依赖于外部参照	32 = 如果同时设定了此位和位 16，则表明已成功融入了外部依赖的外部参照	64 = 如果设定了此位，则表明在上次编辑图形时，图形中至少有一个图元参照了表条目。（此标志对 AutoCAD 命令很有用。大多数读取 DXF 文件的程序都可以忽略它，并且无需由写入 DXF 文件的程序对其进行设定）
        public int C62 { get; set; }//62	颜色编号（如果为负值，则表明图层处于关闭状态）
//6	线型名
//290	打印标志。如果设定为 0，则不打印此图层
        public int C370 { get; set; }//370	线宽枚举值
//390	PlotStyleName 对象的硬指针 ID/句柄
//347	Material 对象的硬指针 ID/句柄


        public LAYER(DXFImage dxfImage, Property prop)
            : base(dxfImage, prop)
        {
           
        }


        protected override bool ReadProperty(Property prop)
        {
            switch (prop.Code)
            {
                case 2: C2 = prop.Value; break;
                case 62: C62 = prop.Value.ToInt(); break;
                case 370: C370 = prop.Value.ToInt(); break;
                default:
                    return base.ReadProperty(prop);
            }
            return false;
        }
    }
}