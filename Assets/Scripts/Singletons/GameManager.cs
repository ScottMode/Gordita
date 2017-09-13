using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager> 
{
	#region Fields
	//Assigned

	//private
	private Dictionary<string, UnityEvent> eventDictionary = new Dictionary<string, UnityEvent>();

	//Consts
	public const string e_PLAYER_FINISHED_MOVE = "PLAYER_FINISHED_MOVE";
	public const string e_PLAYER_DOWN = "PLAYER_DOWN";
	#endregion

	#region Properties
	public Dictionary<string, UnityEvent> EventDictionary = new Dictionary<string, UnityEvent>();
	#endregion

	protected GameManager(){}

	void Awake()
	{
		eventDictionary = new Dictionary<string, UnityEvent> ();
		eventDictionary.Add (e_PLAYER_DOWN, new UnityEvent ());
		eventDictionary.Add (e_PLAYER_FINISHED_MOVE, new UnityEvent ());
	}

	//Add player
	public void AddPlayer()
	{
		
	}

	//Add listener
	public void AddListenerToEvent()
	{
		
	}
}