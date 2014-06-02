using UnityEngine;
using System.Collections;

public class PauseMenuUI : MonoBehaviour
{
	public GameObject muteButton;
	public GameObject soundButton;

	GameController gc;

	void Start()
	{
		gc = GameObject.Find("GameController").GetComponent<GameController>();
	}

	// Update is called once per frame
	void Update ()
	{
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			Time.timeScale = 1;

			gc.paused = false;
			NGUITools.SetActive(this.gameObject, false);
		}
	}

	public void OnResumeClick()
	{
		Time.timeScale = 1;
		
		gc.paused = false;
		NGUITools.SetActive(this.gameObject, false);
	}

	public void OnMainMenuClick()
	{
		Application.LoadLevel(0);
	}

	public void OnMuteClick()
	{
		NGUITools.SetActive(soundButton, true);
		NGUITools.SetActive(muteButton, false);

		AudioListener.volume = 0;
	}

	public void OnSoundClick()
	{
		NGUITools.SetActive(soundButton, false);
		NGUITools.SetActive(muteButton, true);

		AudioListener.volume = 1;
	}
}

