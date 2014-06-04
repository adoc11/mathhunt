using UnityEngine;
using System.Collections;

public class PauseMenuUI : MonoBehaviour
{

	GameController gc;

	void Start()
	{
		gc = GameObject.Find("GameController").GetComponent<GameController>();
	}

//	// Update is called once per frame
//	void Update ()
//	{
//		if(Input.GetKeyDown(KeyCode.Escape))
//		{
//			Time.timeScale = 1;
//
//			gc.paused = false;
//			NGUITools.SetActive(this.gameObject, false);
//		}
//	}

}

