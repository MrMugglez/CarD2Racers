using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Credits : MonoBehaviour
{
	ScrollRect credits;

	// Use this for initialization
	void Start ()
	{
		credits = GetComponent<ScrollRect>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (credits.verticalNormalizedPosition > 0 && !Input.GetMouseButton(0))
		{
			credits.verticalNormalizedPosition -= Time.deltaTime * 0.1f;
		}
	}
}
