﻿using UnityEngine;
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

				//gc.updateEquation(inx, value);
				//mp.color = Color.green;
			}

			if((gameObject.tag == "TimeBonus" || gameObject.tag == "ScoreMultiplier") && surface.gameObject.name != "scavengerHuntBoundingBox")
			{
				if(surface.gameObject.name != "bonusDrop")
				{
					surface = null;
					gameObject.transform.position = objectPos;
				}
			}


			if (gameObject.name.Contains("symbol"))
			{
				if(surface.gameObject.name == "scavengerHuntBoundingBox" || surface.gameObject.name == "bonusDrop")
				{
					surface = null;
					gameObject.transform.position = objectPos;
				}
			}

			if (gameObject.name.Contains("shElement"))
			{
				if(surface.gameObject.name == "bonusDrop")
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

		
		if((gameObject.tag == "TimeBonus" || gameObject.tag == "ScoreMultiplier") && surface != null && surface.gameObject.name == "bonusDrop")
		{
			Bonus bonus = gameObject.GetComponent<Bonus>();
			bonus.applyBonus(gameObject.tag);
			UISprite bonusSprite = gameObject.GetComponent<UISprite>();
			bonusSprite.alpha = 255;

			gameObject.collider.enabled = false;
		}

		base.OnDragDropRelease(surface);

		if(gc.validateEquation())
		{
			gc.equationSolved = true;
			//gc.getNextEquation();
		}
		
	}

//	protected override void OnDragDropRelease (GameObject surface)
//	{
//		if(surface != null && surface.gameObject.name.Contains("missingPiece"))
//		{
//			GameController gc = GameObject.Find ("GameController").GetComponent<GameController>();
//
//			int startInx = surface.gameObject.name.LastIndexOf(")")+1;
//			
//			int inx = Convert.ToInt32(surface.gameObject.name.Substring(startInx));
//			
//			string value = gameObject.GetComponent<UILabel>().text;
//
//			UISprite mp = surface.gameObject.GetComponent<UISprite>();
//
//			
//			if (gc.eq[inx].Contains(value))
//			{
//				// Determine which of the symbols in the equation list are still valid
//				// and remove the invalid symbols
//				mCollider.enabled = false;
//				gc.updateEquation(inx, value);
//				gc.validateEquation();
//				mp.color = Color.green;
//			}
//			else
//			{
//				gameObject.transform.position = objectPos;
//				mp.color = Color.black;
//				surface = null;
//			}
//		}
//		else
//		{
//			gameObject.transform.position = objectPos;
//			surface = null;
//		}
//		base.OnDragDropRelease(surface);
//	}
}
