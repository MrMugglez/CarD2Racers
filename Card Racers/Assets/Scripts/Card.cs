using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour {

	public int Fuel;
	public enum Directions
	{
		Left,
		Straight,
		Right,
		None
	}
	public Directions Direction;

	public GameObject DirectionPanel;
	public GameObject FuelPanel;

	Racer player;


	GraphicRaycaster gRay;
	PointerEventData data;
	EventSystem eSystem;

	// Use this for initialization
	void Start ()
	{
		if (Direction != Directions.None)
		{
			Texture2D temp = (Texture2D)Resources.Load("Sprites/" + Direction.ToString());
			DirectionPanel.GetComponent<Image>().sprite = Sprite.Create(temp, new Rect(0,0, temp.width, temp.height), new Vector2(0.5f, 0.5f));
		}
		else
		{
			DirectionPanel.SetActive(false);
		}
		FuelPanel.GetComponentInChildren<Text>().text = Fuel.ToString() + "%";

		gRay = GetComponent<GraphicRaycaster>();
		eSystem = GetComponent<EventSystem>();

		player = GameManager.instance.Racer.GetComponent<Racer>();
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Mouse0))
		{
			Clicking("left");
		}
		if (Input.GetKeyDown(KeyCode.Mouse1))
		{
			Clicking("right");
		}
	}

	void Clicking(string mouseButton)
	{

		data = new PointerEventData(eSystem);
		data.position = Input.mousePosition;

		List<RaycastResult> results = new List<RaycastResult>();

		gRay.Raycast(data, results);

		foreach (RaycastResult result in results)
		{
			Debug.Log("Hit " + result.gameObject.name);
			if (result.gameObject == gameObject && mouseButton == "left")
			{
				Debug.Log(mouseButton);


				if (Direction != Card.Directions.None)
				{
					player.GetComponent<Racer>().CarFuel += Fuel;
					player.StartCoroutine(player.Move(Fuel, Direction));
					Texture2D temp = (Texture2D)Resources.Load("Sprites/" + Direction.ToString());
					GameManager.instance.Direction.sprite = Sprite.Create(temp, new Rect(0, 0, temp.width, temp.height), new Vector2(0.5f, 0.5f));

				}
				else
				{
					player.AddFuel(Fuel);
				}
				GameManager.instance.DrawCard(gameObject);

			}
			else if (result.gameObject == gameObject && mouseButton == "right")
			{
				player.AddFuel(Fuel);

				
				GameManager.instance.DrawCard(gameObject);
			}
		}

	}
}
