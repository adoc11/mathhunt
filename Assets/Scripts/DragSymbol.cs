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
		if(surface != null && surface.gameObject.name.Contains("missingPiece"))
		{
			GameController gc = GameObject.Find ("GameController").GetComponent<GameController>();

			int startInx = surface.gameObject.name.LastIndexOf(")")+1;
			
			int inx = Convert.ToInt32(surface.gameObject.name.Substring(startInx));
			
			string value = gameObject.GetComponent<UILabel>().text;

			UISprite mp = surface.gameObject.GetComponent<UISprite>();

			
			if (gc.eq[inx].Contains(value))
			{
				// Determine which of the symbols in the equation list are still valid
				// and remove the invalid symbols
				mCollider.enabled = false;
				gc.updateEquation(inx, value);
				gc.validateEquation();
				mp.color = Color.green;
			}
			else
			{
				gameObject.transform.position = objectPos;
				mp.color = Color.black;
				surface = null;
			}
		}
		else
		{
			gameObject.transform.position = objectPos;
			surface = null;
		}
		base.OnDragDropRelease(surface);
	}
}
