using UnityEditor;
using UnityEngine;

/// <summary>
/// リソースを作ったり削除したりする人。
/// </summary>
public class ResourceManager : MonoBehaviour {

    #region 共通ディレクトリ、共通ファイル

    // ********************************************************************************
    // * 共通ディレクトリ、共通ファイル                                                      *
    // ********************************************************************************

    // Windows のファイルシステムを操作すると、Unityの反映にタイムラグが出るので、
    // Unity に用意されている方法でファイル操作すること。

    // Unityプロジェクト・ディレクトリー
    //  |
    //  +---- Assets/
    //  |       |
    //  |       +---- Resources/
    //  |               |
    //  |               +---- Prefabs/
    //  |               |       |
    //  |               |       +---- Cube.prefab
    //  |               |
    //  |               +---- Materials/
    //  |                       |
    //  |                       +---- Red.mat
    //  |                       |
    //  |                       +---- Green.mat
    //  |                       |
    //  |                       +---- Blue.mat
    //  |
    //  +---- 20180527_164553unity.png ※スクリーンショットを撮ると増えていく

    /// <summary>
    /// パスのノード
    /// </summary>
    public const string nd_assets = "Assets";
    public const string nd_resources = "Resources";
    public const string nd_prefabs = "Prefabs";
    public const string nd_materials = "Materials";

    public const string nd_cube_name = "Cube";
    public const string nd_cube_file = "Cube.prefab";
    public const string nd_red_name = "Red";
    public const string nd_green_name = "Green";
    public const string nd_blue_name = "Blue";
    public const string nd_white_name = "White";

    #endregion

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// リソース・ディレクトリ用意
    /// </summary>
    public static void ReadyDirectory()
    {
        // Assets/Resources
        {
            string path = string.Format("{0}/{1}", nd_assets, nd_resources);
            if (!AssetDatabase.IsValidFolder(path))
            {
                AssetDatabase.CreateFolder(nd_assets, nd_resources);
                Debug.Log("リソースを入れる " + path + " を作ったぜ☆（＾～＾）");
            }
        }
        // Assets/Resources/Prefabs
        {
            string path = string.Format("{0}/{1}/{2}", nd_assets, nd_resources, nd_prefabs);
            if (!AssetDatabase.IsValidFolder(path))
            {
                AssetDatabase.CreateFolder(string.Format("{0}/{1}", nd_assets, nd_resources), nd_prefabs);
                Debug.Log("プレファブを入れる " + path + " を作ったぜ☆（＾～＾）");
            }
        }
        // Assets/Resources/Materials
        {
            string path = string.Format("{0}/{1}/{2}", nd_assets, nd_resources, nd_materials);
            if (!AssetDatabase.IsValidFolder(path))
            {
                AssetDatabase.CreateFolder(string.Format("{0}/{1}", nd_assets, nd_resources), nd_materials);
                Debug.Log("マテリアルを入れる " + path + " を作ったぜ☆（＾～＾）");
            }
        }
    }

    /// <summary>
    /// １つのマテリアル作成
    /// </summary>
    public static void CreateColorMaterial(string matName, Color color)
    {
        string namePath = string.Format("{0}/{1}", ResourceManager.nd_materials, matName);
        Material mtl = Resources.Load<Material>(namePath);
        if (null != mtl)
        {
            // もうある☆（＾～＾）
            Debug.Log(string.Format("{0} のマテリアルはもうある☆（＾～＾）", namePath));
            return;
        }
        Debug.Log(string.Format("{0} のマテリアルが無いんで、作るぜ☆（＾～＾）", namePath));

        // 基となるマテリアルを拾ってきて、少し変えて、新規作成
        Material newMtl = new Material(Shader.Find("Standard"));
        newMtl.name = matName;
        newMtl.SetColor("_Color", color);
        string filePath = string.Format("{0}/{1}/{2}/{3}.mat", ResourceManager.nd_assets, ResourceManager.nd_resources, ResourceManager.nd_materials, matName);
        AssetDatabase.CreateAsset(newMtl, filePath);
    }

    /// <summary>
    /// 複数のマテリアル作成
    /// </summary>
    [MenuItem("Tool/I am resource manager/Create Materials")]
    public static void CreateMaterials()
    {
        ResourceManager.ReadyDirectory();

        // 赤
        CreateColorMaterial(nd_red_name, Color.red);
        // 緑
        CreateColorMaterial(nd_green_name, Color.green);
        // 青
        CreateColorMaterial(nd_blue_name, Color.blue);
        // 白
        CreateColorMaterial(nd_white_name, Color.white);
    }

    /// <summary>
    /// キューブのプレファブ作成
    /// </summary>
    [MenuItem("Tool/I am resource manager/Create Prefub Cube")]
    public static void CreatePrefubCube()
    {
        ResourceManager.ReadyDirectory();

        string prefabCubeName = string.Format("{0}/{1}", ResourceManager.nd_prefabs, ResourceManager.nd_cube_name);
        GameObject prefabCube = (GameObject)Resources.Load(prefabCubeName);
        if (null != prefabCube)
        {
            Debug.Log(string.Format("キューブのプレファブはもうある☆（＾～＾） path={0}", prefabCubeName));
            return;
        }
        Debug.Log(string.Format("キューブのプレファブが無いんで、作るぜ☆（＾～＾） path={0}", prefabCubeName));


        // ********************************************************************************
        // * プレファブ作成                                                               *
        // ********************************************************************************
        // ヒエラルキーには見えない一時ゲームオブジェクトを作る（このオブジェクトはあとで破棄する）
        GameObject expectedTmpObj = EditorUtility.CreateGameObjectWithHideFlags("Expected Cube", HideFlags.HideInHierarchy,
                    typeof(UnityEngine.MeshFilter),
                    typeof(UnityEngine.BoxCollider),
                    typeof(UnityEngine.MeshRenderer)
                    );


        // Unityデフォルトのキューブを作成
        GameObject defaultCube = GameObject.CreatePrimitive(UnityEngine.PrimitiveType.Cube);


        // Cube (メッシュ・フィルター)
        {
            // デフォルトのキューブから、メッシュをもらう
            expectedTmpObj.GetComponent<UnityEngine.MeshFilter>().mesh = defaultCube.GetComponent<MeshFilter>().sharedMesh;
        }

        // メッシュ・レンダラー
        {
            // デフォルトのキューブのメッシュ・レンダラーから、マテリアルをもらう
            expectedTmpObj.GetComponent<MeshRenderer>().material = defaultCube.GetComponent<MeshRenderer>().sharedMaterial;
        }


        // キューブのプレファブを作成する
        PrefabUtility.CreatePrefab(string.Format("{0}/{1}/{2}/{3}", ResourceManager.nd_assets, ResourceManager.nd_resources, ResourceManager.nd_prefabs, ResourceManager.nd_cube_file), expectedTmpObj);

        // Unityデフォルトのキューブを削除
        GameObjectCreator.DestroyGameObject(defaultCube);

        // プレファブの元は破棄する
        GameObjectCreator.DestroyGameObject(expectedTmpObj);
    }

    /// <summary>
    /// リソースを消す☆（＾～＾）
    /// </summary>
    [MenuItem("Tool/I am resource manager/Clear")]
    public static void Clear()
    {
        {
            string path = string.Format("{0}/{1}", ResourceManager.nd_assets, ResourceManager.nd_resources);
            if (AssetDatabase.IsValidFolder(path))
            {
                AssetDatabase.MoveAssetToTrash(path);
                Debug.Log("リソースを入れる " + path + " をゴミ箱に入れたぜ☆（＾～＾）");
            }
        }
        {
            string path = string.Format("{0}/{1}", ResourceManager.nd_assets, ResourceManager.nd_materials);
            if (AssetDatabase.IsValidFolder(path))
            {
                AssetDatabase.MoveAssetToTrash(path);
                Debug.Log("マテリアルを入れる " + path + " をゴミ箱に入れたぜ☆（＾～＾）");
            }
        }
    }

    public static Material GetMaterial(string matName)
    {
        string namePath = string.Format("{0}/{1}", ResourceManager.nd_materials, matName);
        Material mtl = Resources.Load<Material>(namePath);
        Debug.Assert(null != mtl, string.Format("{0} マテリアル作ってないだろ☆（＾～＾）", namePath));
        return mtl;
    }

}
