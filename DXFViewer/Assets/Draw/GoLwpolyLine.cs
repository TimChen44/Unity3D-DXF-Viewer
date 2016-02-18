using UnityEngine;
using System.Collections;
using DXFConvert;

//多段线
//[RequireComponent(typeof(LineRenderer))]
public class GoLwpolyLine : MonoBehaviour
{
    public GameObject goLine;


    // Update is called once per frame

    public void Set(DXFStructure dxf, LWPOLYLINE item, float ScaleX = 1, float ScaleY = 1)
    {
        var goLayer = GoView.Content.GetLayer(item.C8);

        for (int i = 0; i < item.P2D.Count - 1; i++)
        {
            // lr.SetPosition(i, new Vector3((float)item.P2D[i].X * ScaleX, (float)item.P2D[i].Y * ScaleY, 0));

            GameObject go = Instantiate(goLine) as GameObject;
            go.transform.parent = gameObject.transform;
            var l = go.GetComponent<GoLine>();
            l.Set(dxf, item.P2D[i], item.P2D[i + 1], goLayer, ScaleX, ScaleY);
        }

        if (item.C70 == 1 && item.P2D.Count > 2)
        {
            GameObject go = Instantiate(goLine) as GameObject;
            go.transform.parent = gameObject.transform;
            var l = go.GetComponent<GoLine>();
            l.Set(dxf, item.P2D[item.P2D.Count - 1], item.P2D[0], goLayer, ScaleX, ScaleY);
        }
    }
}
