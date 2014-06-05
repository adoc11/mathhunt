using UnityEngine;
using System.Collections;

public class GameSceneUI : MonoBehaviour
{
	public GameObject muteButton;
	public GameObject soundButton;
	public GameObject PausePanel;
	public GameObject pauseButton;
	public GameObject resumeButton;

	GameController gc;

	void Start()
	{
		gc = GameObject.Find("GameController").GetComponent<GameController>();
	}

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			if(!gc.paused)
			{
				Time.timeScale = 0;
				
				gc.paused = true;
				NGUITools.SetActive(PausePanel, true);
				NGUITools.SetActive(pauseButton, false);
				NGUITools.SetActive(resumeButton, true);
			}
			else
			{
				Time.timeScale = 1;
				
				gc.paused = false;
				NGUITools.SetActive(PausePanel, false);
				NGUITools.SetActive(pauseButton, true);
				NGUITools.SetActive(resumeButton, false);
			}
		}
	}

	public void OnResetClick()
	{
		ScavengerHuntArea scavHunt = GameObject.Find("ScavengerHuntPanel").GetComponent<ScavengerHuntArea>();

		Timer timer = GameObject.Find("Timer").GetComponent<Timer>();

		if(timer.timeInSeconds > 10)
			timer.timeInSeconds -= 10;

		Bonus bonus = GameObject.Find("ScoreTimerEquationHistoryPanel").GetComponent<Bonus>();

		if(GameObject.Find("bonusTimer") != null)
		{
			BonusTimer bt = GameObject.Find("bonusTimer").GetComponent<BonusTimer>();
			bt.timeInSeconds = 0;
			bt.bonusOver = true;
		}

		bonus.pickRandomBonus();

		scavHunt.populateScavHunt(40);


	}

	public void OnMainMenuClick()
	{
		Application.LoadLevel(0);
	}

	public void OnResumeClick()
	{
		Time.timeScale = 1;
		
		gc.paused = false;
		NGUITools.SetActive(PausePanel, false);
		NGUITools.SetActive(pauseButton, true);
		NGUITools.SetActive(resumeButton, false);
	}

	public void OnPauseClick()
	{
		gc.paused = true;
		Time.timeScale = 0;
		NGUITools.SetActive(PausePanel, true);
		NGUITools.SetActive(pauseButton, false);
		NGUITools.SetActive(resumeButton, true);
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
		
		AudioListener.volume = 0.5f;
	}
}

