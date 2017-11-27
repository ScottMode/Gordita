using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

/// <summary>
/// Created by Nelson Scott. 10.08.17
/// </summary>
public class NetworkMonster : NetworkBehaviour
{
	#region Fields
	//Assigned
	public Transform playerTransform;
	public SpriteRenderer sprite;
	public float yOffset;
	#endregion

	#region Properties
	//Empty
	#endregion

	void Start()
	{
		//Set transform using get tag if player isn't local network
		if(!isLocalPlayer)
		{
			playerTransform = GameObject.FindGameObjectWithTag("Player").transform.GetChild(2);

			transform.position = GameManager.Instance.spawnPoint.transform.position;
		}
	}

	/// <summary>
	/// Sets up local player
	/// </summary>
	public override void OnStartLocalPlayer()
	{
		GameObject p = GameObject.FindGameObjectWithTag("Player");

		transform.SetParent(p.transform);

		transform.Translate(0, yOffset, 0);

		sprite.enabled = false;

		base.OnStartLocalPlayer();
	}

	void Update()
	{
		//Checks to see if local network player, else shows icon
		if(!isLocalPlayer && playerTransform != null)
		{
			if(transform.position == playerTransform.position)
			{
				sprite.enabled = false;
			}
			else
			{
				sprite.enabled = true;

				transform.LookAt(playerTransform, Vector3.up);

				transform.position = new Vector3 (transform.position.x, 0, transform.position.z);
			}
		}
	}
}