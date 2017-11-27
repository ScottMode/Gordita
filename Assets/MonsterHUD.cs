using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MonsterHUD : MonoBehaviour 
{

	public Slider slider;

	void Update()
	{
		slider.value = 1.00f - (GameManager.Instance.monster.attackTimer / GameManager.Instance.monster.attackMaxTime);
	}
}
