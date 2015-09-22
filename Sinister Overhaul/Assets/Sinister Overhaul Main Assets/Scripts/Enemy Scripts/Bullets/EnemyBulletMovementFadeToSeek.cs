/*EnemyBulletMovementFadeToSeek
 * 
 * This script is for enemy bullets. This movement allows bullets to first shoot out of the barrel
 * in the corresponding direction. After a delay, the bullet will face the player and
 * go after their location after the delay is up. It does not home on current position,
 * rather it homes on the position the player was at when it turned.
 * 
 * Speeds during initial shot and seek shot can be set.
 * Delays between each can be set.
 * 
 * If the player is NULL the bullets will move straight, like a normal enemy bullet.
 * 
 * Place on Parent Bullet objects.
 */using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyBulletMovementFadeToSeek : MonoBehaviour 
{
	private GameObject player = null;
	public float speedStart = 1.0f; //speed of bullet upon start
	public float speedChase = 1.0f; //speed of bullet after targeting player.
	private float speed;
	public float timedDelay = 2.0f;

	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindWithTag ("Player");
		speed = speedStart;	//set speed to start speed.
		if (player) 
		{
			StartCoroutine (SeekPlayer ()); //seek player position.
		}
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.position += transform.up * speed * Time.deltaTime; //Makes the bullet move in a line.
	
	}

	IEnumerator SeekPlayer() //seeks players position, changes speed, and starts after timed delay.
	{
		yield return new WaitForSeconds(timedDelay);
		if (player) 
		{
			Vector3 lookAtPosition = player.transform.position - transform.position;
			lookAtPosition = new Vector3 (lookAtPosition.x, lookAtPosition.y, 0.0f);
			transform.rotation = Quaternion.LookRotation (lookAtPosition, Vector3.forward);
			transform.Rotate (90.0f, 0.0f, 0.0f);
			speed = speedChase;
		}
	}
}
