using UnityEngine;
using System.Collections;
using System;
using NCalc;

public class DragSymbol : UIDragDropItem {

	Vector3 objectPos;

	protected override void OnDragDropStart()
	{
		objectPos = gameObject.transform.position;

		base.OnDragDropStart();
	}

	protected override void OnDragDropRelease (GameObject surface)
	{
		GameController gc = GameObject.Find ("GameController").GetComponent<GameController>();

		if(surface != null)
		{
			if(surface.gameObject.name.Contains("slot") && gameObject.tag == "ScavHuntElement")
			{
				int startInx = surface.gameObject.name.LastIndexOf("t")+1;
				
				int inx = Convert.ToInt32(surface.gameObject.name.Substring(startInx));
				
				string value = gameObject.GetComponent<UILabel>().text;
				
				UISprite mp = surface.gameObject.GetComponent<UISprite>();

				gameObject.transform.localRotation = Quaternion.identity;
			}
		
			if (gameObject.name.Contains("shElement"))
			{
				if(surface.gameObject.name == "bonusDrop" || surface.gameObject.name.Contains("shElement") || surface.gameObject.name.Contains("symbol"))
				{
					surface = null;
					gameObject.transform.position = objectPos;
				}
			}

			if (gameObject.name.Contains("symbol"))
			{
				if(surface.gameObject.name == "scavengerHuntBoundingBox" || surface.gameObject.name.Contains("shElement") || surface.gameObject.name.Contains("symbol") || surface.gameObject.name == "bonusDrop")
				{
					surface = null;
					gameObject.transform.position = objectPos;
				}
			}
		}
		else
		{
			gameObject.transform.position = objectPos;
		}


		base.OnDragDropRelease(surface);

		AudioSource audio = gameObject.audio;
		if(audio.isPlaying)
			audio.Stop();

		audio.Play();

		if(gc.validateEquation())
			gc.equationSolved = true;
	}
}
