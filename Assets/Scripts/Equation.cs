using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Equation : MonoBehaviour
{
	// needs to be abstracted to work with any size equation
	public Constant firstConst;
	public Constant secondConst;
	public Operator op;
	public Constant answer;

	public Equation(){
		int c2 = 0;
		firstConst = new Constant();
		int c1 = firstConst.getValue();
		op = new Operator();
		string o = op.getValue();
		if(op.getValue() == "/"){
			List<int> divisors = firstConst.getDivisors().ToList();
			secondConst = new Constant(divisors);
		}
		else{
			secondConst = new Constant();
			c2 = secondConst.getValue();

		}
		answer = new Constant(op.evaluate(c1, c2));
	}

	public string writeToString(){
		return firstConst.getStrValue() + op.getValue() + secondConst.getStrValue() + "=" + answer.getStrValue();
	}

	public List<string> writeToList(){
		List<string> list = new List<string>();
		list.Add(firstConst.getStrValue());
		list.Add(op.getValue());
		list.Add(secondConst.getStrValue());
		list.Add("=");
		list.Add(answer.getStrValue());
		return list;
	}

	public List<string> addMissing(){
		List<string> missingParts = new List<string>();
		System.Random myRand = new System.Random();
		for(int i = 0; i < 3; i++){
			int randNum = myRand.Next(0,2);
			if(randNum == 1 && i == 0){
				missingParts.Add(firstConst.getStrValue());
				firstConst.missing = true;
			}
			else if(randNum == 1 && i == 1){
				missingParts.Add(op.getValue());
				op.missing = true;
			}
			else if(randNum == 1 && i == 2){
				missingParts.Add(secondConst.getStrValue());
				secondConst.missing = true;
			}
		}
		return missingParts;
	}
}
