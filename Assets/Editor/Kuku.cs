//
//  [ライセンス]
//
//      Copyright (c) 2018 Muzudho
//      Released under the MIT license
//      https://opensource.org/licenses/mit-license.php
//
//      会社が倒産したり、開発者が死んだりして　サポートできなくなるプログラムばかりになるのを防ぐものだから、
//      殆どのライセンスは　使ってもらおう、という利権なんだぜ☆（＾～＾）
//      著作権とは考え方が　ひっくり返っているんだぜ☆（＾～＾）
//  
//      参考Webサイト
//          「MITライセンスってなに？どうやって使うの？商用でも無料で使えるの？」倉田 幸暢
//          https://wisdommingle.com/mit-license/
//
//  [使い方]
//
//      Unity の Projectビューで Assets/Editor フォルダーを作って、その中にこの
//      Menu.cs ファイルを入れろだぜ☆（＾～＾）
//      そうすれば　メニューに Tool 項目が出てくるぜ☆（＾～＾）

using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.Text;


/// <summary>
/// 参考Webサイト
/// 
///     キューブ作成
///         2013年08月13日「Getting a primitive mesh without creating a new GameObject」unity
///         https://answers.unity.com/questions/514293/changing-a-gameobjects-primitive-mesh.html
///         
///     キューブ着色
///         2017.07.16「ステップ８　Unity（ユニティ） のMaterial（マテリアル）を使ってみよう」ハジプロ！
///         https://hajipro.com/unity/unity-material
/// </summary>
public class Menu : MonoBehaviour {

    // Use this for initialization
    void Start () {
    }

    // Update is called once per frame
    void Update () {
		
	}

    /// <summary>
    /// 消す☆（＾～＾）
    /// </summary>
    [MenuItem("Tool/Kuku/Clear")]
    static void Clear()
    {
        GameObject cubesObj = GameObject.Find("Cubes");
        if (null != cubesObj)
        {
            // 既存なら消す☆（＾～＾）
            GameObjectCreator.DestroyGameObject(cubesObj);
            Debug.Log("既存のCubesオブジェクトは消したぜ☆（＾～＾）");
        }
    }

    /// <summary>
    /// 九九オブジェクト作成
    /// 
    /// キューブのプレファブを先に作成しておくこと。
    /// </summary>
    [MenuItem("Tool/Kuku/Create 9x9")]
    static void CreateKuku()
    {
        ResourceManager.CreateMaterials();

        for (int z = 1; z < 11; z++)
        {
            for (int x = 1; x < 11; x++)
            {
                float height = x * z;
                GameObjectCreator.CreateCube(string.Format("キューブ{0}x{1}", x, z), x, height/2, z, 1.0f, height, 1.0f);
            }
        }
    }

    /// <summary>
    /// 赤く塗るぜ☆（＾～＾）
    /// </summary>
    [MenuItem("Tool/Kuku/Paint/Case A")]
    static void PaintCaseA()
    {
        /*
        // 例
        string[] names = new string[]
        {
            "Cubes/キューブ2x6",
            "Cubes/キューブ2x7",
            "Cubes/キューブ2x8",
            "Cubes/キューブ2x9",
            "Cubes/キューブ3x7",
            "Cubes/キューブ5x5",
            "Cubes/キューブ7x4",
            "Cubes/キューブ8x4",
            "Cubes/キューブ9x4",
            "Cubes/キューブ10x4",
        };
        */
        //*
        // 例　平方数
        string[] names = new string[]
        {
            "Cubes/キューブ1x1",
            "Cubes/キューブ2x2",
            "Cubes/キューブ3x3",
            "Cubes/キューブ4x4",
            "Cubes/キューブ5x5",
            "Cubes/キューブ6x6",
            "Cubes/キューブ7x7",
            "Cubes/キューブ8x8",
            "Cubes/キューブ9x9",
            "Cubes/キューブ10x10",
        };
        // */
        /*
        // 例　上の方の平方数の近く
        string[] names = new string[]
        {
            "Cubes/キューブ6x5",
            "Cubes/キューブ6x6",
            "Cubes/キューブ7x6",
            "Cubes/キューブ7x7",
            "Cubes/キューブ8x7",
            "Cubes/キューブ8x8",
            "Cubes/キューブ9x8",
            "Cubes/キューブ9x9",
            "Cubes/キューブ10x9",
            "Cubes/キューブ10x10",
        };
        */
        /*
        // 例　素数
        string[] names = new string[]
        {
            "Cubes/キューブ1x2",
            "Cubes/キューブ1x3",
            "Cubes/キューブ1x5",
            "Cubes/キューブ1x7",
            "Cubes/キューブ2x1",
            "Cubes/キューブ3x1",
            "Cubes/キューブ5x1",
            "Cubes/キューブ7x1",
        };
        */

        string namePath = string.Format("{0}/{1}", ResourceManager.nd_materials, ResourceManager.nd_red_name);
        Material mtl = Resources.Load<Material>(namePath);
        Debug.Assert(null != mtl, string.Format("path={0} Redマテリアル作ってないだろ☆（＾～＾）", namePath));

        foreach (string name in names)
        {
            GameObjectCreator.PaintGameObject(name, mtl);
        }
    }

    static string[] GetCubeNames(string request)
    {
        List<string> names = new List<string>();

        switch (request)
        {
            case "1x1cube":
                names.Add(string.Format("Cubes/キューブ{0}x{1}", 1, 1));
                break;
            case "2x2cubes":
                for (int z = 1; z < 3; z++)
                {
                    for (int x = 1; x < 3; x++)
                    {
                        names.Add(string.Format("Cubes/キューブ{0}x{1}", x, z));
                    }
                }
                break;
            case "3x3cubes":
                for (int z = 1; z < 4; z++)
                {
                    for (int x = 1; x < 4; x++)
                    {
                        names.Add(string.Format("Cubes/キューブ{0}x{1}", x, z));
                    }
                }
                break;
            case "4x4cubes":
                for (int z = 1; z < 5; z++)
                {
                    for (int x = 1; x < 5; x++)
                    {
                        names.Add(string.Format("Cubes/キューブ{0}x{1}", x, z));
                    }
                }
                break;
            case "5x5cubes":
                for (int z = 1; z < 6; z++)
                {
                    for (int x = 1; x < 6; x++)
                    {
                        names.Add(string.Format("Cubes/キューブ{0}x{1}", x, z));
                    }
                }
                break;
            case "6x6cubes":
                for (int z = 1; z < 7; z++)
                {
                    for (int x = 1; x < 7; x++)
                    {
                        names.Add(string.Format("Cubes/キューブ{0}x{1}", x, z));
                    }
                }
                break;
            case "7x7cubes":
                for (int z = 1; z < 8; z++)
                {
                    for (int x = 1; x < 8; x++)
                    {
                        names.Add(string.Format("Cubes/キューブ{0}x{1}", x, z));
                    }
                }
                break;
            case "8x8cubes":
                for (int z = 1; z < 9; z++)
                {
                    for (int x = 1; x < 9; x++)
                    {
                        names.Add(string.Format("Cubes/キューブ{0}x{1}", x, z));
                    }
                }
                break;
            case "9x9cubes":
                for (int z = 1; z < 10; z++)
                {
                    for (int x = 1; x < 10; x++)
                    {
                        names.Add(string.Format("Cubes/キューブ{0}x{1}", x, z));
                    }
                }
                break;
            case "all":
                for (int z = 1; z < 11; z++)
                {
                    for (int x = 1; x < 11; x++)
                    {
                        names.Add(string.Format("Cubes/キューブ{0}x{1}", x, z));
                    }
                }
                break;
            case "left_top_wing":
                for (int z = 1; z < 11; z++)
                {
                    for (int x = 1; x < z; x++)
                    {
                        names.Add(string.Format("Cubes/キューブ{0}x{1}", x, z));
                    }
                }
                break;
            case "right_bottom_wing":
                for (int z = 1; z < 11; z++)
                {
                    for (int x = (z+1); x < 11; x++)
                    {
                        names.Add(string.Format("Cubes/キューブ{0}x{1}", x, z));
                    }
                }
                break;
            case "square":
                for (int n = 1; n < 11; n++)
                {
                    names.Add(string.Format("Cubes/キューブ{0}x{1}", n, n));
                }
                break;
            default:
                Debug.Log(string.Format("未定義のキューブ団体名☆（＾～＾） {0}", request));
                break;
        }

        // デバッグ
        {
            StringBuilder sb = new StringBuilder();
            foreach (string name in names)
            {
                sb.Append(string.Format("{0}, ",name));
            }
            Debug.Log(sb.ToString());
        }


        return names.ToArray();
    }

    /// <summary>
    /// ２色で塗るぜ☆（＾～＾）
    /// </summary>
    [MenuItem("Tool/Kuku/Paint/Case B")]
    static void PaintCaseB()
    {
        // 赤色
        string[] reds = new string[]
        {
            "Cubes/キューブ1x2",
            "Cubes/キューブ1x3",
            "Cubes/キューブ2x3",
        };
        // 青色
        string[] blues = new string[]
        {
            "Cubes/キューブ2x1",
            "Cubes/キューブ3x1",
            "Cubes/キューブ3x2",
        };

        Material redMtl = ResourceManager.GetMaterial(ResourceManager.nd_red_name);
        Material blueMtl = ResourceManager.GetMaterial(ResourceManager.nd_blue_name);

        foreach (string cubeName in reds)
        {
            GameObjectCreator.PaintGameObject(cubeName, redMtl);
        }

        foreach (string cubeName in blues)
        {
            GameObjectCreator.PaintGameObject(cubeName, blueMtl);
        }
    }

    /// <summary>
    /// 対角線を塗るぜ☆（＾～＾）
    /// </summary>
    [MenuItem("Tool/Kuku/Paint/Diagonal (Square Number)")]
    static void PaintDiagonal()
    {
        Material mtl = ResourceManager.GetMaterial(ResourceManager.nd_red_name);

        string[] names = GetCubeNames("square");
        foreach (string name in names)
        {
            GameObjectCreator.PaintGameObject(name, mtl);
        }
    }

    /// <summary>
    /// 全てのキューブを漂白するぜ☆（＾～＾）
    /// </summary>
    [MenuItem("Tool/Kuku/Paint/All White")]
    static void PaintAllWhite()
    {
        Material mtl = ResourceManager.GetMaterial(ResourceManager.nd_white_name);

        string[] names = GetCubeNames("all");
        foreach (string name in names)
        {
            GameObjectCreator.PaintGameObject(name, mtl);
        }

    }

    /// <summary>
    /// 対角線の左上を赤く塗るぜ☆（＾～＾）
    /// </summary>
    [MenuItem("Tool/Kuku/Paint/Left Top Wing Red")]
    static void PaintLeftTopWingRed()
    {
        Material mtl = ResourceManager.GetMaterial(ResourceManager.nd_red_name);

        string[] names = GetCubeNames("left_top_wing");
        foreach (string name in names)
        {
            GameObjectCreator.PaintGameObject(name, mtl);
        }
    }

    /// <summary>
    /// 対角線の右下を青く塗るぜ☆（＾～＾）
    /// </summary>
    [MenuItem("Tool/Kuku/Paint/Right Bottom Wing Blue")]
    static void PaintRightBottomWingBlue()
    {
        Material mtl = ResourceManager.GetMaterial(ResourceManager.nd_blue_name);

        string[] names = GetCubeNames("right_bottom_wing");
        foreach (string name in names)
        {
            GameObjectCreator.PaintGameObject(name, mtl);
        }
    }


    /// <summary>
    /// キューブの高さを全部１にするぜ☆（＾～＾）
    /// </summary>
    [MenuItem("Tool/Kuku/Magic/Set Height 1")]
    static void SetHeight1()
    {
        float height = 1.0f;

        string[] names = GetCubeNames("all");
        foreach (string name in names)
        {
            GameObject obj = GameObject.Find(name);
            if (null != obj)
            {
                obj.transform.position = new Vector3(
                    obj.transform.position.x,
                    height / 2,
                    obj.transform.position.z);
                obj.transform.localScale = new Vector3(
                    obj.transform.localScale.x,
                    height,
                    obj.transform.localScale.z
                    );
            }
        }

    }


    /// <summary>
    /// キューブの横幅、奥行を全部 0.9 にするぜ☆（＾～＾）
    /// </summary>
    [MenuItem("Tool/Kuku/Magic/Set width,depth 0.9")]
    static void SetWidthDepth09()
    {
        float width = 0.9f;
        float depth = 0.9f;

        for (int z = 1; z < 11; z++)
        {
            for (int x = 1; x < 11; x++)
            {
                GameObject obj = GameObject.Find(string.Format("Cubes/キューブ{0}x{1}", x, z));
                if (null != obj)
                {
                    obj.transform.localScale = new Vector3(
                        width,
                        obj.transform.localScale.y,
                        depth
                        );
                }
            }
        }

    }

    /// <summary>
    /// すべてのキューブを隠すぜ☆（＾～＾）
    /// 
    /// 参考Webサイト
    ///     2012年5月15日「Unity: GameObjectの非表示２つの方法と削除」ハマケン100%開発
    ///     http://hamken100.blogspot.jp/2012/05/unity-gameobject.html
    /// </summary>
    [MenuItem("Tool/Kuku/Magic/Set Invisible All Cubes")]
    static void SetInvisibleAllCubes()
    {
        string[] names = GetCubeNames("all");
        GameObjectCreator.SetVisibleAllGameObjects(names, true);
    }

    /// <summary>
    /// 1x1のキューブを表示するぜ☆（＾～＾）
    /// </summary>
    [MenuItem("Tool/Kuku/Magic/Set Visible 1x1 Cube")]
    static void SetVisible1x1Cube()
    {
        string[] names = GetCubeNames("1x1cube");
        GameObjectCreator.SetVisibleAllGameObjects(names, true);
    }

    /// <summary>
    /// 2x2のキューブを表示するぜ☆（＾～＾）
    /// </summary>
    [MenuItem("Tool/Kuku/Magic/Set Visible 2x2 Cubes")]
    static void SetVisible2x2Cubes()
    {
        string[] names = GetCubeNames("2x2cubes");
        GameObjectCreator.SetVisibleAllGameObjects(names, true);
    }

    /// <summary>
    /// 3x3のキューブを表示するぜ☆（＾～＾）
    /// </summary>
    [MenuItem("Tool/Kuku/Magic/Set Visible 3x3 Cubes")]
    static void SetVisible3x3Cubes()
    {
        string[] names = GetCubeNames("3x3cubes");
        GameObjectCreator.SetVisibleAllGameObjects(names, true);
    }

    /// <summary>
    /// 4x4のキューブを表示するぜ☆（＾～＾）
    /// </summary>
    [MenuItem("Tool/Kuku/Magic/Set Visible 4x4 Cubes")]
    static void SetVisible4x4Cubes()
    {
        string[] names = GetCubeNames("4x4cubes");
        GameObjectCreator.SetVisibleAllGameObjects(names, true);
    }

    /// <summary>
    /// 5x5のキューブを表示するぜ☆（＾～＾）
    /// </summary>
    [MenuItem("Tool/Kuku/Magic/Set Visible 5x5 Cubes")]
    static void SetVisible5x5Cubes()
    {
        string[] names = GetCubeNames("5x5cubes");
        GameObjectCreator.SetVisibleAllGameObjects(names, true);
    }

    /// <summary>
    /// 6x6のキューブを表示するぜ☆（＾～＾）
    /// </summary>
    [MenuItem("Tool/Kuku/Magic/Set Visible 6x6 Cubes")]
    static void SetVisible6x6Cubes()
    {
        string[] names = GetCubeNames("6x6cubes");
        GameObjectCreator.SetVisibleAllGameObjects(names, true);
    }

    /// <summary>
    /// 7x7のキューブを表示するぜ☆（＾～＾）
    /// </summary>
    [MenuItem("Tool/Kuku/Magic/Set Visible 7x7 Cubes")]
    static void SetVisible7x7Cubes()
    {
        string[] names = GetCubeNames("7x7cubes");
        GameObjectCreator.SetVisibleAllGameObjects(names, true);
    }

    /// <summary>
    /// 8x8のキューブを表示するぜ☆（＾～＾）
    /// </summary>
    [MenuItem("Tool/Kuku/Magic/Set Visible 8x8 Cubes")]
    static void SetVisible8x8Cubes()
    {
        string[] names = GetCubeNames("8x8cubes");
        GameObjectCreator.SetVisibleAllGameObjects(names, true);
    }

    /// <summary>
    /// 9x9のキューブを表示するぜ☆（＾～＾）
    /// </summary>
    [MenuItem("Tool/Kuku/Magic/Set Visible 9x9 Cubes")]
    static void SetVisible9x9Cubes()
    {
        string[] names = GetCubeNames("9x9cubes");
        GameObjectCreator.SetVisibleAllGameObjects(names, true);
    }

    /// <summary>
    /// すべてのキューブを表示するぜ☆（＾～＾）
    /// </summary>
    [MenuItem("Tool/Kuku/Magic/Set Visible All Cubes")]
    static void SetVisibleAllCubes()
    {
        string[] names = GetCubeNames("all");
        GameObjectCreator.SetVisibleAllGameObjects(names,true);
    }



    /// <summary>
    /// タワーをスライスするぜ☆（＾～＾）
    /// </summary>
    [MenuItem("Tool/Kuku/Magic/Slice Tower By Z")]
    static void SliceTowerByZ()
    {

        for (int z = 1; z < 11; z++)
        {
            for (int x = 1; x < 11; x++)
            {
                GameObject obj = GameObject.Find(string.Format("Cubes/キューブ{0}x{1}", x, z));
                if (null != obj)
                {
                    obj.transform.position = new Vector3(
                        obj.transform.position.x + 10 * (z - 1),
                        obj.transform.position.y, obj.transform.position.z);
                }
            }
        }

    }

    /// <summary>
    /// 平行光線でトップビューにするぜ☆（＾～＾）
    /// 
    /// 参考Webサイト
    ///     20150531「UnityでRotation（Quaternion）をうまく使いたい」お米 is ライス
    ///     http://spi8823.hatenablog.com/entry/2015/05/31/025903
    /// </summary>
    [MenuItem("Tool/Kuku/Camera/Top View (Orthographic)")]
    static void SetCameraTopView()
    {
        GameObject obj = GameObject.Find("Main Camera");
        if (null != obj)
        {
            obj.transform.position = new Vector3(
                5.5f,
                110f,
                5.5f
                );
            obj.transform.rotation = Quaternion.Euler(
                90f,
                0f,
                0f
                );
            obj.transform.localScale = new Vector3(1f, 1f, 1f);
            obj.GetComponent<Camera>().orthographic = true;
        }
    }

}
