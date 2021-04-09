using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditorInternal;
using UnityEngine;

public class CoinMovmenet : MonoBehaviour
{

	public float CoinSpeed = 2f;

	void FixedUpdate ()
	{
		GetComponent<Rigidbody2D>().velocity = new Vector2(CoinSpeed,GetComponent<Rigidbody2D>().velocity.y);
	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			//Game manager
			//Add score
			Destroy(this.gameObject);
		}
	}
}
