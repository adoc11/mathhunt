using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;


public class Equation : MonoBehaviour {
	public int max = 100;
	private int min = 0;
	private string equals = "=";
	private List<string> ops = new List<string>() {"+", "-", "*", "/"};
	private System.Random rand = new System.Random();

	public string Generate(){
		string eq = "";
		int firstConst = genRandomConst();
		int secondConst = genRandomConst();
		string op = genRandomOp();
		string answer = "";

		if(op == "+"){
			int temp = firstConst + secondConst;
			answer = temp.ToString();
		}
		if(op == "-"){
			int temp = firstConst - secondConst;
			answer = temp.ToString();
		}
		if(op == "*"){
			int temp = firstConst * secondConst;
			answer = temp.ToString();
		}
		if(op == "/"){
			int temp = firstConst / secondConst;
			answer = temp.ToString();
		}

		eq = firstConst + op + secondConst + equals + answer;

		int numEmpty = rand.Next(1, 3);

		for(int i = 0; i < numEmpty; i++){
			int remove = rand.Next(0, 3);
			StringBuilder sb = new StringBuilder(eq);
			if(sb[remove] == '_'){
				i--;
			}
			else{
				sb[remove] = '_';
				eq = sb.ToString();
			}
		}

		return eq;
	}

	//public List<List<string>> findAllSolutions(string eq){

	//}

	public int genRandomConst(){
		int temp = rand.Next(min, max);
		return temp;
	}

	public string genRandomOp(){
		int index =  rand.Next(0, 4);
		return ops[index];
	}
}
