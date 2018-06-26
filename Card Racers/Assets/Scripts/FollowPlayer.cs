using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

	public float FollowSpeed = 5f;
	GameObject player;

	// Use this for initialization
	void Start ()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		transform.position = player.transform.position;
		transform.eulerAngles = player.transform.eulerAngles;
	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.position = Vector3.Lerp(transform.position, player.transform.position, Time.deltaTime * FollowSpeed);
		transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, player.transform.eulerAngles, Time.deltaTime * FollowSpeed);
	}
}
