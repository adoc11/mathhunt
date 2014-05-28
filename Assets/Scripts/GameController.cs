using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
	public bool gameOver;
	public GameObject EquationPrefab;
	public GameObject SymbolPrefab;
	public GameObject MissingPiecePrefab;
	public GameObject ScavengerHuntElementPrefab;

	public List<string> eq = new List<string>();

	string _equation; 
	string _saveEquation;
	bool equationSolved = false;
	int numSHSymbols = 20;

	List<string> generatedEquation;
	List<Color> possibleColors = new List<Color>()
	{
		Color.black,
		Color.blue,
		Color.red,
		Color.yellow,
		Color.cyan,
		Color.magenta,
		Color.green,
		new Color(255, 165, 0)
	};

	void Start()
	{
		gameOver = false;
		createEquation();
		populateScavengerHunt();
	}

	void Update ()
	{
		if(gameOver)
		{
			Application.LoadLevel("GameOver");
		}
		else if(equationSolved)
		{
			equationSolved = false;
			StartCoroutine("WaitToClear");
		}
	}

	IEnumerator WaitToClear()
	{
		yield return new WaitForSeconds(2);
		clearEquation();
	}


	public void updateEquation(int inx, string value)
	{
		int i = _equation.IndexOf("{T" + inx + "}");

		if (i > -1)
		{
			_equation = _equation.Remove(i, 4).Insert(i, value);
		}
	}

	void clearEquation()
	{
		foreach(GameObject missingPiece in GameObject.FindGameObjectsWithTag("Missing Piece"))
		{
			UISprite mp = missingPiece.gameObject.GetComponent<UISprite>();
			mp.color = Color.black;
			NGUITools.Destroy(missingPiece.transform.GetChild(0).transform.GetChild(0).gameObject);
		}
		_equation = _saveEquation;

		eq.Clear();
		for(int i = 0; i < generatedEquation.Count; i++)
		{	
			eq.Add(generatedEquation[i]);
		}
	}

	void createEquation()
	{
		GameObject equationPrefab = (GameObject)Instantiate(EquationPrefab, GameObject.Find("EquationPanel").transform.position, Quaternion.identity);

		equationPrefab.transform.parent = GameObject.Find("EquationPanel").transform;
		equationPrefab.transform.localPosition = new Vector3(0, -207, 0);
		equationPrefab.transform.localScale = new Vector3(1, 1, 1);

		Equation equation = new Equation();
		equation.addMissing();

		generatedEquation = equation.writeToList();

		for(int i = 0; i < generatedEquation.Count; i++)
		{
			eq.Add(generatedEquation[i]);
		}

		float offset = 15f;
		float startPosX = 0;

		float elementWidth = 0;

		for(int i = 0; i < eq.Count; i++)
		{
			if (!eq[i].Contains("{T"))
			{
				GameObject symbolPrefab = (GameObject)Instantiate(SymbolPrefab, equationPrefab.transform.position, Quaternion.identity);
				symbolPrefab.name = symbolPrefab.name + i;
				symbolPrefab.transform.parent = equationPrefab.transform;

				UILabel symbolValue = GameObject.Find(symbolPrefab.name).GetComponent<UILabel>();

				symbolPrefab.transform.localPosition = new Vector3(startPosX, 0, 0);

				if (eq[i] == "*")
					symbolValue.text = "x";
				else
					symbolValue.text = eq[i];

				elementWidth = symbolValue.width;

				_equation += eq[i];
			}
			else
			{
				GameObject emptyBoxPrefab = (GameObject)Instantiate(MissingPiecePrefab, equationPrefab.transform.position, Quaternion.identity);
				emptyBoxPrefab.name = emptyBoxPrefab.name + i;
				emptyBoxPrefab.tag = "Missing Piece";
				emptyBoxPrefab.transform.parent = equationPrefab.transform;

				UISprite boxSprite = GameObject.Find(emptyBoxPrefab.name).GetComponent<UISprite>();
				emptyBoxPrefab.transform.localPosition = new Vector3(startPosX + 20f, 0, 0);
				emptyBoxPrefab.transform.localScale = Vector3.one;

				elementWidth = boxSprite.width - 20f;
				_equation += "{T" + i + "}"; 
			}

			startPosX += elementWidth + offset;
		}

		_saveEquation = _equation;
	}

	public void validateEquation()
	{



		List<string> tokens = new List<string>();

		string tempEQ = _equation;
		int pos = tempEQ.IndexOf("{T");
		while (pos > -1 && pos < tempEQ.Length)
		{
			tokens.Add(tempEQ.Substring(pos,4));
			pos = tempEQ.IndexOf("{T", pos + 1);
		}

		List<string> tempList = new List<string>();
		for (int i = 0; i < tokens.Count; i++)
		{
			int inx = Convert.ToInt16(tokens[i].Substring(2,1));
			for (int j = 0; j < eq[inx].Count; j++)
			{
				tempEQ = _equation;
				int k = tempEQ.IndexOf(tokens[i]);
				tempEQ = tempEQ.Remove(k, 4).Insert(k, eq[inx][j]);
				if (!verifySymbols(tempEQ))
					tempList.Add(eq[inx][j]);
			}

			foreach(string s in tempList)
			{
				eq[inx].Remove(s);
			}
		}

		if (_equation.IndexOf("{T") == -1)
		{
			equationSolved = true;
		}
	}

	bool verifySymbols(string eq)
	{
		eq = eq.Replace("x", "*");

		NCalc.Expression expr = new NCalc.Expression(eq);

		return (bool)expr.Evaluate();
	}

	void populateScavengerHunt()
	{
		GameObject scavHuntBoundingBox = GameObject.Find("scavengerHuntBoundingBox");
		float boundingBoxStartPosX = scavHuntBoundingBox.transform.localPosition.x - (scavHuntBoundingBox.transform.localScale.x / 2);//- scavHuntBoundingBox.transform.localPosition.x / 2;
		float boundingBoxEndPosX = scavHuntBoundingBox.transform.localPosition.x + (scavHuntBoundingBox.transform.localScale.x / 2); //scavHuntBoundingBox.transform.localPosition.x / 2;

		float boundingBoxStartPosY = scavHuntBoundingBox.transform.localPosition.y - (scavHuntBoundingBox.transform.localScale.y / 2);
		float boundingBoxEndPosY =  scavHuntBoundingBox.transform.localPosition.y + (scavHuntBoundingBox.transform.localScale.y / 2);

		int k = 0;
		GameObject scavHuntPrefab;
		System.Random rand = new System.Random();
		for(int i = 0; i < tokenizedEquation.Count; i++)
		{
			if(tokenizedEquation[i].Contains("{T"))
			{
				for(int j = 0; j < generatedEquation[i].Count; j++)
				{
					scavHuntPrefab = (GameObject)Instantiate(ScavengerHuntElementPrefab, scavHuntBoundingBox.transform.position, Quaternion.identity);
					scavHuntPrefab.name = scavHuntPrefab.name + k;
					scavHuntPrefab.transform.parent = GameObject.Find("ScavengerHuntPanel").transform;
					scavHuntPrefab.transform.localScale = Vector3.one;
					
					UILabel scavHuntValue = GameObject.Find(scavHuntPrefab.name).GetComponent<UILabel>();

					if (generatedEquation[i][j] == "*")
						scavHuntValue.text = "x";
					else
						scavHuntValue.text = generatedEquation[i][j];

					int inx = rand.Next(0, possibleColors.Count);
					scavHuntValue.color = possibleColors[inx];

					scavHuntPrefab.transform.localPosition = new Vector3(UnityEngine.Random.Range(boundingBoxStartPosX, boundingBoxEndPosX), UnityEngine.Random.Range(boundingBoxStartPosY, boundingBoxEndPosY), 0.0f);
					k +=1;
				}
			}
		}

		System.Random rand2 = new System.Random();
		for(int i = GameObject.FindGameObjectsWithTag("ScavHuntElement").Length; i < numSHSymbols; i++)
		{
			scavHuntPrefab = (GameObject)Instantiate(ScavengerHuntElementPrefab, scavHuntBoundingBox.transform.position, Quaternion.identity);
			scavHuntPrefab.name = scavHuntPrefab.name + (i+1);
			scavHuntPrefab.transform.parent = GameObject.Find("ScavengerHuntPanel").transform;
			scavHuntPrefab.transform.localScale = Vector3.one;
			
			UILabel scavHuntValue = GameObject.Find(scavHuntPrefab.name).GetComponent<UILabel>();
			
			List<string> randomChoices = new List<string>();
			for(int j = 0; j <= 100; j++)
			{
				randomChoices.Add(j.ToString());
			}

			randomChoices.Add("+");
			randomChoices.Add("-");
			randomChoices.Add("x");
			randomChoices.Add("/");

			scavHuntValue.text = randomChoices[rand2.Next(0, randomChoices.Count)];
			
			int inx = rand.Next(0, possibleColors.Count);
			scavHuntValue.color = possibleColors[inx];
			
			scavHuntPrefab.transform.localPosition = new Vector3(UnityEngine.Random.Range(boundingBoxStartPosX, boundingBoxEndPosX), UnityEngine.Random.Range(boundingBoxStartPosY, boundingBoxEndPosY), 0.0f);
		}
	}
	
}

