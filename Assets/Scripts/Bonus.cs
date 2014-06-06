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
	//char[] _operators = new [] { '+', '-', 'x', '/', '√', '^' };

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
			bonusLabel.text = "";
			selectedBonus = "";
			AudioSource audio = bt.gameObject.audio;
			
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
//		List<string> bonusOperators = new List<string>()
//		{
//			//"++",
//			//"+-",
////			"+*",
////			"+/",
////			"+^",
////			"+√",
////			"--",
////			"-*",
////			"-/",
////			"-^",
////			"-√",
////			"**",
////			"//",
////			"*/",
//			"x",
//			"/",
//			"^",
//			"√"
//		};

		List<string> operatorsInScavHunt = new List<string>();

		foreach(GameObject go in GameObject.FindGameObjectsWithTag("ScavHuntElement"))
        {
			UILabel l = go.GetComponent<UILabel>();

			if(l.text == "+" || l.text == "-" || l.text == "x" 
			   || l.text == "^" || l.text == "/" || l.text == "+" || l.text == "√")
				operatorsInScavHunt.Add(l.text);
		}

		List<KeyValuePair<string, double>> bonuses = new List<KeyValuePair<string, double>>()
		{
			new KeyValuePair<string, double>("time", 0.1),
			new KeyValuePair<string, double>("score", 0.1),
			new KeyValuePair<string, double>("operators", 0.90)
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

			bt.enabled = true;
			bt.timeInSeconds = 31;
			UILabel bonusTimeLabel = bt.GetComponent<UILabel>();
			bonusTimeLabel.enabled = true;

			isTimeBonus = true;

			AudioSource audio = bt.gameObject.audio;
			
			audio.Play();
			bt.bonusOver = false;
		}
		else if(selectedElement == "score")
		{
			NGUITools.SetActive(scoreBonusPrefab, true);

			bt.enabled = true;
			bt.timeInSeconds = 31;
			UILabel bonusTimeLabel = bt.GetComponent<UILabel>();
			bonusTimeLabel.enabled = true;

			isScoreBonus = true;
			bt.bonusOver = false;

			AudioSource audio = bt.gameObject.audio;
			
			audio.Play();
		}
		else if(selectedElement == "operators")
		{
			bt.enabled = true;
			bt.timeInSeconds = 31;
			UILabel bonusTimeLabel = bt.GetComponent<UILabel>();
			bonusTimeLabel.enabled = true;

			selectedBonus = operatorsInScavHunt[rand.Next(0, operatorsInScavHunt.Count)];
			bonusLabel.text = selectedBonus;

			AudioSource audio = bt.gameObject.audio;
			
			audio.Play();

			bt.bonusOver = false;
		}
	}
}

