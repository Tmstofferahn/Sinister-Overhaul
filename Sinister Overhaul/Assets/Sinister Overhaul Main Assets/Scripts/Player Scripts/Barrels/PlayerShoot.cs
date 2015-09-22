/* PlayerShoot
 * 
 * This is the first iteration of the Player shoot command.
 * 
 * PlayerShoot will allow the player to shoot based upon a shotDelay.
 * 
 * Calculates current position of barrel and applies that to the initial spawn of the bullet.
 * Creates the spawn of the bullet.
 * Then makes the player wait based upon a timed delay using shotDelay and ResetReadyToShoot.
 * 
 * Place on Parent Player Barrel objects.
 */

using UnityEngine;
using System.Collections;

public class PlayerShoot : MonoBehaviour 
{

	public GameObject bullet;
	public float shotDelay = 0.2f;
	private bool readyToShoot = true;
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetButton ("Fire1") && readyToShoot && GameControl.control.isPaused == false) 
		{
			Instantiate (bullet, transform.position, transform.rotation);
			readyToShoot = false;
			Invoke ("ResetReadyToShoot", shotDelay);
		}
	
	}//end of Update()

	void ResetReadyToShoot()
	{
		readyToShoot = true;
	}//end of ResetReadyToShoot()
}
