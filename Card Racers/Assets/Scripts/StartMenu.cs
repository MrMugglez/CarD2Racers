using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
	public void LoadScene(string sceneName)
	{
		SceneManager.LoadScene(sceneName);
	}

	public void Exit()
	{
		Application.Quit();
	}

	public void ShowObject(GameObject obj)
	{
		obj.SetActive(true);
	}

	public void HideObject(GameObject obj)
	{
		obj.SetActive(false);
	}
}
