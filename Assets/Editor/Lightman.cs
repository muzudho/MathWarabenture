using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// 
/// </summary>
public class Lightman : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// ライト作成
    /// </summary>
    /// <param name="name"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <param name="depth"></param>
    [MenuItem("Tool/I am lightman/Create Light")]
    public static void CreateCamera()
    {
        GameObject go = new GameObject("Directional light aaaa");

        go.AddComponent<Light>();

        {
            Transform t = go.GetComponent<Transform>();

            t.position = new Vector3(0f, 3.0f, 0f);
            t.rotation = Quaternion.Euler(50.0f, -30.0f, 0f);
        }

        {
            Light light = go.GetComponent<Light>();
            light.type = LightType.Directional;

            // type
            light.shadows = LightShadows.Soft;

        }
    }

}
