using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
//using NCalc;
//using System.Random;

public class GameController : MonoBehaviour
{
	public bool gameOver;
	public bool paused;
	public GameObject SymbolPrefab;
	public UILabel ScoreLabel;
	public int numSHSymbols = 40;
	public GameObject startingSlot;
	public bool equationSolved = false;
	public GameObject PausePanel;
	public GameObject addToTimerMessage;

	public int score { get; set; }

	List<GameObject> slots;
	string _equation; 
	Vector3 messagePos;
	char[] _operators = new [] { '+', '-', 'x', '/', '&', '^' }; 
	bool runDisplayTimeMessage = false;
	int tempScore = 0;
	int delay = 0;
	int delayMod = 0;

	System.Random rand = new System.Random();

	ScavengerHuntArea scavHunt;
	Timer timer;

	void Start()
	{
		gameOver = false;
		paused = false;
		generateRandomStartingValue();
		scavHunt = GameObject.Find("ScavengerHuntPanel").GetComponent<ScavengerHuntArea>();
		scavHunt.populateScavHunt(numSHSymbols);
		timer = GameObject.Find("Timer").GetComponent<Timer>();

		ScoreLabel.text = "0";
	}
	
	// Update is called once per frame
	void Update ()
	{
		delay++;

		if(score < tempScore && delay % delayMod == 0)
		{
			score += 1;
		}
		if(Input.GetKeyDown(KeyCode.Escape) && !paused)
		{
			paused = true;
			Time.timeScale = 0;
			NGUITools.SetActive(PausePanel, true);
		}
		else if(gameOver)
		{
			Application.LoadLevel("GameOver");
		}
		else if(equationSolved)
		{
			Transform tr = null;
			equationSolved = false;

			if( GameObject.Find("bonusTimer") != null)
			{
				BonusTimer bt = GameObject.Find("bonusTimer").GetComponent<BonusTimer>();

				if(bt != null)
					bt.timeInSeconds = 0;
			}

			delay = 0;

			foreach(GameObject slot in slots)
			{
				UISprite mp = slot.gameObject.transform.parent.gameObject.GetComponent<UISprite>();
				tr = slot.transform.GetComponentsInChildren<Transform>().FirstOrDefault(t => t.name.Contains("shElement") || t.name.Contains("symbol"));
				
				if(tr != null)
				{
					mp.color = new Color(0.0f, 1.0f, 0.0f, .1f);
				}

			}

			messagePos = addToTimerMessage.transform.localPosition;
			
			NGUITools.SetActive(addToTimerMessage, true);


			runDisplayTimeMessage = true;
			addToTimerAndScore();

			//ScoreLabel.text = score.ToString();

			StartCoroutine("WaitToClear");
		}

		ScoreLabel.text = score.ToString();

		PlayerPrefs.SetInt("Score", score);

		if(runDisplayTimeMessage)
		{
			addToTimerMessage.transform.localPosition += Vector3.up * 4;

			StartCoroutine("ClearMessage");
		}
	}

	IEnumerator ClearMessage()
	{
		yield return new WaitForSeconds(1);
		addToTimerMessage.transform.localPosition = messagePos;
		NGUITools.SetActive(addToTimerMessage, false);
		runDisplayTimeMessage = false;
	}

	IEnumerator WaitToClear()
	{
		yield return new WaitForSeconds(3);

		clearEquation();

		scavHunt.populateScavHunt(numSHSymbols);
		generateRandomStartingValue();
	}

	void addToTimerAndScore()
	{
		Transform tr = null;
		int slotCounter = 0;
		for(int i = 0; i < slots.Count; i++)
		{
			tr = slots[i].transform.GetComponentsInChildren<Transform>().FirstOrDefault(t => t.name.Contains("shElement"));
			if (tr != null)
				slotCounter++;
		}

		UILabel timeToAddVal = GameObject.Find ("secondsVal").GetComponent<UILabel>();
	
		Bonus scoreBonus = null;
		if(GameObject.FindGameObjectWithTag("ScoreMultiplier") != null)
			scoreBonus = GameObject.FindGameObjectWithTag("ScoreMultiplier").GetComponent<Bonus>();



		switch(slotCounter)
		{
		case 2: 
		case 3:
			timer.timeInSeconds += 20;
			timeToAddVal.text = "+" + 20;
			if(scoreBonus != null && scoreBonus.isScoreBonus)
			{
				//score += 10 * scoreBonus.multiplier;
				tempScore = score + 10 * scoreBonus.multiplier;
				delayMod = 8;
			}
			else
			{
				//score += 10;
				tempScore = score + 10;
				delayMod = 10;
			}
			break;
		case 4: 
		case 5:
			timer.timeInSeconds += 35;
			timeToAddVal.text = "+" + 35;
			if(scoreBonus != null && scoreBonus.isScoreBonus)
			{
				//score += 50 * scoreBonus.multiplier;
				tempScore = score + 50 * scoreBonus.multiplier;
				delayMod = 1;
			}
			else
			{
				tempScore = score + 50;
				//score += 50;
				delayMod = 3;
			}
			break;
		case 6: 
		case 7:
			timer.timeInSeconds += 60;
			timeToAddVal.text = "+" + 60;
			if(scoreBonus != null && scoreBonus.isScoreBonus)
			{
				//score += 100 * scoreBonus.multiplier;
				tempScore = score + 100 * scoreBonus.multiplier;
				delayMod = 1;
			}
			else
			{
				//score += 100;
				tempScore = score + 100;
				delayMod = 1;
			}
			break;
		}
	}

	void clearEquation()
	{
		Transform tr = null;
		foreach(GameObject slot in slots)
		{
			UISprite mp = slot.gameObject.transform.parent.gameObject.GetComponent<UISprite>();
			//tr = slot.transform.GetComponentsInChildren<Transform>().FirstOrDefault(t => t.name.Contains("shElement") || t.name.Contains("symbol"));
			tr = slot.transform.GetComponentsInChildren<Transform>().FirstOrDefault(t => t.name.Contains("symbol"));

			if(tr != null)
			{
				mp.color = new Color(0.0f, 0.0f, 0.0f, .1f);
				Destroy(tr.gameObject);
			}
		}
	}

	void generateRandomStartingValue()
	{	
		slots = new List<GameObject>();

		for(int i = 1; i <= 9; i++)
		{
			if(i != 8)
			{
				slots.Add(GameObject.Find("Grid" + i.ToString()));
			}
		}

		foreach(GameObject slot in slots)
		{
			UISprite sp = slot.gameObject.transform.parent.GetComponent<UISprite>();
			sp.color = new Color(0.0f, 0.0f, 0.0f, 0.1f);
		}

		startingSlot = slots[rand.Next(0, slots.Count)];

		GameObject symbolPrefab = (GameObject)Instantiate(SymbolPrefab, startingSlot.transform.position, Quaternion.identity);
		symbolPrefab.name = "symbol" + Guid.NewGuid().ToString();

		symbolPrefab.transform.parent = startingSlot.transform;
		
		UILabel symbolValue = GameObject.Find(symbolPrefab.name).GetComponent<UILabel>();

		symbolValue.text = rand.Next(0, 21).ToString();
		symbolValue.alpha = 255;
	}

	public bool validateEquation()
	{
		bool result = false;
		Transform tr = null;
		UILabel scavHuntLabel;
		_equation = "";
		for(int i = 0; i < slots.Count; i++)
		{

			tr = slots[i].transform.GetComponentsInChildren<Transform>().FirstOrDefault(t => t.name.Contains("shElement") || t.name.Contains("symbol"));
			if (tr != null)
			{
				scavHuntLabel = tr.gameObject.GetComponent<UILabel>();
				_equation += scavHuntLabel.text + " ";
			}

			if(i == 6)
			{
				_equation += "=";
			}

		}

		try
		{
			result = verifySymbols(_equation);
		}
		catch(Exception ex)
		{
			Debug.Log(ex.Message);
		}

		return result;
	}

	bool verifySymbols(string eq)
	{
		bool result = false;

		// Expression must contain at least one operator
		if (eq.IndexOfAny(_operators) == -1)
			return result;

		if(eq.IndexOf("^") > -1)
		{
			int startInx1 = eq.IndexOf("^");
			int startInx2 = eq.IndexOf("^")+2;
			int endInx1 = eq.IndexOf(" ", startInx1);
			int endInx2 = eq.IndexOf(" ", startInx2);

			string leftOfPow = (startInx1 > 0 ? eq.Substring(0, startInx1) : "");
			if (leftOfPow != "")
			{
				startInx1 = leftOfPow.LastIndexOf(" ",leftOfPow.Length-2);
				if (startInx1 != -1)
					leftOfPow = leftOfPow.Substring(startInx1);
				else
					startInx1 = 0;

				string powLeftVal = leftOfPow.Trim();
				string powRightVal = eq.Substring(startInx2, endInx2 - startInx2);
				eq = eq.Replace(powLeftVal + " ^ " + powRightVal, "Pow(" + powLeftVal + "," + powRightVal + ")");

			}
		}

		if(eq.IndexOf("&") > -1)
		{
			int startInx = eq.IndexOf("&")+2;
			int endInx = eq.IndexOf(" ", startInx);
			string sqrtVal = eq.Substring(startInx, endInx - startInx);
			eq = eq.Replace("& " + sqrtVal, "Sqrt(" + sqrtVal + ")");
		}

		eq = eq.Replace("x", "*");


		NCalc.Expression expr;

		try
		{
			if(eq.IndexOf("=") != eq.Length - 1)
			{
				expr = new NCalc.Expression(eq);
				result = (bool)expr.Evaluate();
			}
		}
		catch(Exception ex)
		{
			Debug.Log (ex.Message);
		}
		
		return result;
	}
}