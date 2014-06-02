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

	public void Update()
	{
		if(GameObject.FindGameObjectWithTag("TimeBonus") != null)
		{
			tb = GameObject.FindGameObjectWithTag("TimeBonus").GetComponent<Bonus>();
		}

		if(tb != null)
		{
			if(!tb.isTimeBonus)
			{
				timeInSeconds -= Time.deltaTime;
				seconds = (int)timeInSeconds % 60;
				minutes = (int)timeInSeconds / 60; 
				
				formatString = string.Format("{0:00}:{1:00}", minutes, seconds); 
				//timerTexture.text = formatString;

				label.text = formatString;

				if (GetTime() == "00:00")
				{
					GameController gc = GameObject.Find ("GameController").GetComponent<GameController>();
					gc.gameOver = true;
				}
			}
		}
		else
		{
			timeInSeconds -= Time.deltaTime;
			seconds = (int)timeInSeconds % 60;
			minutes = (int)timeInSeconds / 60; 
			
			formatString = string.Format("{0:00}:{1:00}", minutes, seconds); 
			//timerTexture.text = formatString;
			
			label.text = formatString;
			
			if (GetTime() == "00:00")
			{
				GameController gc = GameObject.Find ("GameController").GetComponent<GameController>();
				gc.gameOver = true;
			}
		}
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

