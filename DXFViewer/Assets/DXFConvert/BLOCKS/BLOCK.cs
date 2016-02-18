using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Loader;

namespace DXFConvert
{
    //http://docs.autodesk.com/ACD/2011/CHS/filesDXF/WS1a9193826455f5ff18cb41610ec0a2e719-7a43.htm
    public class BLOCK : Entity
    {
        public BLOCK() { }

        //0	图元类型 (BLOCK)
        //5	句柄
        //102	应用程序定义的组的开始“{application_name”。例如，“{ACAD_REACTORS”表示 AutoCAD 永久反应器组的开始（可选）
        //应用程序定义的代码	102 组中的代码和值由应用程序定义（可选）
        //102	组的结束“}”（可选）
        //330	所有者对象的软指针 ID/句柄
        //100	子类标记 (AcDbEntity)
        //8	图层名
        //100	子类标记 (AcDbBlockBegin)
        public string C2 { get; set; } //2	块名
        //70	块类型标志（按位编码值，可以组合使用）：	0 = 表示不应用下列任何标志	1 = 由图案填充、关联标注、其他内部操作或应用程序生成的匿名块	2 = 块具有非固定属性定义（如果块具有任何固定属性定义或根本没有属性定义，则不设定此位）	4 = 块是外部参照 (xref)	8 = 块是外部参照覆盖	16 = 块依赖外部参照	32 = 块是融入的外部参照，或者依赖外部参照（输入时被忽略）	64 = 定义是被引用的外部参照（输入时被忽略）
        public double C10 { get; set; }  //10	基点	DXF：X 值；APP：三维点
        public double C20 { get; set; }//20	DXF：基点的 Y 值和 Y 值
        public double C30 { get; set; }//30
        //3	块名
        //1	外部参照路径名
        //4	块说明（可选）

        public BLOCK(ILoader dxfData, Property prop)
            : base(dxfData, prop)
        {
            LINEList = new List<LINE>();
            LWPOLYLINEList = new List<LWPOLYLINE>();
            TEXTList = new List<TEXT>();
            CIRCLEList = new List<CIRCLE>();
            ARCList = new List<ARC>();
            INSERTList = new List<INSERT>();
            ELLIPSEList = new List<ELLIPSE>();
        }

        public List<LINE> LINEList { get; set; }
        public List<LWPOLYLINE> LWPOLYLINEList { get; set; }
        public List<TEXT> TEXTList { get; set; }
        public List<CIRCLE> CIRCLEList { get; set; }
        public List<ARC> ARCList { get; set; }
        public List<INSERT> INSERTList { get; set; }
        public List<ELLIPSE> ELLIPSEList { get; set; }

        protected override Property ReadSonClass(Property prop)
        {
            if (prop.Code == 0 && prop.Value != "ENDBLK")
            {
                switch (prop.Value)
                {
                    //case "C3DFACE":
                    //case "C3DSOLID":
                    //case "ACAD_PROXY_ENTITY":
                    case "ARC":
                        var arc = new ARC(DXFData, prop);
                        ARCList.Add(arc);
                        return arc.ReadProperties();
                    //case "ATTDEF":
                    //case "ATTRIB":
                    //case "BODY":
                    case "CIRCLE":
                        var circle = new CIRCLE(DXFData, prop);
                        CIRCLEList.Add(circle);
                        return circle.ReadProperties();
                    //case "DIMENSION":
                    case "ELLIPSE":
                        var ellipes = new ELLIPSE(DXFData, prop);
                        ELLIPSEList.Add(ellipes);
                        return ellipes.ReadProperties();
                    //case "HATCH":
                    //case "HELIX":
                    //case "IMAGE":
                    case "INSERT":
                        var insert = new INSERT(DXFData, prop);
                        INSERTList.Add(insert);
                        return insert.ReadProperties();
                    //case "LEADER":
                    //case "LIGHT":                    
                    case "LINE":
                        var line = new LINE(DXFData, prop);
                        LINEList.Add(line);
                        return line.ReadProperties();
                    case "LWPOLYLINE":
                        var lwpolyline = new LWPOLYLINE(DXFData, prop);
                        LWPOLYLINEList.Add(lwpolyline);
                        return lwpolyline.ReadProperties();
                    //case "MESH": 
                    //case "MLINE": 
                    //case "MLEADERSTYLE": 
                    //case "MLEADER": 
                    //case "MTEXT": 
                    //case "OLEFRAME": 
                    //case "OLE2FRAME": 
                    //case "POINT": 
                    //case "POLYLINE": 
                    //case "RAY": 
                    //case "REGION": 
                    //case "SECTION": 
                    //case "SEQEND": 
                    //case "SHAPE": 
                    //case "SOLID": 
                    //case "SPLINE": 
                    //case "SUN": 
                    //case "SURFACE": 
                    //case "TABLE": 
                    case "TEXT":
                        var text = new TEXT(DXFData, prop);
                        TEXTList.Add(text);
                        return text.ReadProperties();
                    //case "TOLERANCE": 
                    //case "TRACE": 
                    //case "UNDERLAY": 
                    //case "VERTEX": 
                    //case "VIEWPORT": 
                    //case "WIPEOUT": 
                    //case "XLINE": 

                    default:
                        return CreateSonClass(new ENTITIE(DXFData, prop));
                }
            }
            else
            {
                return base.ReadSonClass(prop);
            }
        }


        protected override bool ReadProperty(Property prop)
        {
            if (prop.Code == 0 && prop.Value == "ENDBLK")
            {
                return true;
            }
            else
            {
                switch (prop.Code)
                {
                    case 2: C2 = prop.Value; break;
                    case 10: C10 = prop.Value.ToDouble(); break;
                    case 20: C20 = prop.Value.ToDouble(); break;
                    case 30: C30 = prop.Value.ToDouble(); break;
                    default:
                        SaveProperty(prop); break;
                }
                return false;
            }
        }
    }
}
