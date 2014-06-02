using UnityEngine;
using System.Collections;

public class MainMenuUI : MonoBehaviour
{
	public UILabel scoreVal;
	
	void Start()
	{
		scoreVal.text = PlayerPrefs.GetInt("HighScore").ToString();
	}

	public void OnStartClick()
	{
		Application.LoadLevel(1);
	}
}

