using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityStandardAssets.Characters.FirstPerson;

/// <summary>
/// Created by Nelson Scott. 10.08.17
/// </summary>
public class NetworkPlayer : NetworkBehaviour
{
	#region Fields
	//Assigned
	public Transform playerTransform;
	//public SpriteRenderer sprite;
	public float yOffset;
	public GameObject cam;
	public RigidbodyFirstPersonController controller;
	#endregion

	#region Properties
	//Empty
	#endregion

	void Start()
	{
		//Set transform using get tag if player isn't local network
		if(!isLocalPlayer)
		{
			cam.SetActive (false);
			//playerTransform = GameObject.FindGameObjectWithTag("Player").transform.GetChild(2);

			//transform.position = GameManager.Instance.spawnPoint.transform.position;
		}
		else
		{
			cam.SetActive (true);
			GetComponent<NetworkIdentity> ().localPlayerAuthority = true;
		}
		transform.position = GameManager.Instance.spawnPoint.transform.position;
	}

	/// <summary>
	/// Sets up local player
	/// </summary>
	public override void OnStartLocalPlayer()
	{
		/*GameObject p = GameObject.FindGameObjectWithTag("Player");

		transform.SetParent(p.transform);

		transform.Translate(0, yOffset, 0);

		//sprite.enabled = false;
*/
		base.OnStartLocalPlayer();
	}

	void Update()
	{
		//Checks to see if local network player, else shows icon
		/*if(!isLocalPlayer)
		{
			transform.position = new Vector3 (transform.position.x, 0, transform.position.z);
			if(transform.position == playerTransform.position)
			{
				//sprite.enabled = false;
			}
			else
			{
				//sprite.enabled = true;

				transform.LookAt(playerTransform, Vector3.up);

				transform.position = new Vector3 (transform.position.x, 0, transform.position.z);
			}
		}*/
	}

	public void DownPlayer()
	{
		if (!GameManager.Instance.isMonster){
			GameManager.Instance.menu.ShowDownPanel (true);
			controller.enabled = false;
		}
	}

	public void RevivePlayer()
	{
		GameManager.Instance.menu.ShowDownPanel (false);
		controller.enabled = true;
	}

	public void Won()
	{
		controller.enabled = false;
	}

	void OnCollisionEnter(Collision col)
	{
		if (col.collider.gameObject.tag == "Player")
		{
			RevivePlayer ();
		}
	}
}