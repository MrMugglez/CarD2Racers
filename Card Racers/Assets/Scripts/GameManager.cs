using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public static GameManager instance;
	public SoundManager soundManager;

	public GameObject Racer;

	public List<GameObject> CardDeck = new List<GameObject>();
	public List<GameObject> GameDeck = new List<GameObject>();

	public GameObject CarUI;
	public Image Direction;
	public Slider FuelReserve;

	public GameObject CardMat;
	public int MaxHand = 5;
	[HideInInspector]
	public int currentHand;

	// Use this for initialization
	void Awake ()
	{
		if (instance == null)
		{
			instance = this;
		}
		else
		{
			Destroy(gameObject);
		}
	}

	private void Start()
	{
		CreateDeck();
		Shuffle(GameDeck);
		DealHand();
	}

	// Update is called once per frame
	void Update ()
	{
		FuelReserve.value = Racer.GetComponent<Racer>().CarFuel;
	}

	public static void Shuffle(List<GameObject> list)
	{
		int n = list.Count;
		while (n > 1)
		{
			n--;
			int k = Random.Range(0, n);
			GameObject value = list[k];
			list[k] = list[n];
			list[n] = value;
		}
	}

	void CreateDeck()
	{
		foreach (GameObject card in CardDeck)
		{
			GameDeck.Add(card);
		}
	}

	void DealHand()
	{
		for (int h = 0; h < MaxHand; h++)
		{
			Instantiate(GameDeck[0], CardMat.transform);
			GameDeck.Remove(GameDeck[0]);
		}
	}

	public void DrawCard(GameObject cardToReplace)
	{
		if (GameDeck.Count < 1)
		{
			CreateDeck();
			Shuffle(GameDeck);
		}
		GameObject temp = Instantiate(GameDeck[0], CardMat.transform);
		temp.transform.SetSiblingIndex(cardToReplace.transform.GetSiblingIndex());
		GameDeck.Remove(GameDeck[0]);


		Destroy(cardToReplace);
	}


}
