using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace TimCommon.DXFConvert
{
    //http://docs.autodesk.com/ACD/2011/CHS/filesDXF/WSfacf1429558a55de185c428100849a0ab7-5df0.htm
    public class ENTITIES : SECTION
    {
        public ENTITIES() { }

        public ENTITIES(DXFImage dxfImage, Property prop)
            : base(dxfImage, prop)
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
            if (prop.Code == 0 && prop.Value != "ENDSEC")
            {
                switch (prop.Value)
                {
                    //case "C3DFACE":
                    //case "C3DSOLID":
                    //case "ACAD_PROXY_ENTITY":
                    case "ARC":
                        var arc = new ARC(DXFImage, prop);
                        ARCList.Add(arc);
                        return arc.ReadProperties();
                    //case "ATTDEF":
                    //case "ATTRIB":
                    //case "BODY":
                    case "CIRCLE":
                        var circle = new CIRCLE(DXFImage, prop);
                        CIRCLEList.Add(circle);
                        return circle.ReadProperties();
                    //case "DIMENSION":
                    case "ELLIPSE":
                        var ellipes = new ELLIPSE(DXFImage, prop);
                        ELLIPSEList.Add(ellipes);
                        return ellipes.ReadProperties();
                    //case "HATCH":
                    //case "HELIX":
                    //case "IMAGE":
                    case "INSERT":
                        var insert = new INSERT(DXFImage, prop);
                         INSERTList.Add(insert);
                         return insert.ReadProperties();
                    //case "LEADER":
                    //case "LIGHT":                    
                    case "LINE":
                        var line = new LINE(DXFImage, prop);
                        LINEList.Add(line);
                        return line.ReadProperties();
                    case "LWPOLYLINE":
                        var lwpolyline = new LWPOLYLINE(DXFImage, prop);
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
                        var text = new TEXT(DXFImage, prop);
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
                        return CreateSonClass(new ENTITIE(DXFImage, prop));
                }
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
}
