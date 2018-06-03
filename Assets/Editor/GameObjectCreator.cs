using UnityEditor;
using UnityEngine;

/// <summary>
/// ゲーム・オブジェクトを作る人。
/// </summary>
public class GameObjectCreator : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// ゲームオブジェクトの削除
    /// </summary>
    /// <param name="o"></param>
    public static void DestroyGameObject(GameObject o)
    {
        if (Application.isEditor) // エディターのメニューから実行した場合などこっち。
        {
            DestroyImmediate(o);
        }
        else // ゲーム中ならこっち。
        {
            Destroy(o); // エディター画面でこれを呼ぶと、フリーズする
        }
    }

    /// <summary>
    /// 消す☆（＾～＾）
    /// </summary>
    [MenuItem("Tool/I am game object creator/Clear")]
    public static void Clear()
    {
        var allObjects = FindObjectsOfType<GameObject>();
        foreach (var go in allObjects)
        {
            GameObjectCreator.DestroyGameObject(go);
        }

        /*
        // ヒエラルキー・ウィンドウに出てこない隠れているオブジェクトも全部消すぜ☆（＾～＾）
        // 参考Webサイト
        //      2014年01月11日「How do I remove a Game Object that is not visible in the hierarchy?」unity
        //      https://answers.unity.com/questions/613728/how-do-i-remove-a-game-object-that-is-not-visible.html

        var allObjects = FindObjectsOfType<GameObject>();
        foreach (var go in allObjects)
        {
            if ((go.hideFlags & HideFlags.HideInHierarchy) != 0)
            {
                GameObjectCreator.DestroyGameObject(go);
            }
        }
        */
    }

    /// <summary>
    /// キューブ作成
    /// 
    /// キューブのプレファブを先に作成しておくこと。
    /// </summary>
    /// <param name="name"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <param name="depth"></param>
    [MenuItem("Tool/I am game object creator/Create Prefub Cube")]
    public static void CreateCube(string name, float x, float y, float z, float width, float height, float depth)
    {
        ResourceManager.CreatePrefubCube();

        // ********************************************************************************
        // * キューブの入れ物                                                             *
        // ********************************************************************************
        GameObject cubesObj = GameObject.Find("Cubes");
        if (null == cubesObj)
        {
            // ヒエラルキーへの配置もされる
            cubesObj = new GameObject("Cubes");
            Debug.Log("Cubesオブジェクトを作ったぜ☆（＾～＾）");
        }


        // ********************************************************************************
        // * プレファブのインスタンスを、ヒエラルキーのCubesにぶら下げる                  *
        // ********************************************************************************
        // プレハブを取得
        string prefabCubeName = string.Format("{0}/{1}", ResourceManager.nd_prefabs, ResourceManager.nd_cube_name);
        GameObject prefabCube = (GameObject)Resources.Load(prefabCubeName);
        Debug.Assert(null != prefabCube, string.Format("name={0} プレファブ作ってないだろ☆（＾～＾）", prefabCubeName));


        // プレハブからインスタンスを生成
        GameObject cube = UnityEngine.Object.Instantiate(prefabCube, new Vector3(x, y, z), Quaternion.identity);
        cube.name = name;
        cube.transform.parent = cubesObj.transform;
        cube.transform.localScale = new Vector3(width, height, depth);
    }

    /// <summary>
    /// ゲーム・オブジェクトに色を塗るぜ☆（＾～＾）
    /// </summary>
    /// <param name="gameObjectName"></param>
    /// <param name="mtl"></param>
    public static void PaintGameObject(string gameObjectName, Material mtl)
    {
        GameObject obj = GameObject.Find(gameObjectName);
        if (null != obj)
        {
            obj.GetComponent<MeshRenderer>().material = mtl;
        }
    }

    /// <summary>
    /// すべてのゲーム・オブジェクトの表示／非表示を設定するぜ☆（＾～＾）
    /// </summary>
    public static void SetVisibleAllGameObjects(string[] names, bool value)
    {
        foreach (string name in names)
        {
            GameObject obj = GameObject.Find(name);
            if (null != obj)
            {
                obj.GetComponent<MeshRenderer>().enabled = value;
            }
        }
    }

}
