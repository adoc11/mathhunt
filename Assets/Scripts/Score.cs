using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour
{
	public UILabel ScoreLabel;

	public int score { get; set; }

	public void displayScore()
	{
		ScoreLabel.text = "Score: " + score.ToString();
	}
}

