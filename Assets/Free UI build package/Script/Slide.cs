using Assets.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slide : MonoBehaviour {
	Image Filler;
	public Slider slider;

	PlayerManager player;

	void Start () {
		player = PlayerManager.Instance; 
		Filler = GetComponent<Image>();
	}
	
	void Update () 
	{
		Filler.fillAmount = player.currentHealth / 1000;
	}
}
