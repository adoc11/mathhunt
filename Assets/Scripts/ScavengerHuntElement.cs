using UnityEngine;
using System.Collections;

public class ScavengerHuntElement : MonoBehaviour {

	public bool isCorrectElement { get; set; }
	
	Vector3 objectPos;
	// Use this for initialization
	void Start () {
	
	}

	public void trackPositionStart()
	{
		objectPos = gameObject.transform.position;
	}

	public void trackPositionEnd()
	{
		if (!isCorrectElement)
			gameObject.transform.position = objectPos;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
