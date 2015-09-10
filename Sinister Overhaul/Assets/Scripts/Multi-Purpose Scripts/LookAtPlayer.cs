/*LookAtPlayer
 * 
 * LookAtPlayer is designed to look at the current position of the player
 * by rotating the object this is attatched towards the player.
 * 
 * The object this is put on will instantly look at the current location of the player, each update.
 * This call is currently a work in progress, however, it is function.
 * The code is not completely efficient, but it works for its intended purpose.
 *
 *
 * Place on any object to look at player.
 */
using UnityEngine;
using System.Collections;

public class LookAtPlayer : MonoBehaviour 
{
	private GameObject player;
	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindWithTag ("Player");


	}
	
	// Update is called once per frame
	void Update () 
	{
		if (player) 
		{
			Vector3 lookAtPosition = player.transform.position - transform.position;
			lookAtPosition = new Vector3 (lookAtPosition.x, lookAtPosition.y, 0.0f);
			transform.rotation = Quaternion.LookRotation (lookAtPosition, Vector3.forward);
			transform.Rotate (90.0f, 0.0f, 0.0f);
		} 


		//Vector3 lookAtPosition = player.transform.position;
		//transform.LookAt (lookAtPosition, transform.up);
	}
}
