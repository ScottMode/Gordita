using UnityEngine;
using System.Collections;

public class Monster : MonoBehaviour 
{
	#region Fields
	public bool isAttacking;
	float attackTimer;
	public float attackMaxTime;

	public bool canAttack;
	public float attackRechargeTimer;
	public float attackRechargeMaxTime;
	#endregion

	void Start()
	{
		gameObject.SetActive (false);
		canAttack = false;
	}

	void Update()
	{
		if (isAttacking && attackTimer > 0f)
		{
			attackTimer -= Time.deltaTime;
			if (attackTimer <= 0f)
			{
				FinishAttack ();
				attackTimer = 0f;
			}
		}

		//Recharge
		if (!canAttack && attackRechargeTimer > 0f)
		{
			attackRechargeTimer -= Time.deltaTime;
			if (attackRechargeTimer <= 0f)
			{
				canAttack = true;
				attackRechargeTimer = 0f;
			}
		}
	}

	public void Attack(Vector3 position)
	{
		if (canAttack) 
		{
			Debug.LogError ("Attack!: " + position);

			gameObject.SetActive (true);
			isAttacking = true;
			transform.position = position;
			attackTimer = attackMaxTime;

			canAttack = false;
		}
	}

	void FinishAttack()
	{
		gameObject.SetActive (false);
		isAttacking = false;

		attackRechargeTimer = attackRechargeMaxTime;
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "Player")
		{
			FinishAttack ();

			//Down player
		}
	}
}
