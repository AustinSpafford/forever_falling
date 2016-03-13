using UnityEngine;
using System.Collections;

public class FullScreenRenderTexture : MonoBehaviour
{
	public bool DebugEnabled = false;

	public void Awake()
	{
		hostCamera = GetComponent<Camera>();
	}

	public void Start()
	{
		MatchRenderTextureToScreenSize();
	}
	
	public void Update()
	{
		MatchRenderTextureToScreenSize();
	}

	private Camera hostCamera = null;

	private void MatchRenderTextureToScreenSize()
	{
		int desiredWidth = Screen.width;
		int desiredHeight = Screen.height;

		if ((hostCamera.targetTexture == null) ||
			(hostCamera.targetTexture.width != desiredWidth) ||
			(hostCamera.targetTexture.height != desiredHeight))
		{
			var oldRenderTexture = hostCamera.targetTexture;
			var newRenderTexture = new RenderTexture(desiredWidth, desiredHeight, 24);

			if (DebugEnabled)
			{
				Debug.LogFormat(
					"Allocated new RenderTexture, old=({0}, {1}) and new=({2}, {3}).",
					((oldRenderTexture != null) ? oldRenderTexture.width : 0),
					((oldRenderTexture != null) ? oldRenderTexture.height : 0),
					newRenderTexture.width,
					newRenderTexture.height);
			}

			hostCamera.targetTexture = newRenderTexture;

			if (oldRenderTexture != null)
			{
				oldRenderTexture.Release();
				oldRenderTexture = null;
			}
		}
	}
}
