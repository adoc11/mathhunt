using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScavengerHuntArea : MonoBehaviour
{
	//public GameObject ScavengerHuntElementPrefab;
	GameObject scavHuntBoundingBox;
	
	int col;
	int row;
	
	System.Random rand = new System.Random();
	
	List<Color> possibleColors = new List<Color>()
	{
		Color.black,
		Color.blue,
		Color.red,
		Color.yellow,
		Color.cyan,
		Color.magenta,
		Color.green
	};

	public void populateScavHunt(int numSHSymbols)
	{
		scavHuntBoundingBox = GameObject.Find("scavengerHuntBoundingBox");
		float boundingBoxStartPosX = scavHuntBoundingBox.transform.localPosition.x - (scavHuntBoundingBox.transform.localScale.x / 2);// - 10;//- scavHuntBoundingBox.transform.localPosition.x / 2;
		float boundingBoxEndPosX = scavHuntBoundingBox.transform.localPosition.x + (scavHuntBoundingBox.transform.localScale.x / 2) - 10; //scavHuntBoundingBox.transform.localPosition.x / 2;
		
		float boundingBoxStartPosY = scavHuntBoundingBox.transform.localPosition.y - (scavHuntBoundingBox.transform.localScale.y / 2);// - 10;
		float boundingBoxEndPosY =  scavHuntBoundingBox.transform.localPosition.y + (scavHuntBoundingBox.transform.localScale.y / 2) - 10;
		
		GameObject scavHuntPrefab;
		
		string[,] colRows = new string[8, 8];
		
		List<KeyValuePair<string, double>> operators = new List<KeyValuePair<string, double>>()
		{
			new KeyValuePair<string, double>("√", 0.05),
			new KeyValuePair<string, double>("^", 0.05),
			new KeyValuePair<string, double>("x", 0.15),
			new KeyValuePair<string, double>("/", 0.15),
			new KeyValuePair<string, double>("+", 0.3),
			new KeyValuePair<string, double>("-", 0.3)
		};
		
		List<KeyValuePair<string, double>> numValues = new List<KeyValuePair<string, double>>();
		
		for(int k = 0; k <= 20; k++)
		{
			numValues.Add(new KeyValuePair<string, double>(k.ToString(), 0.024)); 
		}
		for(int k = 21; k <= 75; k++)
		{
			numValues.Add(new KeyValuePair<string, double>(k.ToString(), 0.007)); 
		}
		for(int k = 76; k <= 100; k++)
		{
			numValues.Add(new KeyValuePair<string, double>(k.ToString(), 0.004)); 
		}
		
		List<string> randomChoices = new List<string>();
		
		for(int n = 0; n < 30; n++)
		{
			double randValProb = rand.NextDouble();
			double cumulative = 0.0;
			
			string selectedElement = "";
			
			for (int j = 0; j < numValues.Count; j++)
			{
				cumulative += numValues[j].Value;
				if (randValProb < cumulative)
				{
					selectedElement = numValues[j].Key;
					break;
				}
			}
			if (selectedElement == "")
			{
				selectedElement = numValues[rand.Next(20)].Key;
			}
			
			randomChoices.Add(selectedElement);
		}
		
		for(int n = 0; n < 10; n++)
		{
			double randOpProb = rand.NextDouble();
			double cumulative = 0.0;
			
			string selectedElement = "";
			
			for (int j = 0; j < operators.Count; j++)
			{
				cumulative += operators[j].Value;
				if (randOpProb < cumulative)
				{
					selectedElement = operators[j].Key;
					break;
				}
			}
			
			randomChoices.Add(selectedElement);
		}
		
		
		for(int i = 0; i < numSHSymbols; i++)
		{
			scavHuntPrefab = GameObject.Find("shElement"+(i+1).ToString());
			scavHuntPrefab.transform.parent = GameObject.Find("ScavengerHuntPanel").transform;
			scavHuntPrefab.transform.localScale = Vector3.one;
			
			UILabel scavHuntValue = GameObject.Find(scavHuntPrefab.name).GetComponent<UILabel>();

			scavHuntValue.text = randomChoices[i];

			int randPosInx;


			DragSymbol ds = scavHuntPrefab.gameObject.GetComponent<DragSymbol>();
			ds.enabled = true;
			
			do
			{
				randPosInx = rand.Next(0, 64);
				
				row = randPosInx / 8;
				col = randPosInx % 8;
			} while(!string.IsNullOrEmpty(colRows[row, col]));
			
			colRows[row,col] = scavHuntValue.text;
			
			float symbolPosX = (col * 83) + boundingBoxStartPosX + 50;
			float symbolPosY = (row * 48) + boundingBoxStartPosY + 25;
			
			Vector3 symbolPos = new Vector3(symbolPosX, symbolPosY, 0.0f);
			
			int inx = rand.Next(0, possibleColors.Count);
		
			if(scavHuntValue.text == "+")
				scavHuntValue.color = Color.red;
			else if(scavHuntValue.text == "-")
				scavHuntValue.color = Color.blue;
			else if(scavHuntValue.text == "/")
				scavHuntValue.color = Color.black;
			else if(scavHuntValue.text == "x")
				scavHuntValue.color = Color.green;
			else if(scavHuntValue.text == "^")
				scavHuntValue.color = Color.magenta;
			else if(scavHuntValue.text == "√")
				scavHuntValue.color = Color.yellow;
			else
				scavHuntValue.color = possibleColors[inx];

			scavHuntValue.alpha = 255;
			
			scavHuntPrefab.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, UnityEngine.Random.Range(-25.0f, 25.0f));
			scavHuntPrefab.transform.localPosition = symbolPos;
		}
	}
}