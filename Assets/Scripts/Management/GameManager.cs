using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager instance {get; private set;}

	public bool debugMode;
	private bool mIsPaused;

	public static bool DebugMode()
	{
		return instance.debugMode;
	}
	public static bool IsPaused()
	{
		return instance.mIsPaused;
	}
	public static void Paused(bool flag)
	{
		instance.mIsPaused = flag;
	}

	public void CloseApplication()
	{
		#if UNITY_EDITOR
			EditorApplication.isPlaying = false;
		#endif

		Application.Quit();
	}
	public void LeaveGame()
	{

	}
	public void ToggleDebugMode()
	{
		debugMode = !debugMode;
	}

	void Awake()
	{
		if(instance == null)
		{
			DontDestroyOnLoad(gameObject);
			instance = this;
		}
		else if(instance != this)
		{
			DestroyImmediate(gameObject);
			return;
		}

		#if !UNITY_EDITOR
			instance.debugMode = false;
		#endif
	}
	void Update()
	{
		if(debugMode)
		{
			Debug.Log("Debug mode enabled!");
		}
	}
}
