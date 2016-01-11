#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.Collections;

public class RootSceneLauncher : Editor
{
    #region Inspector Methods
    [MenuItem("JEngine/Play &p")]
    public static void RootSceneLaunch()
    {
        if (EditorApplication.isPlaying == true)
        {
            EditorApplication.isPlaying = false;
            return;
        }

		EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo ();
		EditorSceneManager.OpenScene("Assets/JEngine/Scenes/RootScene.unity");
        EditorApplication.isPlaying = true;
    }
    #endregion
}
#endif
