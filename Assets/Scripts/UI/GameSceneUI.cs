using UnityEngine;
using System.Collections;

public class GameSceneUI : MonoBehaviour
{
	public void OnResetClick()
	{
		ScavengerHuntArea scavHunt = GameObject.Find("ScavengerHuntPanel").GetComponent<ScavengerHuntArea>();

		if(GameObject.Find("bonusTimer") != null)
		{
			BonusTimer bt = GameObject.Find("bonusTimer").GetComponent<BonusTimer>();
			bt.timeInSeconds = 0;
		}

		scavHunt.populateScavHunt(40);
	}
}

