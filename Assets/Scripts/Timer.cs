using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour
{
	public UILabel label;
	public float timeInSeconds;
	private string formatString;
	
	public int seconds;
	public int minutes;

	Bonus tb;
	GameController gc;

	void Start()
	{
		gc = GameObject.Find("GameController").GetComponent<GameController>();
		tb = GameObject.Find("ScoreTimerEquationHistoryPanel").GetComponent<Bonus>();
	}

	public void Update()
	{

		if(!tb.isTimeBonus)
		{
			if(gc.numEquationsSolved < 10)
				timeInSeconds -= Time.deltaTime;
			else if(gc.numEquationsSolved < 20)
				timeInSeconds -= Time.deltaTime * 1.5f;
			else if(gc.numEquationsSolved < 30)
				timeInSeconds -= Time.deltaTime * 3;
			else if(gc.numEquationsSolved > 40)
				timeInSeconds -= Time.deltaTime * 5;

			seconds = (int)timeInSeconds % 60;
			minutes = (int)timeInSeconds / 60; 
			
			formatString = string.Format("{0:00}:{1:00}", minutes, seconds); 
			//timerTexture.text = formatString;

			label.text = formatString;

			if (GetTime() == "00:00")
			{
				gc.gameOver = true;
			}
		}
//		else
//		{
//			if(gc.numEquationsSolved < 10)
//				timeInSeconds -= Time.deltaTime;
//			else if(gc.numEquationsSolved >= 10 && gc.numEquationsSolved < 20)
//				timeInSeconds -= Time.deltaTime * 2;
//			else if(gc.numEquationsSolved >= 20 && gc.numEquationsSolved < 30)
//				timeInSeconds -= Time.deltaTime * 5;
//			else if(gc.numEquationsSolved >= 40 && gc.numEquationsSolved > 40)
//				timeInSeconds -= Time.deltaTime * 8;
//
//			seconds = (int)timeInSeconds % 60;
//			minutes = (int)timeInSeconds / 60; 
//			
//			formatString = string.Format("{0:00}:{1:00}", minutes, seconds); 
//			//timerTexture.text = formatString;
//			
//			label.text = formatString;
//			
//			if (GetTime() == "00:00")
//			{
//				gc.gameOver = true;
//			}
//		}
	}
	
	public int GetMinutes()
	{
		string[] digits = label.text.Split(':');
		string digit =  digits[0];
		int minute = int.Parse(digit.Substring(1));
		
		return minute;
	}
	
	public string GetTime(){ return formatString; }
}

