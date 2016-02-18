using UnityEngine;
using System.Collections;
using DXFConvert;
using System.Collections.Generic;

//实体集合基类
public class EntitiesBase : MonoBehaviour
{

    public GameObject GoLine;
    public GameObject GoLwpolyLine;
    public GameObject GoCircle;
    public GameObject GoArc;
    public GameObject GoEllipse;
    public GameObject GoInsert;
    public GameObject GoText;

    public Material GoDefaultMat;//默认材质


    //绘制直线集合
    public void DrawLINEList(DXFStructure dxf, List<LINE> LINEList, float ScaleX = 1, float ScaleY = 1)
    {
        //绘制直线
        foreach (LINE item in LINEList)
        {
            GameObject go = Instantiate(GoLine) as GameObject;
            go.transform.parent = gameObject.transform;
            var l = go.GetComponent<GoLine>();
            l.Set(dxf, item, ScaleX, ScaleY);
        }
    }

    //绘制多段线集合
    public void DrawLWPOLYLINEList(DXFStructure dxf, List<LWPOLYLINE> LWPOLYLINEList, float ScaleX = 1, float ScaleY = 1)
    {
        // 多段线
        foreach (var item in LWPOLYLINEList)
        {
            GameObject go = Instantiate(GoLwpolyLine) as GameObject;
            go.transform.parent = gameObject.transform;
            var l = go.GetComponent<GoLwpolyLine>();
            l.Set(dxf, item, ScaleX, ScaleY);
        }
    }

    //绘制文本集合
    public void DrawTEXTList(DXFStructure dxf, List<TEXT> TEXTList, float ScaleX = 1, float ScaleY = 1)
    {
        //绘制直线
        foreach (TEXT item in TEXTList)
        {
            GameObject go = Instantiate(GoText) as GameObject;
            go.transform.parent = gameObject.transform;
            var l = go.GetComponent<GoText>();
            l.Set(dxf, item, ScaleX, ScaleY);
        }
    }

    //绘制圆集合
    public void DrawCIRCLEList(DXFStructure dxf, List<CIRCLE> CIRCLEList, float ScaleX = 1, float ScaleY = 1)
    {
        // 绘制圆
        foreach (var item in CIRCLEList)
        {
            GameObject go = Instantiate(GoCircle) as GameObject;
            go.transform.parent = gameObject.transform;
            var l = go.GetComponent<GoCircle>();
            l.Set(dxf, item, ScaleX, ScaleY);
        }
    }

    //绘制圆弧集合
    public void DrawARCList(DXFStructure dxf, List<ARC> ARCList, float ScaleX = 1, float ScaleY = 1)
    {
        //绘制弧线
        int ii = 0;
        foreach (var item in ARCList)
        {
            GameObject go = Instantiate(GoArc) as GameObject;
            go.transform.parent = gameObject.transform;
            var l = go.GetComponent<GoArc>();
            l.Set(dxf, item, ScaleX, ScaleY);

            ii++;
            System.Diagnostics.Debug.WriteLine(ii.ToString());
        }
    }

    //绘制块集合
    public void DrawINSERTList(DXFStructure dxf, List<INSERT> INSERTList, float ScaleX = 1, float ScaleY = 1)
    {
        foreach (var item in INSERTList)
        {
            GameObject go = Instantiate(GoInsert) as GameObject;
            go.transform.parent = gameObject.transform;
            var l = go.GetComponent<GoInsert>();
            l.Set(dxf, item);
        }
    }

    //绘制椭圆集合
    public void DrawELLIPSEList(DXFStructure dxf, List<ELLIPSE> ELLIPSEList, float ScaleX = 1, float ScaleY = 1)
    {
        //绘制椭圆
        foreach (var item in ELLIPSEList)
        {
            GameObject go = Instantiate(GoEllipse) as GameObject;
            go.transform.parent = gameObject.transform;
            var l = go.GetComponent<GoEllipse>();
            l.Set(dxf, item, ScaleX, ScaleY);

        }
    }
}