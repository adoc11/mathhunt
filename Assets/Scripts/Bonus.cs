using UnityEngine;
using System.Collections;

public class Bonus : MonoBehaviour
{
	public GameObject bonusTimer;
	public int multiplier;
	public bool isTimeBonus = false;
	public bool isScoreBonus = false;
	BonusTimer bt;
	Timer t;
	// Use this for initialization
	void Start ()
	{
		bt = GameObject.Find("bonusTimer").GetComponent<BonusTimer>();
		t = GameObject.Find("Timer").GetComponent<Timer>();
	}

	// Update is called once per frame
	void Update ()
	{
		if(bt.bonusOver)
		{
			isTimeBonus = false;
			NGUITools.SetActive(bonusTimer, false);
			Destroy(this.gameObject);
		}
	}

	public void applyBonus(string bonusType)
	{
		if(bonusType == "TimeBonus")
		{
			UILabel bonusTimeLabel = bt.GetComponent<UILabel>();
			bonusTimeLabel.enabled = true;
			bt.enabled = true;
			isTimeBonus = true;
			bt.timeInSeconds = 31;
			NGUITools.SetActive(bonusTimer, true);
		}
		else if(bonusType == "ScoreMultiplier")
		{
			isScoreBonus = true;
		}
	}
}

