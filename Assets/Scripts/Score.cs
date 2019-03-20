using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

	public static int score;
	Text text;

	void Start()
	{
		text = GetComponent<Text>();
		score=0;
	}

	void Update()
	{
		if(score<1)
		{
			score=0;
		}
		text.text = "" + score;
	}

	public static void addPoints(int value)
	{
		score += value;
	}

	public static void resetScore()
	{
		score=0;
	}
	
}
