using UnityEngine;
using System.Collections;
using TimCommon.DXFConvert;
using System.Linq;
using System.Collections.Generic;

public class GoLayer : EntitiesBase
{
    //图层材质
    public Material LayerMaterial;

    public float ZoomAdjust = 1;


    LAYER Layer;
    public List<LINE> LINEList { get; set; }
    public List<LWPOLYLINE> LWPOLYLINEList { get; set; }
    public List<TEXT> TEXTList { get; set; }
    public List<CIRCLE> CIRCLEList { get; set; }
    public List<ARC> ARCList { get; set; }
    public List<INSERT> INSERTList { get; set; }
    public List<ELLIPSE> ELLIPSEList { get; set; }

    public void Set(LAYER layer)
    {
        Layer = layer;
        gameObject.name = "Layer_" + layer.C2;

        LayerMaterial = new Material(GoDefaultMat);
        if (ACI.ContainsKey(layer.C62))
        {
            LayerMaterial.color = ACI[layer.C62];

            if (layer.C62 == 7 || layer.C62 == 8) ZoomAdjust = 0.6f;
        }
        else
        {
            LayerMaterial.color = Color.black;
            ZoomAdjust = 0.8f;//默认颜色的层线段宽度做限制

        }


    }

    public void Load(DXFStructure dxf)
    {
        //找到当前层的物体
        LINEList = dxf.ENTITIES.LINEList.Where(x => x.C8 == Layer.C2).ToList();
        LWPOLYLINEList = dxf.ENTITIES.LWPOLYLINEList.Where(x => x.C8 == Layer.C2).ToList();
        TEXTList = dxf.ENTITIES.TEXTList.Where(x => x.C8 == Layer.C2).ToList();
        CIRCLEList = dxf.ENTITIES.CIRCLEList.Where(x => x.C8 == Layer.C2).ToList();
        ARCList = dxf.ENTITIES.ARCList.Where(x => x.C8 == Layer.C2).ToList();
        INSERTList = dxf.ENTITIES.INSERTList.Where(x => x.C8 == Layer.C2).ToList();
        ELLIPSEList = dxf.ENTITIES.ELLIPSEList.Where(x => x.C8 == Layer.C2).ToList();


        //绘制层下属物体
        DrawLINEList(dxf, LINEList);
        DrawLWPOLYLINEList(dxf, LWPOLYLINEList);
        DrawTEXTList(dxf, TEXTList);
        DrawCIRCLEList(dxf, CIRCLEList);
        DrawARCList(dxf, ARCList);
        DrawINSERTList(dxf, INSERTList);
        DrawELLIPSEList(dxf, ELLIPSEList);
    }

    public static GoLayer DefaultLayer;

    static Dictionary<int, Color> ACI = new Dictionary<int, Color>()
    {
         {1,new Color(1f,0f,0f) },
         {2,new Color(1f,1f,0f) },
         {3,new Color(0f,1f,0f) },
         {4,new Color(0f,1f,1f) },
         {5,new Color(0f,0f,1f) },
         {6,new Color(1f,0f,1f) },
         {7,new Color(0f,0f,0f) },
         {8,new Color(0f,0f,0f) },
         {9,new Color(0f,0f,0f) },
         {10,new Color(1f,0f,0f) },
         {11,new Color(1f,0.5f,0.5f) },
         {12,new Color(0.65f,0f,0f) },
         {13,new Color(0.65f,0.325f,0.325f) },
         {14,new Color(0.5f,0f,0f) },
         {15,new Color(0.5f,0.25f,0.25f) },
         {16,new Color(0.3f,0f,0f) },
         {17,new Color(0.3f,0.15f,0.15f) },
         {18,new Color(0.15f,0f,0f) },
         {19,new Color(0.15f,0.075f,0.075f) },
         {20,new Color(1f,0.25f,0f) },
         {21,new Color(1f,0.625f,0.5f) },
         {22,new Color(0.65f,0.1625f,0f) },
         {23,new Color(0.65f,0.4063f,0.325f) },
         {24,new Color(0.5f,0.125f,0f) },
         {25,new Color(0.5f,0.3125f,0.25f) },
         {26,new Color(0.3f,0.075f,0f) },
         {27,new Color(0.3f,0.1875f,0.15f) },
         {28,new Color(0.15f,0.0375f,0f) },
         {29,new Color(0.15f,0.0938f,0.075f) },
         {30,new Color(1f,0.5f,0f) },
         {31,new Color(1f,0.75f,0.5f) },
         {32,new Color(0.65f,0.325f,0f) },
         {33,new Color(0.65f,0.4875f,0.325f) },
         {34,new Color(0.5f,0.25f,0f) },
         {35,new Color(0.5f,0.375f,0.25f) },
         {36,new Color(0.3f,0.15f,0f) },
         {37,new Color(0.3f,0.225f,0.15f) },
         {38,new Color(0.15f,0.075f,0f) },
         {39,new Color(0.15f,0.1125f,0.075f) },
         {40,new Color(1f,0.75f,0f) },
         {41,new Color(1f,0.875f,0.5f) },
         {42,new Color(0.65f,0.4875f,0f) },
         {43,new Color(0.65f,0.5688f,0.325f) },
         {44,new Color(0.5f,0.375f,0f) },
         {45,new Color(0.5f,0.4375f,0.25f) },
         {46,new Color(0.3f,0.225f,0f) },
         {47,new Color(0.3f,0.2625f,0.15f) },
         {48,new Color(0.15f,0.1125f,0f) },
         {49,new Color(0.15f,0.1313f,0.075f) },
         {50,new Color(1f,1f,0f) },
         {51,new Color(1f,1f,0.5f) },
         {52,new Color(0.65f,0.65f,0f) },
         {53,new Color(0.65f,0.65f,0.325f) },
         {54,new Color(0.5f,0.5f,0f) },
         {55,new Color(0.5f,0.5f,0.25f) },
         {56,new Color(0.3f,0.3f,0f) },
         {57,new Color(0.3f,0.3f,0.15f) },
         {58,new Color(0.15f,0.15f,0f) },
         {59,new Color(0.15f,0.15f,0.075f) },
         {60,new Color(0.75f,1f,0f) },
         {61,new Color(0.875f,1f,0.5f) },
         {62,new Color(0.4875f,0.65f,0f) },
         {63,new Color(0.5688f,0.65f,0.325f) },
         {64,new Color(0.375f,0.5f,0f) },
         {65,new Color(0.4375f,0.5f,0.25f) },
         {66,new Color(0.225f,0.3f,0f) },
         {67,new Color(0.2625f,0.3f,0.15f) },
         {68,new Color(0.1125f,0.15f,0f) },
         {69,new Color(0.1313f,0.15f,0.075f) },
         {70,new Color(0.5f,1f,0f) },
         {71,new Color(0.75f,1f,0.5f) },
         {72,new Color(0.325f,0.65f,0f) },
         {73,new Color(0.4875f,0.65f,0.325f) },
         {74,new Color(0.25f,0.5f,0f) },
         {75,new Color(0.375f,0.5f,0.25f) },
         {76,new Color(0.15f,0.3f,0f) },
         {77,new Color(0.225f,0.3f,0.15f) },
         {78,new Color(0.075f,0.15f,0f) },
         {79,new Color(0.1125f,0.15f,0.075f) },
         {80,new Color(0.25f,1f,0f) },
         {81,new Color(0.625f,1f,0.5f) },
         {82,new Color(0.1625f,0.65f,0f) },
         {83,new Color(0.4063f,0.65f,0.325f) },
         {84,new Color(0.125f,0.5f,0f) },
         {85,new Color(0.3125f,0.5f,0.25f) },
         {86,new Color(0.075f,0.3f,0f) },
         {87,new Color(0.1875f,0.3f,0.15f) },
         {88,new Color(0.0375f,0.15f,0f) },
         {89,new Color(0.0938f,0.15f,0.075f) },
         {90,new Color(0f,1f,0f) },
         {91,new Color(0.5f,1f,0.5f) },
         {92,new Color(0f,0.65f,0f) },
         {93,new Color(0.325f,0.65f,0.325f) },
         {94,new Color(0f,0.5f,0f) },
         {95,new Color(0.25f,0.5f,0.25f) },
         {96,new Color(0f,0.3f,0f) },
         {97,new Color(0.15f,0.3f,0.15f) },
         {98,new Color(0f,0.15f,0f) },
         {99,new Color(0.075f,0.15f,0.075f) },
         {100,new Color(0f,1f,0.25f) },
         {101,new Color(0.5f,1f,0.625f) },
         {102,new Color(0f,0.65f,0.1625f) },
         {103,new Color(0.325f,0.65f,0.4063f) },
         {104,new Color(0f,0.5f,0.125f) },
         {105,new Color(0.25f,0.5f,0.3125f) },
         {106,new Color(0f,0.3f,0.075f) },
         {107,new Color(0.15f,0.3f,0.1875f) },
         {108,new Color(0f,0.15f,0.0375f) },
         {109,new Color(0.075f,0.15f,0.0938f) },
         {110,new Color(0f,1f,0.5f) },
         {111,new Color(0.5f,1f,0.75f) },
         {112,new Color(0f,0.65f,0.325f) },
         {113,new Color(0.325f,0.65f,0.4875f) },
         {114,new Color(0f,0.5f,0.25f) },
         {115,new Color(0.25f,0.5f,0.375f) },
         {116,new Color(0f,0.3f,0.15f) },
         {117,new Color(0.15f,0.3f,0.225f) },
         {118,new Color(0f,0.15f,0.075f) },
         {119,new Color(0.075f,0.15f,0.1125f) },
         {120,new Color(0f,1f,0.75f) },
         {121,new Color(0.5f,1f,0.875f) },
         {122,new Color(0f,0.65f,0.4875f) },
         {123,new Color(0.325f,0.65f,0.5688f) },
         {124,new Color(0f,0.5f,0.375f) },
         {125,new Color(0.25f,0.5f,0.4375f) },
         {126,new Color(0f,0.3f,0.225f) },
         {127,new Color(0.15f,0.3f,0.2625f) },
         {128,new Color(0f,0.15f,0.1125f) },
         {129,new Color(0.075f,0.15f,0.1313f) },
         {130,new Color(0f,1f,1f) },
         {131,new Color(0.5f,1f,1f) },
         {132,new Color(0f,0.65f,0.65f) },
         {133,new Color(0.325f,0.65f,0.65f) },
         {134,new Color(0f,0.5f,0.5f) },
         {135,new Color(0.25f,0.5f,0.5f) },
         {136,new Color(0f,0.3f,0.3f) },
         {137,new Color(0.15f,0.3f,0.3f) },
         {138,new Color(0f,0.15f,0.15f) },
         {139,new Color(0.075f,0.15f,0.15f) },
         {140,new Color(0f,0.75f,1f) },
         {141,new Color(0.5f,0.875f,1f) },
         {142,new Color(0f,0.4875f,0.65f) },
         {143,new Color(0.325f,0.5688f,0.65f) },
         {144,new Color(0f,0.375f,0.5f) },
         {145,new Color(0.25f,0.4375f,0.5f) },
         {146,new Color(0f,0.225f,0.3f) },
         {147,new Color(0.15f,0.2625f,0.3f) },
         {148,new Color(0f,0.1125f,0.15f) },
         {149,new Color(0.075f,0.1313f,0.15f) },
         {150,new Color(0f,0.5f,1f) },
         {151,new Color(0.5f,0.75f,1f) },
         {152,new Color(0f,0.325f,0.65f) },
         {153,new Color(0.325f,0.4875f,0.65f) },
         {154,new Color(0f,0.25f,0.5f) },
         {155,new Color(0.25f,0.375f,0.5f) },
         {156,new Color(0f,0.15f,0.3f) },
         {157,new Color(0.15f,0.225f,0.3f) },
         {158,new Color(0f,0.075f,0.15f) },
         {159,new Color(0.075f,0.1125f,0.15f) },
         {160,new Color(0f,0.25f,1f) },
         {161,new Color(0.5f,0.625f,1f) },
         {162,new Color(0f,0.1625f,0.65f) },
         {163,new Color(0.325f,0.4063f,0.65f) },
         {164,new Color(0f,0.125f,0.5f) },
         {165,new Color(0.25f,0.3125f,0.5f) },
         {166,new Color(0f,0.075f,0.3f) },
         {167,new Color(0.15f,0.1875f,0.3f) },
         {168,new Color(0f,0.0375f,0.15f) },
         {169,new Color(0.075f,0.0938f,0.15f) },
         {170,new Color(0f,0f,1f) },
         {171,new Color(0.5f,0.5f,1f) },
         {172,new Color(0f,0f,0.65f) },
         {173,new Color(0.325f,0.325f,0.65f) },
         {174,new Color(0f,0f,0.5f) },
         {175,new Color(0.25f,0.25f,0.5f) },
         {176,new Color(0f,0f,0.3f) },
         {177,new Color(0.15f,0.15f,0.3f) },
         {178,new Color(0f,0f,0.15f) },
         {179,new Color(0.075f,0.075f,0.15f) },
         {180,new Color(0.25f,0f,1f) },
         {181,new Color(0.625f,0.5f,1f) },
         {182,new Color(0.1625f,0f,0.65f) },
         {183,new Color(0.4063f,0.325f,0.65f) },
         {184,new Color(0.125f,0f,0.5f) },
         {185,new Color(0.3125f,0.25f,0.5f) },
         {186,new Color(0.075f,0f,0.3f) },
         {187,new Color(0.1875f,0.15f,0.3f) },
         {188,new Color(0.0375f,0f,0.15f) },
         {189,new Color(0.0938f,0.075f,0.15f) },
         {190,new Color(0.5f,0f,1f) },
         {191,new Color(0.75f,0.5f,1f) },
         {192,new Color(0.325f,0f,0.65f) },
         {193,new Color(0.4875f,0.325f,0.65f) },
         {194,new Color(0.25f,0f,0.5f) },
         {195,new Color(0.375f,0.25f,0.5f) },
         {196,new Color(0.15f,0f,0.3f) },
         {197,new Color(0.225f,0.15f,0.3f) },
         {198,new Color(0.075f,0f,0.15f) },
         {199,new Color(0.1125f,0.075f,0.15f) },
         {200,new Color(0.75f,0f,1f) },
         {201,new Color(0.875f,0.5f,1f) },
         {202,new Color(0.4875f,0f,0.65f) },
         {203,new Color(0.5688f,0.325f,0.65f) },
         {204,new Color(0.375f,0f,0.5f) },
         {205,new Color(0.4375f,0.25f,0.5f) },
         {206,new Color(0.225f,0f,0.3f) },
         {207,new Color(0.2625f,0.15f,0.3f) },
         {208,new Color(0.1125f,0f,0.15f) },
         {209,new Color(0.1313f,0.075f,0.15f) },
         {210,new Color(1f,0f,1f) },
         {211,new Color(1f,0.5f,1f) },
         {212,new Color(0.65f,0f,0.65f) },
         {213,new Color(0.65f,0.325f,0.65f) },
         {214,new Color(0.5f,0f,0.5f) },
         {215,new Color(0.5f,0.25f,0.5f) },
         {216,new Color(0.3f,0f,0.3f) },
         {217,new Color(0.3f,0.15f,0.3f) },
         {218,new Color(0.15f,0f,0.15f) },
         {219,new Color(0.15f,0.075f,0.15f) },
         {220,new Color(1f,0f,0.75f) },
         {221,new Color(1f,0.5f,0.875f) },
         {222,new Color(0.65f,0f,0.4875f) },
         {223,new Color(0.65f,0.325f,0.5688f) },
         {224,new Color(0.5f,0f,0.375f) },
         {225,new Color(0.5f,0.25f,0.4375f) },
         {226,new Color(0.3f,0f,0.225f) },
         {227,new Color(0.3f,0.15f,0.2625f) },
         {228,new Color(0.15f,0f,0.1125f) },
         {229,new Color(0.15f,0.075f,0.1313f) },
         {230,new Color(1f,0f,0.5f) },
         {231,new Color(1f,0.5f,0.75f) },
         {232,new Color(0.65f,0f,0.325f) },
         {233,new Color(0.65f,0.325f,0.4875f) },
         {234,new Color(0.5f,0f,0.25f) },
         {235,new Color(0.5f,0.25f,0.375f) },
         {236,new Color(0.3f,0f,0.15f) },
         {237,new Color(0.3f,0.15f,0.225f) },
         {238,new Color(0.15f,0f,0.075f) },
         {239,new Color(0.15f,0.075f,0.1125f) },
         {240,new Color(1f,0f,0.25f) },
         {241,new Color(1f,0.5f,0.625f) },
         {242,new Color(0.65f,0f,0.1625f) },
         {243,new Color(0.65f,0.325f,0.4063f) },
         {244,new Color(0.5f,0f,0.125f) },
         {245,new Color(0.5f,0.25f,0.3125f) },
         {246,new Color(0.3f,0f,0.075f) },
         {247,new Color(0.3f,0.15f,0.1875f) },
         {248,new Color(0.15f,0f,0.0375f) },
         {249,new Color(0.15f,0.075f,0.0938f) },
         {250,new Color(0.33f,0.33f,0.33f) },
         {251,new Color(0.464f,0.464f,0.464f) },
         {252,new Color(0.598f,0.598f,0.598f) },
         {253,new Color(0.732f,0.732f,0.732f) },
         {254,new Color(0.866f,0.866f,0.866f) },
         {255,new Color(1f,1f,1f) },

    };

}
