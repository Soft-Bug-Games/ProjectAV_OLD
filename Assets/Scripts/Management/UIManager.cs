using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	public static UIManager instance {get; private set;}

	private Canvas mHUD;
	private Canvas mInGameMenu;

	public static Canvas HUD()
	{
		return instance.mHUD;
	}
	public static Canvas InGameMenu()
	{
		return instance.mInGameMenu;
	}
	public static void AssignHUD(Canvas canvas)
	{
		instance.mHUD = canvas;
	}
	public static void AssignInGameMenu(Canvas canvas)
	{
		instance.mInGameMenu = canvas;
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
	}
}