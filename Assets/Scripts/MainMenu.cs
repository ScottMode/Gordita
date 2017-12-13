using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using System.Collections.Generic;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
	#region Fields
	//Assigned
	public SearchListPanel searchPanel;
	public GameObject mainPanel;
	public NetworkManager networkMan;

	public GameObject downPanel;
	public GameObject winPanel;

	//Matchmaking
	List<MatchInfoSnapshot> matchList = new List<MatchInfoSnapshot>();
	bool matchCreated;
	NetworkMatch networkMatch;
	MatchInfo currentMatch;
	int matchCount;
	#endregion

	void Awake()
	{
		networkMatch = gameObject.AddComponent<NetworkMatch>();

        ListRooms();
	}

	public void OnMatchList(bool success, string extendedInfo, List<MatchInfoSnapshot> matches)
	{
		if (success && matches != null)
		{
			if (matches.Count > 0) {
				searchPanel.Setup (matches);
			} else {
				Debug.LogWarning ("No matches found");
			}
		}
		else
		{
			Debug.LogError("List match failed: " + extendedInfo);
		}

        ListRooms();
	}

	public void OnMatchJoined(bool success, string extendedInfo, MatchInfo matchInfo)
	{
		if (success)
		{
			Debug.Log("Join match succeeded");
			if (matchCreated)
			{
				Debug.LogWarning("Match already set up, aborting...");
				return;
			}
			Utility.SetAccessTokenForNetwork(matchInfo.networkId, matchInfo.accessToken);
			NetworkClient myClient = new NetworkClient();
			myClient.RegisterHandler(MsgType.Connect, OnConnected);
			myClient.Connect(matchInfo);
			currentMatch = matchInfo;
		}
		else
		{
			Debug.LogError("Join match failed " + extendedInfo);
		}
	}

	public void OnConnected(NetworkMessage msg)
	{
		Debug.Log("Connected!");

		if (GameManager.Instance.isMonster)
		{
			GameManager.Instance.SetupMonster ();
		}
		else
		{
			GameManager.Instance.SpawnPlayer ();
		}

		networkMan.StartClient(currentMatch);
	}

	public void JoinGame(MatchInfoSnapshot info)
	{
		networkMatch.JoinMatch(info.networkId, "", "", "", 0, 0, OnMatchJoined);
	}







	public void OnMatchCreate(bool success, string extendedInfo, MatchInfo matchInfo)
	{
		if (success)
		{
			Debug.Log("Create match succeeded");
			matchCreated = true;
			NetworkServer.Listen(matchInfo, 9000);
			Utility.SetAccessTokenForNetwork(matchInfo.networkId, matchInfo.accessToken);
			networkMan.StartHost(matchInfo);

			//Hide menu 
			mainPanel.SetActive(false);

			if (GameManager.Instance.isMonster)
			{
				GameManager.Instance.SetupMonster ();
			}
			else
			{
				GameManager.Instance.SpawnPlayer ();
			}
		}
		else
		{
			Debug.LogError("Create match failed: " + extendedInfo);
		}
	}

	#region Button Functions
	public void CreateRoom()
	{
		string matchName = string.Format("Maze_{0}", matchCount);
		uint matchSize = 4;
		bool matchAdvertise = true;
		string matchPassword = "";

		networkMatch.CreateMatch(matchName, matchSize, matchAdvertise, matchPassword, "", "", 0, 0, OnMatchCreate);
	}

	public void ListRooms()
	{
		networkMatch.ListMatches (0, 20, "", true, 0, 0, OnMatchList);
	}
	#endregion

	public void ShowWinPanel()
	{
		winPanel.SetActive(true);
	}

	public void ShowDownPanel(bool isDown)
	{
		downPanel.SetActive (isDown);
	}
}