using UnityEngine;
using System.Collections;

public class BonusTimer : MonoBehaviour
{
	public UILabel label;
	public float timeInSeconds;
	private string formatString;
	
	public int seconds;
	public bool bonusOver = false;
	//public int minutes;
	
	
	public void Update()
	{
		timeInSeconds -= Time.deltaTime;
		seconds = (int)timeInSeconds % 60;
		//minutes = (int)timeInSeconds / 60; 
		
		formatString = string.Format("{0} sec", seconds); 
		//timerTexture.text = formatString;

		label.text = formatString;

		if (GetTime() == "0 sec")
		{
			bonusOver = true;
			UILabel bonusTimeLabel = gameObject.GetComponent<UILabel>();
			bonusTimeLabel.enabled = false;
			this.enabled = false;
		}
	}
	
	public string GetTime(){ return formatString; }
}

