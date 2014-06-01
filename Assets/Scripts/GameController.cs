using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
//using NCalc;
//using System.Random;

public class GameController : MonoBehaviour
{
	public bool gameOver;
	//public GameObject EquationPrefab;
	public GameObject SymbolPrefab;
	//public GameObject MissingPiecePrefab;
	public UILabel ScoreLabel;
	public int numSHSymbols = 40;
	public GameObject startingSlot;

	List<GameObject> slots;
	string _equation; 
	bool equationSolved = false;
	//GameObject equationPrefab;
	int score { get; set; }
	System.Random rand = new System.Random();
	
	//Hard coded test equation. This will be replaced with some algorithm which generates equations
	List<List<string>> generatedEquation;
	List<string> tokenizedEquation;

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
		else if(equationSolved)
		{
			equationSolved = false;
			score += 10;
			ScoreLabel.text = "Score: " + score;
			StartCoroutine("WaitToClear");
		}
	}
	
	IEnumerator WaitToClear()
	{
		yield return new WaitForSeconds(2);
		//clearEquation();
//		if(numAnswers < 1)
//		{
//			generatedEquation.Clear();
//			tokenizedEquation.Clear();
//			destroyPrefabs();
//			createEquation();
//			populateScavengerHunt();
//		}
	}
	
	
//	public void updateEquation(int inx, string value)
//	{
//		int i = _equation.IndexOf("{T" + inx + "}");
//		
//		if (i > -1)
//		{
//			_equation = _equation.Remove(i, 4).Insert(i, value);
//		}
//	}
	
//	void clearEquation()
//	{
//		foreach(GameObject missingPiece in GameObject.FindGameObjectsWithTag("Missing Piece"))
//		{
//			UISprite mp = missingPiece.gameObject.GetComponent<UISprite>();
//			mp.color = Color.black;
//			NGUITools.Destroy(missingPiece.transform.GetChild(0).transform.GetChild(0).gameObject);
//		}
//		_equation = _saveEquation;
//		
//		eq.Clear();
//		for(int i = 0; i < generatedEquation.Count; i++)
//		{
//			List<string> eqPart = new List<string>();
//			for(int j = 0; j < generatedEquation[i].Count; j++)
//			{
//				eqPart.Add(generatedEquation[i][j]);
//			}
//			
//			eq.Add(eqPart);
//		}
//	}

//	void destroyPrefabs()
//	{
//		Destroy (GameObject.Find(equationPrefab.name));
//
//		foreach(GameObject scavHuntElement in GameObject.FindGameObjectsWithTag("ScavHuntElement"))
//		{
//			Destroy (scavHuntElement);
//		}
//	}
	
	void generateRandomStartingValue()
	{	
		List<GameObject> slots = new List<GameObject>();

		foreach(GameObject s in GameObject.FindGameObjectsWithTag("Slot"))
		{
			slots.Add(s);
		}

		startingSlot = slots[rand.Next(0, slots.Count - 1)];

		if(startingSlot.name == "slot8")
		{
			startingSlot = slots[8];
		}

		GameObject symbolPrefab = (GameObject)Instantiate(SymbolPrefab, startingSlot.transform.position, Quaternion.identity);
		symbolPrefab.transform.parent = startingSlot.transform;
		
		UILabel symbolValue = GameObject.Find(symbolPrefab.name).GetComponent<UILabel>();

		symbolValue.text = rand.Next(0, 21).ToString();
	
		
		//symbolPrefab.transform.localPosition = new Vector3(startPosX, 0, 0);
			
	}

//	public bool validateEquation()
//	{
//	}
	
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