using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Racer : MonoBehaviour
{
    private bool m_isMoving = false;

    [SerializeField]
    private float m_speed = 5f;
    [SerializeField]
	private float m_turnSpeed = 5f;
    private int m_carFuel = 0;
    [HideInInspector]
    public int CarFuel { get { return m_carFuel; } set { m_carFuel = value; } }
    [SerializeField]
	private float m_recoveryTiltX = 45f;

	public Card.Directions currentDirection = Card.Directions.Straight;
    
	void Update()
	{
		StopFlipping();
		if (!m_isMoving && m_carFuel > 0)
		{
			Move(m_carFuel, currentDirection);
		}
	}

	public IEnumerator Move(int fuel,  Card.Directions direction)
	{
		m_isMoving = true;
		currentDirection = direction;	

		if (m_carFuel > 100)
		{
			m_carFuel = 100;
		}
		Debug.Log("current fuel is " + m_carFuel);
		Debug.Log("current direction is " + direction);

		GameManager.instance.soundManager.CarSound(direction);

		if (direction == Card.Directions.Straight)
		{
			while (fuel > 0)
			{
				transform.Translate(Vector3.forward * m_speed * Time.deltaTime);
				fuel--;
				m_carFuel--;

				Debug.Log("current fuel is " + m_carFuel);
				yield return null;
			}
			
		}
		else
		{
			Vector3 rotation = DirectionAndFuelToVector3(fuel, direction);
			Vector3 newAngle = new Vector3(transform.eulerAngles.x + rotation.x, transform.eulerAngles.y + rotation.y, transform.eulerAngles.z + rotation.z);
			Vector3 currentAngle = transform.eulerAngles;
			float timer = 0f;
			float completion = 0f;
			while (completion <= 1)
			{
				timer += Time.deltaTime;
				completion = timer * m_turnSpeed;
				transform.eulerAngles = Vector3.Lerp(currentAngle, newAngle, completion);

				yield return null;
			}
			Debug.Log("Finished turning");
			m_carFuel -= fuel;
			fuel = 0;
		}
		m_isMoving = false;
	}

	private IEnumerator BurnFuel(int fuel)
	{
		while(m_isMoving)
		{
			yield return new WaitForSeconds(0.1f);
		}
		StartCoroutine(Move(fuel, currentDirection));
	}

    // Order Cards call this Function
	public void AddFuel(int fuel)
	{
		m_carFuel += fuel;
		if (currentDirection != Card.Directions.None)
		{
			StartCoroutine(BurnFuel(fuel));
		}
		else
		{
			m_carFuel += fuel;
		}
	}

	private Vector3 DirectionAndFuelToVector3(int fuel, Card.Directions direction)
	{
		Vector3 rotation = new Vector3(0,0,0);
		float fuelMath = fuel;
		fuelMath /= 100;
		if (direction == Card.Directions.Left)
		{
			rotation = new Vector3(0, -180 * fuelMath, 0);
		}
		else if (direction == Card.Directions.Right)
		{
			rotation = new Vector3(0, 180 * fuelMath, 0);
		}
		
		return rotation;
	}
	
    //hacky way of stopping the car from flipping over doing certain actions
	private void StopFlipping()
	{
		if (transform.rotation.x > m_recoveryTiltX || transform.rotation.x < -m_recoveryTiltX)
		{
			transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(0, transform.eulerAngles.y, transform.eulerAngles.z), Time.deltaTime * 5);
		}
	}
}

