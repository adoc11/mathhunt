using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bonus : MonoBehaviour
{
	public GameObject bonusTimer;
	public GameObject timeBonusPrefab;
	public GameObject scoreBonusPrefab;

	public int multiplier;
	public bool isTimeBonus = false;
	public bool isScoreBonus = false;
	public UILabel bonusLabel;
	public string selectedBonus;

	BonusTimer bt;
	Timer t;
	System.Random rand = new System.Random();

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
			//NGUITools.SetActive(bonusTimer, false);
			NGUITools.SetActive(timeBonusPrefab, false);
			NGUITools.SetActive(scoreBonusPrefab, false);
			//bonusLabel.text = "";
			//Destroy(this.gameObject);
			AudioSource audio = timeBonusPrefab.audio;
			
			audio.Stop();
		}
	}

	public void pickRandomBonus()
	{
		bonusLabel.text = "";
		NGUITools.SetActive(timeBonusPrefab, false);
		NGUITools.SetActive(scoreBonusPrefab, false);

		bt.enabled = false;
		bt.bonusOver = false;
		List<string> bonusOperators = new List<string>()
		{
			//"++",
			//"+-",
//			"+*",
//			"+/",
//			"+^",
//			"+√",
//			"--",
//			"-*",
//			"-/",
//			"-^",
//			"-√",
//			"**",
//			"//",
//			"*/",
			"x",
			"/",
			"^",
			"√"
		};

		List<KeyValuePair<string, double>> bonuses = new List<KeyValuePair<string, double>>()
		{
			new KeyValuePair<string, double>("time", 0.05),
			new KeyValuePair<string, double>("score", 0.05),
			new KeyValuePair<string, double>("operators", 0.95)
		};
		
		double randBonusProb = rand.NextDouble();
		double cumulative = 0.0;
		
		string selectedElement = "";
		
		for (int j = 0; j < bonuses.Count; j++)
		{
			cumulative += bonuses[j].Value;
			if (randBonusProb < cumulative)
			{
				selectedElement = bonuses[j].Key;
				break;
			}
		}

		if(selectedElement == "time")
		{
			NGUITools.SetActive(timeBonusPrefab, true);

			AudioSource audio = timeBonusPrefab.audio;

			audio.Play();

			bt.enabled = true;
			bt.timeInSeconds = 31;
			UILabel bonusTimeLabel = bt.GetComponent<UILabel>();
			bonusTimeLabel.enabled = true;

			isTimeBonus = true;
		}
		else if(selectedElement == "score")
		{
			NGUITools.SetActive(scoreBonusPrefab, true);

			isScoreBonus = true;
			bt.bonusOver = false;
		}
		else if(selectedElement == "operators")
		{
			selectedBonus = bonusOperators[rand.Next(0, bonuses.Count + 1)];
			bonusLabel.text = selectedBonus;
		}
	}
}

