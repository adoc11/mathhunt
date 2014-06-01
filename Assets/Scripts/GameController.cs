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
	public GameObject SymbolPrefab;
	public UILabel ScoreLabel;
	public int numSHSymbols = 40;
	public GameObject startingSlot;
	public bool equationSolved = false;

	List<GameObject> slots;
	string _equation; 
	
	int score { get; set; }
	System.Random rand = new System.Random();

	ScavengerHuntArea scavHunt;

	void Start()
	{
		gameOver = false;
		generateRandomStartingValue();
		scavHunt = gameObject.GetComponent<ScavengerHuntArea>();
		scavHunt.populateScavHunt(numSHSymbols);
		//createEquation();
		//populateScavengerHunt();

		ScoreLabel.text = "Score: " + 0;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(gameOver)
		{
			Application.LoadLevel("GameOver");
		}
//		else if(equationSolved)
//		{
//			Transform tr = null;
//			equationSolved = false;
//			score += 10;
//			ScoreLabel.text = "Score: " + score;
//
//			foreach(GameObject slot in slots)
//			{
//				UISprite mp = slot.gameObject.transform.parent.gameObject.GetComponent<UISprite>();
//				tr = slot.transform.GetComponentsInChildren<Transform>().FirstOrDefault(t => t.name.Contains("shElement") || t.name.Contains("symbol"));
//				
//				if(tr != null)
//				{
//					//mp.color = new Color(.812f, 1.0f, .812f);
//					mp.color = new Color(0.0f, 1.0f, 0.0f, .1f);
//				}
//
//			}

			//getNextEquation();


			//StartCoroutine("WaitToClear");
		//}
	}

	public void getNextEquation()
	{
		clearEquation();
		//scavHunt.clearScavHuntArea();
		scavHunt.populateScavHunt(numSHSymbols);
		generateRandomStartingValue();
	}
	
	IEnumerator WaitToClear()
	{
		yield return new WaitForSeconds(2);
		clearEquation();
		//scavHunt.clearScavHuntArea();
		//scavHunt.populateScavHunt(numSHSymbols);
		generateRandomStartingValue();
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
				//UILabel l = tr.gameObject.GetComponent<UILabel>();
				//l.text = "";
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
				slots.Add(GameObject.Find("Grid" + i.ToString()));
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

		//Debug.Log(_equation);

		return verifySymbols(_equation);
	}

	bool verifySymbols(string eq)
	{
		bool result = false;
		int startInx = eq.IndexOf("√")+2;
		int endInx = eq.IndexOf(" ", startInx);
		string sqrtVal = eq.Substring(startInx, endInx - startInx);
		eq = eq.Replace("x", "*");
		eq = eq.Replace("√", "Sqrt(" + sqrtVal + ")");

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
	
//	public void validateEquation()
//	{
//		
//		//int count = _equation.Split("{T").Length - 1;
//		
//		List<string> tokens = new List<string>();
//		
//		string tempEQ = _equation;
//		int pos = tempEQ.IndexOf("{T");
//		while (pos > -1 && pos < tempEQ.Length)
//		{
//			tokens.Add(tempEQ.Substring(pos,4));
//			pos = tempEQ.IndexOf("{T", pos + 1);
//		}
//		
//		List<string> tempList = new List<string>();
//		for (int i = 0; i < tokens.Count; i++)
//		{
//			int inx = Convert.ToInt16(tokens[i].Substring(2,1));
//			for (int j = 0; j < eq[inx].Count; j++)
//			{
//				tempEQ = _equation;
//				int k = tempEQ.IndexOf(tokens[i]);
//				tempEQ = tempEQ.Remove(k, 4).Insert(k, eq[inx][j]);
//				if (!verifySymbols(tempEQ))
//					//eq[inx].Remove(eq[inx][j]);
//					tempList.Add(eq[inx][j]);
//			}
//			
//			foreach(string s in tempList)
//			{
//				eq[inx].Remove(s);
//			}
//		}
//		
//		if (_equation.IndexOf("{T") == -1)
//		{
//			//clearEquation();
//			equationSolved = true;
//			numAnswers--;
//		}
//	}
//	
//	bool verifySymbols(string eq)
//	{
//		eq = eq.Replace("x", "*");
//		//eq = eq.Replace("√", "sqrt(
//		
//		NCalc.Expression expr = new NCalc.Expression(eq);
//		
//		return (bool)expr.Evaluate();
//	}

//	void populateScavengerHunt()
//	{
//		GameObject scavHuntBoundingBox = GameObject.Find("scavengerHuntBoundingBox");
//		float boundingBoxStartPosX = scavHuntBoundingBox.transform.localPosition.x - (scavHuntBoundingBox.transform.localScale.x / 2) + 10;//- scavHuntBoundingBox.transform.localPosition.x / 2;
//		float boundingBoxEndPosX = scavHuntBoundingBox.transform.localPosition.x + (scavHuntBoundingBox.transform.localScale.x / 2) - 10; //scavHuntBoundingBox.transform.localPosition.x / 2;
//		
//		float boundingBoxStartPosY = scavHuntBoundingBox.transform.localPosition.y - (scavHuntBoundingBox.transform.localScale.y / 2) + 10;
//		float boundingBoxEndPosY =  scavHuntBoundingBox.transform.localPosition.y + (scavHuntBoundingBox.transform.localScale.y / 2) - 10;
//		
//		int k = 0;
//		GameObject scavHuntPrefab;
//		System.Random rand = new System.Random();
//		bool safeSpawn = false;
//
//		List<Collider> shColliders = new List<Collider>();
//
//		foreach(GameObject sc in GameObject.FindGameObjectsWithTag("ScavHuntElement"))
//		{
//			shColliders.Add(sc.GetComponent<Collider>());
//		}
//
//		for(int i = 0; i < numSHSymbols; i++)
//		{
//			scavHuntPrefab = (GameObject)Instantiate(ScavengerHuntElementPrefab, scavHuntBoundingBox.transform.position, Quaternion.identity);
//			scavHuntPrefab.name = scavHuntPrefab.name + (i+1);
//			scavHuntPrefab.transform.parent = GameObject.Find("ScavengerHuntPanel").transform;
//			scavHuntPrefab.transform.localScale = Vector3.one;
//			
//			UILabel scavHuntValue = GameObject.Find(scavHuntPrefab.name).GetComponent<UILabel>();
//			
//			List<string> randomChoices = new List<string>();
//			for(int j = 0; j <= 100; j++)
//			{
//				randomChoices.Add(j.ToString());
//			}
//
//			for(int n = 0; n < 10; n++)
//			{
//				randomChoices.Add("+");
//				randomChoices.Add("-");
//				randomChoices.Add("x");
//				randomChoices.Add("/");
//				randomChoices.Add("^");
//				randomChoices.Add("√");
//			}
//			
//			scavHuntValue.text = randomChoices[rand.Next(0, randomChoices.Count)];
//			
//			int inx = rand.Next(0, possibleColors.Count);
//			scavHuntValue.color = possibleColors[inx];
//
//			if (!safeSpawn){
//				safeSpawn = true;
//				
//				foreach(Collider shc in shColliders){
//					if (shc.bounds.Intersects(scavHuntPrefab.collider.bounds)){
//						safeSpawn = false;
//						break;
//					}
//				}
//			}
//			
//			scavHuntPrefab.transform.localPosition = new Vector3(UnityEngine.Random.Range(boundingBoxStartPosX, boundingBoxEndPosX), UnityEngine.Random.Range(boundingBoxStartPosY, boundingBoxEndPosY), 0.0f);
//		}
//	}
}