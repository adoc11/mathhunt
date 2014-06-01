using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Operator : MonoBehaviour
{
	private string value;
	public bool missing = false;
	private List<string> ops = new List<string>() {"+", "-", "*", "/"};

	public Operator(string theValue){
		if(ops.Contains(theValue)){
			value = theValue;
		}
		else{
			throw new UnityException("Invalid Operator");
		}
	}

	public Operator(){
		System.Random rand = new System.Random();
		value = ops[rand.Next(0,ops.Count)];
	}

	public string getValue(){
		if(missing){
			return "{T}";
		}
		else{
			return value;
		}
	}

	public int evaluate(int first, int second){
		int result = 0;
		if(value == "+"){
			result = first + second;
		}
		else if(value == "-"){
			result = first - second;
		}
		else if(value == "*"){
			result = first * second;
		}
		else if(value == "/"){
			if( second != 0)
				result = first / second;
			else
				result = 0;
		}
		return result;
	}
}

