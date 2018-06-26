using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoverCar : MonoBehaviour
{
	public float MoveSpeed = 5f;
	public Transform dropOffLocation;
	Vector3 dropOff;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnCollisionEnter(Collision coll)
	{
		if (coll.gameObject.tag == "Player")
		{
			StartCoroutine(MoveUp(coll.gameObject));
		}
	}

	IEnumerator MoveUp(GameObject player)
	{
		player.GetComponent<Rigidbody>().isKinematic = true;

		Vector3 startPos = player.transform.position;
		float timer = 0f;
		while (player.transform.position.y < 4f)
		{
			timer += Time.deltaTime;
			player.transform.position = Vector3.Lerp(startPos, new Vector3(startPos.x, 4f, startPos.z), timer * MoveSpeed);
			yield return null;
		}
		StartCoroutine(MoveToDestination(player));
	}

	IEnumerator MoveToDestination(GameObject player)
	{
		dropOff = dropOffLocation.position;
		Vector3 startPos = player.transform.position;
		float timer = 0f;
		while (player.transform.position != dropOff)
		{
			timer += Time.deltaTime;
			player.transform.position = Vector3.Lerp(startPos, dropOff, timer * MoveSpeed);
			yield return null;
		}
		player.GetComponent<Rigidbody>().isKinematic = false;
	}
}
