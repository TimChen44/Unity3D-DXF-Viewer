using UnityEngine;
using System.Collections;
using TimCommon.DXFConvert;

//圆弧
[RequireComponent(typeof(LineRenderer))]
public class GoArc : MonoBehaviour, IResizeObject
{

    public LineRenderer lr;

    public float ZoomAdjust = 1;

    private float Diameter = 0;//直径，用来来优化圆弧的显示，太小的园就不要显示了

    void Awake()
    {
        GoView.Content.ResizeObjects.Add(this);
    }

    // Update is called once per frame
    //void Update()
    //{
    //    if (GoView.Content.Zoom > Diameter)
    //    {
    //        lr.enabled = false;
    //        return;
    //    }
    //    lr.enabled = true;
    //   // lr.SetWidth(GoView.Content.Zoom * ZoomAdjust, GoView.Content.Zoom * ZoomAdjust);
    //}

    public int MaxResolution = 360;//最大线段数量
    public int MinResolution = 36;//最小线段数里
    public int OptimizingLevel = 10;//优化等级，数字越大优化级别越高，越容易失真

    public void Set(DXFStructure dxf, ARC item, float ScaleX = 1, float ScaleY = 1)
    {
        var goLayer = GoView.Content.GetLayer(item.C8);
        if (goLayer != null)
        {
            lr.material = goLayer.LayerMaterial;
            ZoomAdjust = goLayer.ZoomAdjust;
        }

        float R = (float)item.C40;
        Diameter = R * 2;

        float nd = 0;//绘制的角度总数
        if (item.C51 > item.C50)
            nd = (float)(item.C51 - item.C50);
        else
            nd = (float)(item.C51 + 360 - item.C50);

        //计算一个圆弧需要多少线条
        float ndB=nd / 360;//圆弧占用圆的比例
        int resolution = (int)(item.C40 / OptimizingLevel * ndB);
        if (resolution > (int)(MaxResolution * ndB)) resolution = (int)(MaxResolution * ndB);
        if (resolution < (int)(MinResolution * ndB)) resolution = (int)(MinResolution * ndB);

        lr.SetVertexCount(resolution + 1);

        for (int i = 0; i < resolution; i++)
        {
            var ii = (float)(i * nd / (float)resolution);

            ii += (float)item.C50;
            if (ii > 360) ii -= 360;

            lr.SetPosition(i, new Vector3((R * Mathf.Cos(2 * Mathf.PI / 360 * ii) + (float)item.C10) * ScaleX,
            (R * Mathf.Sin(2 * Mathf.PI / 360 * ii) + (float)item.C20) * ScaleY, 0));
        }

        lr.SetPosition(resolution, new Vector3((R * Mathf.Cos(2 * Mathf.PI / 360 * (float)item.C51) + (float)item.C10) * ScaleX,
            (R * Mathf.Sin(2 * Mathf.PI / 360 * (float)item.C51) + (float)item.C20) * ScaleY, 0));


        this.gameObject.isStatic = true;
    }


    #region IResizeObject 成员

    public void SetSetWidth()
    {
        lr.SetWidth(GoView.Content.Zoom * ZoomAdjust, GoView.Content.Zoom * ZoomAdjust);
    }

    //只会隐藏
    public void ToMin()
    {
        if (GoView.Content.Zoom > Diameter) lr.gameObject.SetActive(false);
    }

    //只会显示
    public void ToMax()
    {
        if (GoView.Content.Zoom < Diameter) lr.gameObject.SetActive(true);
    }

    public void HideOrShow()
    {
        if (GoView.Content.Zoom > Diameter) lr.gameObject.SetActive(false);
        else lr.gameObject.SetActive(true);
    }

    #endregion

}
