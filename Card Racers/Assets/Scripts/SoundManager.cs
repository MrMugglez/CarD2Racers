using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

	AudioSource racer;

	public AudioClip CarStraight;
	public AudioClip CarTurn;
	public AudioClip CarCrash;

	// Use this for initialization
	void Start ()
	{
		racer = GameManager.instance.Racer.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void CarSound(Card.Directions dir = Card.Directions.None)
	{
		/* Disable for now
		if (dir == Card.Directions.Straight)
		{
			racer.clip = CarStraight;
			racer.Play();
		}
		else if (dir == Card.Directions.Left || dir == Card.Directions.Right)
		{
			racer.clip = CarTurn;
			racer.Play();
		}
		else 
		{
			racer.clip = CarCrash;
			racer.Play();
		}*/
	}
}
