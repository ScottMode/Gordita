using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class MazeFinishTrigger : MonoBehaviour {

	public void MoveToFinish(BaseEventData data)
	{
		InputManager.Instance.TeleportToClickPosition (data);
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "Player")
		{
			//Call method to add to list of finished players
			Debug.LogError("id is: " + col.gameObject.GetComponent<NetworkPlayer>().playerControllerId);
			GameManager.Instance.PlayerFinished (col.gameObject.GetComponent<NetworkPlayer> ());
		}
	}
}
