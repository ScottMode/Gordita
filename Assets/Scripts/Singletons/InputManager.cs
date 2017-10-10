using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class InputManager : Singleton<InputManager> 
{
	#region Fields
	//Assigned
	public Player player;
	#endregion

	protected InputManager(){}
	
	void Awake () 
	{
		
	}

	void Update()
	{
		
	}

	/// <summary>
	/// Moves the player to the clicked point on a clickable surface. 
	/// </summary>
	/// <param name="data">Data.</param>
	public void TeleportToClickPosition(BaseEventData data)
	{
		Debug.Log ("here");

		PointerEventData pointerData = data as PointerEventData;
		Vector3 worldPosition = pointerData.pointerCurrentRaycast.worldPosition;

		player.MoveToPosition (worldPosition);
	}
}