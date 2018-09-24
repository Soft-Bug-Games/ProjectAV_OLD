using UnityEngine;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour
{
	public static PlayerManager instance {get; private set;}

	private Dictionary<string, Player> mPlayerList;

	public static Dictionary<string, Player> PlayerList()
	{
		return instance.mPlayerList;
	}
	public static Player GetPlayer(string ID)
	{
		return instance.mPlayerList[ID];
	}
	public static Player LocalPlayer()
	{
		foreach(KeyValuePair<string, Player> player in instance.mPlayerList)
		{
			if(player.Value.Layer() == LayerMask.NameToLayer("Local"))
			{
				return player.Value;
			}
		}

		if(GameManager.instance.debugMode)
		{
			Debug.LogError("[PlayerManager.cs] No local player found!");
		}
		return null;
	}
	public static void AddPlayer(string ID, Player player)
	{
		instance.mPlayerList.Add(ID, player);
	}
	public static void AssignPlayerList(Dictionary<string, Player> playerList)
	{
		instance.mPlayerList = playerList;
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
	void Update()
	{
		if(GameManager.instance.debugMode)
		{

		}
	}
}