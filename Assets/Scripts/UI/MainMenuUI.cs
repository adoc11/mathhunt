using UnityEngine;
using System.Collections;

public class MainMenuUI : MonoBehaviour
{
	public UILabel scoreVal;
	
	void Start()
	{
		scoreVal.text = PlayerPrefs.GetInt("Score").ToString();
	}

	public void OnStartClick()
	{
		Application.LoadLevel(1);
	}
}

