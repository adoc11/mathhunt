using UnityEngine;
using System.Collections;

public class MainMenuUI : MonoBehaviour
{
	public UILabel scoreVal;
	public GameObject mainMenuPanel;
	public GameObject helpPanel;
	
	void Start()
	{
		scoreVal.text = PlayerPrefs.GetInt("HighScore").ToString();
	}

	public void OnHelpClick()
	{
		NGUITools.SetActive(this.gameObject, false);
		NGUITools.SetActive(helpPanel, true);
	}

	public void OnStartClick()
	{
		Application.LoadLevel(1);
	}
}

