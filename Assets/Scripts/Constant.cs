using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Constant : MonoBehaviour
{
	private string strValue;
	private int value;
	public bool missing = false;
	public int max = 10;
	private int min = 0;

	public Constant(int theValue){
		value = theValue;
		strValue = theValue.ToString();
	}

	public Constant(){
		System.Random rand = new System.Random();
		value = rand.Next(min, max);
		strValue = value.ToString();
	}

	public Constant(List<int> options){
		System.Random rand = new System.Random();
		value = options[rand.Next(0,options.Count)];
		strValue = value.ToString();
	}

	public int getValue(){
		return value;
	}

	public string getStrValue(){
		if(missing){
			return "{T}";
		}
		else{
			return strValue;
		}
	}

	public IEnumerable<int> getDivisors(){
		int temp = value;
		return from a in Enumerable.Range(2, temp / 2)
			where temp % a == 0
				select temp;
	}
}