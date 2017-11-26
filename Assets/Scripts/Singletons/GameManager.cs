using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine.Events;

public enum GameState {
	Menus,
	Lobby,
	Countdown,
	Gameplay,
	FinishedMaze
}

public class GameManager : Singleton<GameManager> 
{
	#region Fields
	//Assigned
	public Player player;
	public Transform spawnPoint;
	public bool isMonster;
	public Monster monster;

	//private
	private Dictionary<string, UnityEvent> eventDictionary = new Dictionary<string, UnityEvent>();
	private GameState currentState = GameState.Menus;

	//Constants
	public const string e_PLAYER_FINISHED_MOVE = "PLAYER_FINISHED_MOVE";
	public const string e_PLAYER_DOWN = "PLAYER_DOWN";
	public const string e_PLAYER_FINISHED_MAZE = "PLAYER_FINISHED_MAZE";
	#endregion

	#region Properties
	//private
	public Dictionary<string, UnityEvent> EventDictionary = new Dictionary<string, UnityEvent>();
	public GameState CurrentState { get { return currentState; } }
	#endregion

	protected GameManager(){}

	void Awake()
	{
		eventDictionary = new Dictionary<string, UnityEvent> ();
		eventDictionary.Add (e_PLAYER_DOWN, new UnityEvent ());
		eventDictionary.Add (e_PLAYER_FINISHED_MOVE, new UnityEvent ());
	}

	#region Game Setup
	public void SpawnPlayer() 
	{
		player.transform.position = spawnPoint.position;
	}

	public void SetupMonster()
	{
		Debug.LogError ("Setting up the mosnter");
		monster.canAttack = true;
	}
	#endregion

	#region Game Events
	public void PlayerFinished(string id) 
	{
		CheckAllPlayersFinished ();
	}
	private void CheckAllPlayersFinished()
	{
		
	}
	#endregion
}