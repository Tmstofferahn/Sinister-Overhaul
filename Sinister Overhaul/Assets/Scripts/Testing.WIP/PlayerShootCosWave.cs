/*PlayerShootCosWave
 * 
 * DO NOT USE
 * 
 * PlayerShootSinWave is better at this point. 
 * 
 * PlayerShootCosWave is used to make the bullets look like they are moving
 * in a wave like motion.
 * 
 * This is applied to the barrel shooting the bullets and will cause the initial position
 * of the bullets to move back and forth along a wave. 
 * 
 * Works like the standard PlayerShoot except with added sine wave movement.
 *
 * Works identical to PlayerShootSinWave
 * 
 * DO NOT USE
 * 
 * Place on Parent Player Barrel objects.
 */

using UnityEngine;
using System.Collections;

public class PlayerShootCosWave : MonoBehaviour 
{
	
	
	public GameObject bullet; 			//Create object for the bullet
	public float shotDelay = 0.2f; 		//delay inbetween bullets in seconds.
	private bool readyToShoot = true; 	//value used to allow the delay.
	public float frequency = 20.0f; 	//how fast it moves back and forth (look up cos waves)
	public float amplitude = 0.5f; 		//how far it moves back and forth (look up cos waves)
	public int reverse = 1; 			//reverse the cosine waves initial position (-1 = reverse, 1 = normal)
	private Vector3 pos;
	//used to hold new position of the inital bullet location.
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetButton ("Fire1") && readyToShoot) 
		{
			pos = transform.position;								//Get position of barrel.
			pos.x += reverse * (Mathf.Cos (Time.time * frequency)*amplitude);	//apply sine wave movement in X coordinate for the initial position of the bullet
			Instantiate (bullet, pos, transform.rotation);			//apply position and rotation to spawn of bullet, then spawn bullet.
			readyToShoot = false;									//player just shot, can no longer shoot.
			Invoke ("ResetReadyToShoot", shotDelay);				//calls ResetReadyToShoot to set readyToShoot to true after shotDelay timer.
		}
		
	}//end of Update()
	
	void ResetReadyToShoot()
	{
		readyToShoot = true;
	}//end of ResetReadyToShoot()
}
