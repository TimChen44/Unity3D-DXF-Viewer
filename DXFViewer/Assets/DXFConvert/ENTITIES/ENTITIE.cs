using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Loader;

namespace DXFConvert
{
    //http://docs.autodesk.com/ACD/2011/CHS/filesDXF/WS1a9193826455f5ff18cb41610ec0a2e719-7a3d.htm
    public class ENTITIE : Entity
    {//实体中通用组码处理
        public ENTITIE() { }

        //-1	APP：图元名（每次打开图形时都会发生变化）
        //0	图元类型
        //5	句柄
        //102	应用程序定义的组的开始	“{application_name”（可选）应用程序定义的代码	102 组中的代码和值由应用程序定义（可选）
        //102	组的结束“}”（可选）
        //102	“{ACAD_REACTORS”表示 AutoCAD 永久反应器组的开始。仅当将永久反应器附加到此对象时，此组才存在（可选）
        //330	所有者词典的软指针 ID/句柄（可选）
        //102	组的结束“}”（可选）
        //102	“{ACAD_XDICTIONARY”表示扩展词典组的开始。仅当将扩展词典附加到此对象时，此组才存在（可选）
        //360	所有者词典的硬所有者 ID/句柄（可选）
        //102	组的结束“}”（可选）
        //330	所有者 BLOCK_RECORD 对象的软指针 ID/句柄
        //100	子类标记 (AcDbEntity)
        //67	不存在或零表示图元位于模型空间中。1 表示图元位于图纸空间中（可选）
        //410	APP：布局选项卡名
        public string C8 { get; set; }  //8	图层名
        //6	线型名（如果不是“BYLAYER”，则出现）。特殊名称“BYBLOCK”表示可变的线型（可选）
        //347	材质对象的硬指针 ID/句柄（如果不是“BYLAYER”，则出现）
        //62	颜色号（如果不是“BYLAYER”，则出现）；零表示“BYBLOCK”（可变的）颜色；256 表示“BYLAYER”；负值表示层已关闭（可选）
        //370	线宽枚举值。作为 16 位整数存储和移动。
        //48	线型比例（可选）
        //60	对象可见性（可选）：0 = 可见；1 = 不可见
        //92	后面的 310 组（二进制数据块记录）中表示的代理图元图形中的字节数（可选）
        //310	代理图元图形数据（多行；每行最多 256 个字符）（可选）
        //420	一个 24 位颜色值，应按照值为 0 到 255 的字节进行处理。最低字节是蓝色值，中间字节是绿色值，第三个字节是红色值。最高字节始终为 0。该组码不能用于自定义图元本身的数据，因为该组码是为 AcDbEntity 类级别颜色数据和 AcDbEntity 类级别透明度数据保留的。
        //430	颜色名。该组码不能用于自定义图元本身的数据，因为该组码是为 AcDbEntity 类级别颜色数据和 AcDbEntity 类级别透明度数据保留的。
        //440	透明度值。该组码不能用于自定义图元本身的数据，因为该组码是为 AcDbEntity 类级别颜色数据和 AcDbEntity 类级别透明度数据保留的。
        //390	打印样式对象的硬指针 ID/句柄
        //284	阴影模式	0 = 投射和接收阴影	1 = 投射阴影	2 = 接收阴影	3 = 忽略阴影


        public ENTITIE(ILoader dxfData, Property prop)
            : base(dxfData, prop)
        {

        }

        protected override bool ReadProperty(Property prop)
        {
            switch (prop.Code)
            {
                case 8: C8 = prop.Value; break;
                default:
                    return base.ReadProperty(prop);
            }
            return false;
        }
    }

    //2D坐标
    public class P2D
    {
        public double X { get; set; }//x坐标：10
        public double Y { get; set; }//y坐标：20
    }

    //2D坐标
    public class P3D
    {
        public double X { get; set; }//x坐标：10
        public double Y { get; set; }//y坐标：20
        public double Z { get; set; }//y坐标：30
    }
}
