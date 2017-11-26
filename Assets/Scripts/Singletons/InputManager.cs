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
		if (GameManager.Instance.isMonster && Input.GetMouseButtonDown(0))
		{
			Vector3 mousePosition = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 0f);
			Vector3 attackPosition = Camera.main.ScreenToWorldPoint (mousePosition);
			attackPosition = new Vector3 (attackPosition.x, 0f, attackPosition.y);
			GameManager.Instance.monster.Attack (attackPosition);
		}
	}

	/// <summary>
	/// Moves the player to the clicked point on a clickable surface. 
	/// </summary>
	/// <param name="data">Data.</param>
	public void TeleportToClickPosition(BaseEventData data)
	{
		PointerEventData pointerData = data as PointerEventData;
		Vector3 worldPosition = pointerData.pointerCurrentRaycast.worldPosition;

		player.MoveToPosition (worldPosition);
	}
}