using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NCalc;

public class Equation_old  {
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
		new List<int>() {50, 2, 5, 10, 25},
		new List<int>() {52, 2, 4, 13, 26},
		new List<int>() {54, 2, 3, 6, 9, 18, 27},
		new List<int>() {55, 5, 11},
		new List<int>() {56, 2, 4, 7, 8, 14, 28},
		new List<int>() {57, 3, 19},
		new List<int>() {58, 2, 29},
		new List<int>() {60, 2, 3, 4, 5, 6, 10, 12, 15, 20, 30},
		new List<int>() {62, 2, 31},
		new List<int>() {63, 3, 7, 9, 21},
		new List<int>() {64, 2, 4, 8, 16, 32},
		new List<int>() {65, 5, 13},
		new List<int>() {66, 2, 3, 6, 11, 22, 33},
		new List<int>() {68, 2, 4, 17, 34},
		new List<int>() {69, 3, 23},
		new List<int>() {70, 2, 5, 7, 10, 14, 35},
		new List<int>() {72, 2, 3, 4, 6, 8, 9, 12, 18, 24, 36},
		new List<int>() {74, 2, 37},
		new List<int>() {75, 3, 5, 15, 25},
		new List<int>() {76, 2, 4, 19, 38},
		new List<int>() {77, 7, 11},
		new List<int>() {78, 2, 3, 6, 13, 26, 39},
		new List<int>() {80, 2, 4, 5, 8, 10, 16, 20, 40},
		new List<int>() {81, 3, 9, 27},
		new List<int>() {82, 2, 41},
		new List<int>() {84, 2, 3, 4, 6, 7, 12, 14, 21, 28, 42},
		new List<int>() {85, 5, 17},
		new List<int>() {86, 2, 43},
		new List<int>() {87, 3, 29},
		new List<int>() {88, 2, 4, 8, 11, 22, 44},
		new List<int>() {90, 2, 3, 5, 6, 9, 10, 15, 18, 30, 45},
		new List<int>() {91, 7, 13},
		new List<int>() {92, 2, 4, 23, 46},
		new List<int>() {93, 3, 31},
		new List<int>() {94, 2, 47},
		new List<int>() {95, 5, 19},
		new List<int>() {96, 2, 3, 4, 6, 8, 12, 16, 24, 32, 48},
		new List<int>() {98, 2, 7, 14, 49},
		new List<int>() {99, 3, 9, 11, 33},
		new List<int>() {100, 2, 4, 5, 10, 20, 25, 50}
	};
	
	
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
			max = op == "*" ? 20 : 100;
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
			int remove = eqComponents[rand.Next(0, eqComponents.Count)];
			removeComponents.Add(remove);
			eqComponents.Remove(remove);
			// Do not allow both operands to be missing, so if op1 or op2 is selected 
			// remove the other operand from the list so the only option left is the operator
			if (remove == 1)
				eqComponents.Remove(3);
			else if (remove == 3)
				eqComponents.Remove(1);
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
		// If only one substitution, there is only one solution, and we already found it
		if (result.Count(s => s.Contains("{T")) == 1)
			return;
		
		string eq = "";
		int answer = Convert.ToInt16(result[result.Count-1]);
		
		if (result[0] == "{T1}" && result[1] == "{T2}")
		{
			int op2 = Convert.ToInt16(result[2]);
			if (!solutions[1].Contains("+") && answer >= op2)
			{
				int op1 = answer - op2;
				solutions[1].Add("+");
				solutions[0].Add(op1.ToString());
			}
			if (!solutions[1].Contains("-"))
			{
				int op1 = answer + op2;
				solutions[1].Add("-");
				solutions[0].Add(op1.ToString());
			}
			if (!solutions[1].Contains("*") && answer >= op2 && answer % op2 == 0)
			{
				int op1 = answer / op2;
				solutions[1].Add("*");
				solutions[0].Add(op1.ToString());
			}
			if (!solutions[1].Contains("/"))
			{
				int op1 = answer * op2;
				solutions[1].Add("/");
				solutions[0].Add(op1.ToString());
			}
		}
		
		if (result[1] == "{T2}" && result[2] == "{T3}")
		{
			int op1 = Convert.ToInt16(result[0]);
			if (op1 == answer)
			{
				// The only solutions are 0 and 1, and all 4 operators
				if (!solutions[1].Contains("+"))
					solutions[1].Add("+");
				if (!solutions[1].Contains("-"))
					solutions[1].Add("-");
				if (!solutions[1].Contains("*"))
					solutions[1].Add("*");
				if (!solutions[1].Contains("/"))
					solutions[1].Add("/");
				if (!solutions[2].Contains("0"))
					solutions[2].Add("0");
				if (!solutions[2].Contains("1"))
					solutions[2].Add("1");
			}
			if (op1 > answer)
			{
				if (solutions[1].Contains("-") && op1 % answer == 0)
				{
					int op2 = op1 / answer;
					solutions[1].Add("/");
					solutions[2].Add(op2.ToString());
				}
				if (solutions[1].Contains("/"))
				{
					int op2 = op1 - answer;
					solutions[1].Add("-");
					solutions[2].Add(op2.ToString());
				}
			}
			if (op1 < answer)
			{
				if (solutions[1].Contains("+") && answer % op1 == 0)
				{
					int op2 = answer / op1;
					solutions[1].Add("*");
					solutions[2].Add(op2.ToString());
				}
				if (solutions[1].Contains("*"))
				{
					int op2 = answer - op1;
					solutions[1].Add("+");
					solutions[2].Add(op2.ToString());
				}
			}
		}
		
		if (result[0] == "{T1}" && result[2] == "{T3}")
		{
			// Missing 2 operands - need to generate all possible solutions, and randomly pick up to 5
			if (solutions[1].Contains("+"))
			{
				for (int op1 = 1; op1 < answer; op1++)
				{
					if (!solutions[0].Contains(op1.ToString()))
					{
						int op2 = answer - op1;
						solutions[0].Add(op1.ToString());
						solutions[2].Add(op2.ToString());
					}
				}
			}
			if (solutions[1].Contains("-"))
			{
				for (int op1 = 100; op1 > answer; op1--)
				{
					if (!solutions[0].Contains(op1.ToString()))
					{
						int op2 = op1 - answer;
						solutions[0].Add(op1.ToString());
						solutions[2].Add(op2.ToString());
					}
				}
			}
			//if (solutions[1].Contains("*"))
			//{
			//    int max = (int)Math.Ceiling(Math.Sqrt(answer));
			//    for (int op1 = 1; op1 < max; op1++)
			//    {
			//        if (!solutions[0].Contains(op1.ToString()))
			//        {
			//            int op2 = op1 - answer;
			//            solutions[0].Add(op1.ToString());
			//            solutions[2].Add(op2.ToString());
			//        }
			//    }
			//}
			
			
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
