using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using NCalc;

public class Equation : MonoBehaviour  
{
	public List<string> result;
	public int max = 10;
	private int min = 1;
	private string equals = "=";
	private List<string> ops = new List<string>() {"+", "-", "*", "/"};
	private List<List<int>> factors = new List<List<int>> { new List<int>() {4, 2}, 
		new List<int>() {6, 2, 3}, 
		new List<int>() {8, 2, 4}, 
		new List<int>() {9, 3},
		new List<int>() {10, 2, 5},
		new List<int>() {12, 2, 3, 4, 6},
		new List<int>() {14, 2, 7},
		new List<int>() {15, 3, 5},
		new List<int>() {16, 2, 4, 8},
		new List<int>() {18, 2, 3, 6, 9},
		new List<int>() {20, 2, 4, 5, 10},
		new List<int>() {22, 2, 11},
		new List<int>() {24, 2, 3, 4, 6, 8, 12},
		new List<int>() {25, 5},
		new List<int>() {26, 2, 13},
		new List<int>() {27, 3, 9},
		new List<int>() {28, 2, 4, 7, 14},
		new List<int>() {30, 2, 3, 5, 6, 10, 15},
		new List<int>() {32, 2, 4, 8, 16},
		new List<int>() {33, 3, 11},
		new List<int>() {34, 2, 17},
		new List<int>() {35, 5, 7},
		new List<int>() {36, 2, 3, 4, 6, 9, 12, 18},
		new List<int>() {38, 2, 19},
		new List<int>() {39, 3, 13},
		new List<int>() {40, 2, 4, 5, 8, 10, 20},
		new List<int>() {42, 2, 3, 6, 7, 14, 21},
		new List<int>() {44, 2, 4, 11, 22, 44},
		new List<int>() {45, 3, 5, 9, 15},
		new List<int>() {46, 2, 23},
		new List<int>() {48, 2, 3, 4, 6, 8, 12, 16, 24},
		new List<int>() {49, 7},
		new List<int>() {50, 2, 5, 10, 25} };
	
	
	private System.Random rand = new System.Random();
	
	public List<List<string>> Generate()
	{
		string eq = "";
		int temp = 0;
		int firstConst = 0;
		int secondConst = 0;
		string answer = "";
		string op = "";
		
		while (temp <= 0)
		{
			op = genRandomOp();
			min = 1;
			max = op == "+" || op == "*" ? 100 : 100;
			if (op == "/")
			{
				min = 0;
				max = factors.Count;
				int inx = genRandomConst();
				firstConst = factors[inx][0];
				min = 1;
				max = factors[inx].Count;
				int inx1 = genRandomConst();
				secondConst = factors[inx][inx1];
			}
			else {
				firstConst = genRandomConst();
				if (op == "-")
					max = firstConst;
				
				secondConst = genRandomConst();
			}
			if (op == "+")
			{
				temp = firstConst + secondConst;
				answer = temp.ToString();
			}
			if (op == "-")
			{
				temp = firstConst - secondConst;
				answer = temp.ToString();
			}
			if (op == "*")
			{
				temp = firstConst * secondConst;
				answer = temp.ToString();
			}
			if (op == "/")
			{
				temp = firstConst / secondConst;
				answer = temp.ToString();
			}
		}
		
		//eq = firstConst + op + secondConst + equals + answer;
		
		List<int> eqComponents = new List<int> { 1, 2, 3 };
		List<int> removeComponents = new List<int>();
		
		int numEmpty = rand.Next(1, 3);
		for (int i = 0; i < numEmpty; i++)
		{
			int remove = rand.Next(0, eqComponents.Count);
			removeComponents.Add(eqComponents[remove]);
			eqComponents.Remove(eqComponents[remove]);
		}
		
		removeComponents.Sort();
		result = new List<string>();
		List<List<string>> solutions = new List<List<string>>();
		
		if (removeComponents.Exists(f => f == 1))
			result.Add("{T1}");
		else
			result.Add(firstConst.ToString());
		
		if (removeComponents.Exists(f => f == 2))
			result.Add("{T2}");
		else
			result.Add(op);
		
		if (removeComponents.Exists(f => f == 3))
			result.Add("{T3}");
		else
			result.Add(secondConst.ToString());
		
		result.Add(equals);
		result.Add(answer.ToString());
		
		solutions.Add(new List<string> { firstConst.ToString() });
		solutions.Add(new List<string> { op });
		solutions.Add(new List<string> { secondConst.ToString() });
		solutions.Add(new List<string> { equals });
		solutions.Add(new List<string> { answer.ToString() });
		
		
		//for(int i = 0; i < numEmpty; i++){
		//    int remove = rand.Next(0, 3);
		//    StringBuilder sb = new StringBuilder(eq);
		//    if(sb[remove] == '_'){
		//        i--;
		//    }
		//    else{
		//        sb[remove] = '_';
		//        eq = sb.ToString();
		//    }
		//}
		
		eq = "";
		for (int i = 0; i < result.Count; i++)
			eq += result[i];
		
		findAllSolutions(solutions, result);
		return solutions;
	}
	
	
	public void findAllSolutions(List<List<string>> solutions, List<string> result)
	{
		if (result[1].Contains("{T"))
		{
			string eq = "";
			int answer = Convert.ToInt16(result[result.Count-1]);
			int inx = result.IndexOf("{T1}");
			
			switch (solutions[1][0])
			{
			case "+":
				for (int j = 1; j < 100; j++)
				{
					if (inx == 0)
						eq = j + "*" + result[2];
					else
						eq = result[0] + "*" + j;
					NCalc.Expression expr = new NCalc.Expression(eq);
					if ((int)expr.Evaluate() == answer)
					{
						solutions[1].Add("*");
						if (inx == 0)   
							solutions[0].Add(j.ToString());
						else
							solutions[2].Add(j.ToString());
					}                                                                  
				}
				
				break;
			case "-":
				for (int j = 2; j < 50; j++)
				{
					if (inx == 0 && j > Convert.ToInt16(result[2]))
						eq = j + "/" + result[2];
					else if (inx == 2 && Convert.ToInt16(result[0]) > j)
						eq = result[0] + "/" + j;
					
					if (eq != String.Empty)
					{
						NCalc.Expression expr = new NCalc.Expression(eq);
						if ((int)expr.Evaluate() == answer)
						{
							solutions[1].Add("/");
							if (inx == 0)
								solutions[0].Add(j.ToString());
							else
								solutions[2].Add(j.ToString());
						}
					}
				}
				
				break;
			case "*":
				for (int j = 1; j < 100; j++)
				{
					if (inx == 0)
						eq = j + "+" + result[2];
					else
						eq = result[0] + "+" + j;
					NCalc.Expression expr = new NCalc.Expression(eq);
					if ((int)expr.Evaluate() == answer)
					{
						solutions[1].Add("+");
						if (inx == 0)
							solutions[0].Add(j.ToString());
						else
							solutions[2].Add(j.ToString());
					}                                                                  
				}
				break;
			case "/":
				for (int j = 2; j < 50; j++)
				{
					if (inx == 0 && j > Convert.ToInt16(result[2]))
						eq = j + "-" + result[2];
					else if (inx == 2 && Convert.ToInt16(result[0]) > j)
						eq = result[0] + "-" + j;
					
					if (eq != String.Empty)
					{
						NCalc.Expression expr = new NCalc.Expression(eq);
						if ((int)expr.Evaluate() == answer)
						{
							solutions[1].Add("-");
							if (inx == 0)
								solutions[0].Add(j.ToString());
							else
								solutions[2].Add(j.ToString());
						}
					}
				}
				break;
			default:
				break;
			}
		}
	}
	
	public int genRandomConst(){
		int temp = rand.Next(min, max);
		return temp;
	}
	
	public string genRandomOp(){
		int index =  rand.Next(0, 4);
		return ops[index];
	}
	
}
