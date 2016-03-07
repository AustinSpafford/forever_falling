#if UNITY_EDITOR

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[InitializeOnLoad]
public class OnUnityLoad
{
	static OnUnityLoad ()
	{
		EditorApplication.playmodeStateChanged = OnPlaymodeStateChanged;
	}

	private static void OnPlaymodeStateChanged ()
	{
		// If the user _just_ pressed the Play button.
		if (EditorApplication.isPlayingOrWillChangePlaymode &&
			!EditorApplication.isPlaying)
		{
			var activeScene = EditorSceneManager.GetActiveScene();

			if (activeScene.isDirty)
			{
				Debug.Log("Auto-Saving scene before entering Play mode: " + activeScene.name);

				EditorSceneManager.SaveScene(activeScene);
			}

			EditorApplication.SaveAssets();
		}
	}
}

#endif // #if UNITY_EDITOR
