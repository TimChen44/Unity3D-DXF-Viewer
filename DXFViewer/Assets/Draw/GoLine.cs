using UnityEngine;
using System.Collections;
using TimCommon.DXFConvert;

[RequireComponent(typeof(LineRenderer))]
public class GoLine : MonoBehaviour, IResizeObject
{

    public LineRenderer lr;

    public float ZoomAdjust = 1;

    public string c8;

    public GoLayer gg;

    private float LineLenght = 0;//线段你的长处，长度短的先在远距离查看是就可以隐藏了，减轻系统负担

    // Use this for initialization
    void Awake()
    {
        GoView.Content.ResizeObjects.Add(this);
    }

    // //Update is called once per frame
    //void Update()
    //{
    //    if (GoView.Content.Zoom > LineLenght)
    //    {
    //        lr.enabled = false;
    //        return;
    //    }
    //    lr.enabled = true;
    //    //lr.SetWidth(GoView.Content.Zoom * ZoomAdjust, GoView.Content.Zoom * ZoomAdjust);
    //}


    //设置线段
    public void Set(DXFStructure dxf, LINE item, float ScaleX = 1, float ScaleY = 1)
    {
        var goLayer = GoView.Content.GetLayer(item.C8);
        if (goLayer != null)
        {
            lr.material = goLayer.LayerMaterial;
            ZoomAdjust = goLayer.ZoomAdjust;
        }

        gg = goLayer;

        lr.SetVertexCount(2);

        var p1 = new Vector3((float)item.C10 * ScaleX, (float)item.C20 * ScaleY, (float)item.C30);
        var p2 = new Vector3((float)item.C11 * ScaleX, (float)item.C21 * ScaleY, (float)item.C31);

        lr.SetPosition(0, p1);
        lr.SetPosition(1, p2);

        LineLenght = Vector2.Distance(p1, p2) * 2;

        this.gameObject.isStatic = true;

        if (GoView.Content.MaxX < p1.x)
            GoView.Content.MaxX = p1.x;
        if (GoView.Content.MinX > p1.x)
            GoView.Content.MinX = p1.x;

        if (GoView.Content.MaxY < p1.y)
            GoView.Content.MaxY = p1.y;
        if (GoView.Content.MinY > p1.y)
            GoView.Content.MinY = p1.y;
    }

    public void Set(DXFStructure dxf, P2D P1, P2D P2, GoLayer goLayer, float ScaleX = 1, float ScaleY = 1)
    {
        lr.material = goLayer.LayerMaterial;
        ZoomAdjust = goLayer.ZoomAdjust;

        lr.SetVertexCount(2);

        var p1 = new Vector3((float)P1.X * ScaleX, (float)P1.Y * ScaleY, 0);
        var p2 = new Vector3((float)P2.X * ScaleX, (float)P2.Y * ScaleY, 0);

        lr.SetPosition(0, p1);
        lr.SetPosition(1, p2);

        LineLenght = Vector2.Distance(p1, p2) * 2;

        this.gameObject.isStatic = true;

        if (GoView.Content.MaxX < p1.x)
            GoView.Content.MaxX = p1.x;
        if (GoView.Content.MinX > p1.x)
            GoView.Content.MinX = p1.x;

        if (GoView.Content.MaxY < p1.y)
            GoView.Content.MaxY = p1.y;
        if (GoView.Content.MinY > p1.y)
            GoView.Content.MinY = p1.y;
    }

    #region IResizeObject 成员

    public void SetSetWidth()
    {
        lr.SetWidth(GoView.Content.Zoom * ZoomAdjust, GoView.Content.Zoom * ZoomAdjust);
    }

    //只会隐藏
    public void ToMin()
    {
        if (GoView.Content.Zoom > LineLenght) lr.gameObject.SetActive(false);
    }

    //只会显示
    public void ToMax()
    {
        if (GoView.Content.Zoom < LineLenght) lr.gameObject.SetActive(true);
    }

    public void HideOrShow()
    {
        if (GoView.Content.Zoom > LineLenght) lr.gameObject.SetActive(false);
        else lr.gameObject.SetActive(true);
    }

    #endregion

}
