using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using NCalc;
//using System.Random;

public class GameController : MonoBehaviour
{
	bool gameOver = false;
	bool equationSolved = false;

	public GameObject EquationPrefab;
	//public GameObject EqualSignPrefab;
	public GameObject SymbolPrefab;
	public GameObject MissingPiecePrefab;

	public List<List<string>> eq = new List<List<string>>();

	string _equation; 
	string _saveEquation;

	//Hard coded test equation. This will be replaced with some algorithm which generates equations
	List<List<string>> A = new List<List<string>>()
	{
		new List<string>(){"2"}, //first operand
		new List<string>(){"+", "x"}, //potential operators
		new List<string>(){"4", "6"}, //potential second operand
		new List<string>(){"="},
		new List<string>(){"8"} //answer
	};

	void Start()
	{
		createEquation();
		//generateExpression();
	}

	// Update is called once per frame
	void Update ()
	{
		if(gameOver)
		{
			Application.LoadLevel("GameOverScene");
		}
		else if(equationSolved)
		{
			clearEquation();
		}
	}

//	Expression generateExpression()
//	{
//		int operand1;
//		int operand2;
//		List<string> operators = new List<string>() { "+", "-", "*", "/" };
//		//int result;
//
//		System.Random random = new System.Random();
//
//		operand1 = random.Next(0, 100);
//		operand2 = random.Next(0, 100);
//		string op = operators[random.Next(0, operators.Count)];
//
//		string tempEq = operand1.ToString() + op + operand2.ToString();
//
//		Expression exp = new Expression();
//
//		Debug.Log();
//
//		return exp;
//
//	}



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
			NGUITools.Destroy(missingPiece.transform.GetChild(0).transform.GetChild(0).gameObject);
		}
		_equation = _saveEquation;
		equationSolved = false;

		eq.Clear();
		for(int i = 0; i < A.Count; i++)
		{
			List<string> eqPart = new List<string>();
			for(int j = 0; j < A[i].Count; j++)
			{
				eqPart.Add(A[i][j]);
			}
			
			eq.Add(eqPart);
		}
	}

	void createEquation()
	{
		GameObject equationPrefab = (GameObject)Instantiate(EquationPrefab, GameObject.Find("EquationPanel").transform.position, Quaternion.identity);

		equationPrefab.transform.parent = GameObject.Find("EquationPanel").transform;
		equationPrefab.transform.localPosition = new Vector3(0, -207, 0);
		equationPrefab.transform.localScale = new Vector3(1, 1, 1);

		for(int i = 0; i < A.Count; i++)
		{
			List<string> eqPart = new List<string>();
			for(int j = 0; j < A[i].Count; j++)
			{
				eqPart.Add(A[i][j]);
			}

			eq.Add(eqPart);
		}
		//CreateEquation(equation);

		float offset = 15f;
		float startPosX = 0;

		float elementWidth = 0;

		for(int i = 0; i < eq.Count; i++)
		{
			if (eq[i].Count == 1)
			{
				GameObject symbolPrefab = (GameObject)Instantiate(SymbolPrefab, equationPrefab.transform.position, Quaternion.identity);
				symbolPrefab.name = symbolPrefab.name + i;
				symbolPrefab.transform.parent = equationPrefab.transform;

				UILabel symbolValue = GameObject.Find(symbolPrefab.name).GetComponent<UILabel>();

				symbolPrefab.transform.localPosition = new Vector3(startPosX, 0, 0);

				symbolValue.text = eq[i][0];

				elementWidth = symbolValue.width;

				_equation += eq[i][0];
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
		//GameObject equalPrefab = (GameObject)Instantiate(EqualSignPrefab, equationPrefab.transform.position, Quaternion.identity);
		//equalPrefab.transform.parent = equationPrefab.transform;
		//equalPrefab.transform.localPosition = new Vector3(startPosX - (offset * i), 0, 0)
	}

	public void validateEquation()
	{

		//int count = _equation.Split("{T").Length - 1;

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
					//eq[inx].Remove(eq[inx][j]);
					tempList.Add(eq[inx][j]);
			}

			foreach(string s in tempList)
			{
				eq[inx].Remove(s);
			}
		}

		if (_equation.IndexOf("{T") == -1)
		{
			//clearEquation();
			equationSolved = true;
		}
	}

	bool verifySymbols(string eq)
	{
		eq = eq.Replace("x", "*");

		Expression expr = new Expression(eq);

		return (bool)expr.Evaluate();
	}

}

