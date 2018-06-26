using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{

	public GameObject endScreen;

	// Use this for initialization
	void Start ()
	{
		if (endScreen != null)
		{
			endScreen.SetActive(false);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerEnter(Collider coll)
	{
		if (coll.gameObject.tag == "Player")
		{
			StartCoroutine(FinishGame());
		}
	}

	IEnumerator FinishGame()
	{
		while (Time.timeScale > 0)
		{
			Time.timeScale -= Time.deltaTime;
			if (Time.timeScale < 0.5f)
			{
				endScreen.SetActive(true);
				GameManager.instance.CardMat.SetActive(false);
			}
			yield return null;
		}
	}

	public void LoadScene(string sceneName)
	{
		SceneManager.LoadScene(sceneName);
	}
}
