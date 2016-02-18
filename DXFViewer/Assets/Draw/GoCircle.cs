using UnityEngine;
using System.Collections;
using DXFConvert;

//园
[RequireComponent(typeof(LineRenderer))]
public class GoCircle : MonoBehaviour, IResizeObject
{

    public LineRenderer lr;
    public float ZoomAdjust = 1;

    private float Diameter = 0;//直径，用来来优化园的显示，太小的园就不要显示了
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
    //  //  lr.SetWidth(GoView.Content.Zoom * ZoomAdjust, GoView.Content.Zoom * ZoomAdjust);
    //}

    public int MaxResolution = 360;//最大线段数量
    public int MinResolution = 36;//最小线段数里
    public int OptimizingLevel = 10;//优化等级，数字越大优化级别越高，越容易失真

    public void Set(DXFStructure dxf, CIRCLE item, float ScaleX = 1, float ScaleY = 1)
    {
        var goLayer = GoView.Content.GetLayer(item.C8);
        if (goLayer != null)
        {
            lr.material = goLayer.LayerMaterial;
            ZoomAdjust = goLayer.ZoomAdjust;
        }

        float R = (float)item.C40;
        Diameter = R * 2;
        //计算一个圆需要多少线条
        int resolution = (int)item.C40 / OptimizingLevel;
        if (resolution > MaxResolution) resolution = MaxResolution;
        if (resolution < MinResolution) resolution = MinResolution;

        lr.SetVertexCount(resolution + 1);

        for (int i = 0; i < resolution; ++i)
        {
            lr.SetPosition(i, new Vector3((R * Mathf.Cos(2 * Mathf.PI / resolution * i) + (float)item.C10) * ScaleX,
                (R * Mathf.Sin(2 * Mathf.PI / resolution * i) + (float)item.C20) * ScaleY, 0));
        }
        lr.SetPosition(resolution, new Vector3((R * Mathf.Cos(2 * Mathf.PI / resolution * 0) + (float)item.C10) * ScaleX,
            (R * Mathf.Sin(2 * Mathf.PI / resolution * 0) + (float)item.C20) * ScaleY, 0));

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