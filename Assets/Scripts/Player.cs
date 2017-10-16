using UnityEngine;
using System.Collections;

/// <summary>
/// Created by Nelson Scott. 10.08.17
/// </summary>
public class Player : MonoBehaviour
{
	#region Fields
	//Assigned
	public float warpTime;

	//private
	private Vector3 startPosition;
	private Vector3 endPosition;
	private bool isMoving = false;
	#endregion

	#region Properties
	public bool IsMoving { get { return isMoving; } }
	#endregion

	public void Setup(Vector3 position, Quaternion lookAt)
	{
		transform.position = GameManager.Instance.spawnPoint.position;
		transform.rotation = lookAt;
	}

	/// <summary>
	/// <Moves player to position specified>
	/// </summary>
	/// <param name="tile">Tile.</param>
	public void MoveToPosition(Vector3 pos)
	{
		if(isMoving)
		{
			return;
		}

		isMoving = true;

		StartCoroutine(LerpToPosition(new Vector3(pos.x, 0, pos.z)));
	}
	IEnumerator LerpToPosition(Vector3 pos)
	{
		float timer = 0;
		startPosition = transform.position;
		pos.y = startPosition.y;

		do
		{
			timer += Time.deltaTime;

			transform.position = Vector3.Lerp(startPosition, pos, timer / warpTime);

			if (timer < warpTime)
				yield return null;
			else
				break;
		} while (true);

		isMoving = false;
	}
}