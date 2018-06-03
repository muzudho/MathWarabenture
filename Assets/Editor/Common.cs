using UnityEditor;
using UnityEngine;

/// <summary>
/// 共通部品
/// </summary>
public class Common : MonoBehaviour {


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// スクリーンショット
    /// 
    /// 参考Webサイト
    ///     2015年02月11日「Unityエディタ上からGameビューのスクリーンショットを撮るEditor拡張」@dj_kusuha
    ///     https://qiita.com/dj_kusuha/items/13a68474edfd78e41b82
    /// </summary>
    [MenuItem("Tool/Screen Shot (Game View)")]
    static void CaptureScreenshot()
    {
        // ファイル名
        var path = string.Format("{0}unity.png", System.DateTime.Now.ToString("yyyyMMdd_HHmmss"));
        Application.CaptureScreenshot(path);

        // Gameビューを再描画
        var type = typeof(UnityEditor.EditorWindow).Assembly.GetType("UnityEditor.GameView");
        EditorWindow.GetWindow(type).Repaint();

        Debug.Log("ScreenShot: " + path);
    }

    /// <summary>
    /// ぜーんぶ、消す☆（＾～＾）
    /// </summary>
    [MenuItem("Tool/Clear Unity")]
    public static void Clear()
    {
        ResourceManager.Clear();
        GameObjectCreator.Clear();
    }


}
