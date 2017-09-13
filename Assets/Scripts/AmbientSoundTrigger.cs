using UnityEngine;
using System.Collections;

public class AmbientSoundTrigger : MonoBehaviour 
{
	public GvrAudioSource audioSource;

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "Player")
		{
			audioSource.Play ();
		}
	}
}
