using UnityEngine;
using System.Collections;
using DXFConvert;
using System.Collections.Generic;
using System.Linq;
public class GoView : MonoBehaviour
{
    //图层对象
    public GameObject Layer;

    //视图缩放级别，用来控制线条显示粗细
    public float Zoom;

    //用于显示的相机
    public Camera camera;

    //图层列表，用于其他元检索
    public Dictionary<string, GoLayer> Layers;

    public float MaxX = 0;
    public float MinX = 0;

    public float MaxY = 0;
    public float MinY = 0;

    void Awake()
    {
        Content = this;
        Layers = new Dictionary<string, GoLayer>();

        if (camera == null)
            camera = Camera.main;
    }

    private bool isUdelta = false;

    // Update is called once per frame
    void Update()
    {
        //鼠标滚动缩放
        float delta = Input.GetAxisRaw("Mouse ScrollWheel");
        if (delta != 0)
        {
            isUdelta = true;

            camera.orthographicSize -= delta * (camera.orthographicSize);

            //计算缩放等级
            Zoom = (camera.orthographicSize / 150);

            if (delta > 0)
                ResizeObjects.ForEach((l) => { if (l.gameObject.activeSelf == false) l.ToMax(); });
            else
                ResizeObjects.ForEach((l) => { if (l.gameObject.activeSelf == true)  l.ToMin(); });
            ResizeObjects.ForEach((l) =>
            {
                if (l.gameObject.activeSelf)
                    l.SetSetWidth();
            });

            isUdelta = false;
        }

        //鼠标右键移动
        if (Input.GetMouseButton(2) && isUdelta == false)
        {
            camera.transform.Translate(camera.transform.right * -Input.GetAxis("Mouse X") * (camera.orthographicSize / 2) * 0.2f, Space.World);
            camera.transform.Translate(camera.transform.up * -Input.GetAxis("Mouse Y") * (camera.orthographicSize / 2) * 0.2f, Space.World);
        }
    }

    public static GoView Content { get; set; }
    /// <summary>
    /// 可以调整的对象集合
    /// </summary>
    public List<IResizeObject> ResizeObjects = new List<IResizeObject>();

    public void Set(DXFStructure dxf)
    {
        //先初始化图层
        foreach (TABLE table in dxf.TABLES.TABLEList)
        {
            if (table.LAYERList.Count == 0) continue;

            foreach (LAYER item in table.LAYERList)
            {
                GameObject go = Instantiate(Layer) as GameObject;
                go.transform.parent = gameObject.transform;
                var l = go.GetComponent<GoLayer>();
                l.Set(item);

                if (item.C2 != null)
                    Layers.Add(item.C2, l);
            }
        }

        //构建一个默认图层，没有图层属性的对象放在此图层
        GameObject goDefaultLayer = Instantiate(Layer) as GameObject;
        GoLayer.DefaultLayer = goDefaultLayer.GetComponent<GoLayer>();
        GoLayer.DefaultLayer.ZoomAdjust = 0.8f;

        //绘制各图层下的元素
        foreach (var item in Layers)
        {
            item.Value.Load(dxf);
        }

        //初始化相机位置
        //camera.transform.position = new Vector3((MaxX + MinX) / 2, (MaxY + MinY) / 2, -10);

        this.transform.position = new Vector3(this.transform.position.x - (MaxX + MinX) / 2,
          this.transform.position.y - (MaxY + MinY) / 2,
            0);

        camera.orthographicSize = ((MaxX - MinX) + (MaxY - MinY)) / 20;

        Zoom = (camera.orthographicSize / 250);


        //初始化优化代码
        ResizeObjects.ForEach((l) =>
        {
            l.ToMin();
            if (l.gameObject.activeSelf)
                l.SetSetWidth();
        });
    }

    //获得图层材质
    public Material GetLayerMaterial(string name)
    {
        if (Layers.ContainsKey(name))
            return Layers[name].LayerMaterial;
        else
        {
            Debug.LogError("GetLayerMaterial_No Layer_" + name);
            return null;
        }
    }


    //获得图层材质
    public GoLayer GetLayer(string name)
    {
        if (Layers.ContainsKey(name))
            return Layers[name];
        else
            return GoLayer.DefaultLayer;
    }
}
