using UnityEditor;
using UnityEngine;

/// <summary>
/// カメラマン
/// </summary>
public class Cameraman : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// カメラ作成
    /// </summary>
    /// <param name="name"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <param name="depth"></param>
    [MenuItem("Tool/I am cameraman/Create Main Camera")]
    public static void CreateCamera()
    {
        GameObject go = new GameObject("Main Camera");

        {
            Transform t = go.GetComponent<Transform>();
            t.position = new Vector3( 0f, 1.0f, -10.0f);
        }

        go.AddComponent<Camera>();

        {
            Camera c = go.GetComponent<Camera>();
            c.depth = -1;
        }

        go.AddComponent<GUILayer>();
        go.AddComponent<FlareLayer>();
        go.AddComponent<AudioListener>();

    }

}
