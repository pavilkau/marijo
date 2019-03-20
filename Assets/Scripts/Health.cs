using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {

	private PlayerController player;
	public Sprite[] hearts;
	public Image HealthUI;

	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
	}

	void Update()
	{
		HealthUI.sprite = hearts[player.getPlayerHealth()];
	}
}
