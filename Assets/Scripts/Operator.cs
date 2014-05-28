using UnityEngine;
using System.Collections;

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
		value = ops[rand.Next(0,ops.Count())];
	}

	public string getValue(){
		if(missing){
			return "_";
		}
		else{
			return value;
		}
	}

	public int evaluate(int first, int second){
		if(value == "+"){
			return first + second;
		}
		else if(value == "-"){
			return first - second;
		}
		else if(value == "*"){
			return first * second;
		}
		else if(value == "/"){
			return first / second;
		}
	}
}

