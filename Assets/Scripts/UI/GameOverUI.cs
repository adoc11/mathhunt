using UnityEngine;
using System.Collections;

public class GameOverUI : MonoBehaviour
{
	public UILabel scoreVal;

	void Start()
	{
		scoreVal.text = PlayerPrefs.GetInt("Score").ToString();
	}

	public void OnMainMenuClick()
	{
		Application.LoadLevel(0);
	}
}

