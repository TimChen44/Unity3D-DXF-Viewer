using UnityEngine;
using System.Collections;

public class Manager : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
       // this.GetComponent<Loader.ILoader>().Loaded = Loaded;

    }

    void Loaded(string fileContent)
    {
        string aa = fileContent;

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void OnGUI()
    {
//        GUI.Label(new Rect(25, 25, 100, 20), "DXF文件地址");
//        path = GUI.TextField(new Rect(25, 50, 300, 20), path);

//        if (GUI.Button(new Rect(25, 75, 50, 20), "载入"))
//        {
//#if DEBUG


//            Loaded(System.IO.File.ReadAllText(Application.dataPath + "\\TestData\\Test1.dxf"));
//            return;

//#endif


//            if (System.IO.File.Exists(path) == false) return;
//            if (Loaded != null)
//                Loaded(System.IO.File.ReadAllText(path));
//        }
    }
}
