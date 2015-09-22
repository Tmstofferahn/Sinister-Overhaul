/*EnemyShootSineWave
 * 
 * EnemyShootSineWave is used to make the bullets look like they are moving
 * in a wave like motion.
 * 
 * This is applied to the barrel shooting the bullets and will cause the initial position
 * of the bullets to move back and forth along a wave. 
 * 
 * Works like the standard PlayerShoot except with added sine wave movement.
 *
 * 
 * Place on Enemy Barrel objects.
 */

using UnityEngine;
using System.Collections;

public class EnemyShootSine : MonoBehaviour 
{
	public GameObject bullet = null; 			//Create object for the bullet
	
	public float shotDelay = 0.2f; 				//delay inbetween bullets in seconds.
	private bool readyToShoot = true; 			//value used to allow the delay.
	
	public float frequency = 20.0f; 			//how fast it moves back and forth (look up sine waves)... use negative value for a mirrored effect.
	public float amplitude = 1.0f; 				//how far it moves back and forth (look up sine waves)
	
	private Vector3 pos;						//used to hold new position of the inital bullet location.
	
	void Start()
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
		pos = transform.position;									//Get position of barrel.
		pos.x += Mathf.Sin (Time.time * frequency)*amplitude;	//apply sine wave movement in X coordinate for the initial position of the bullet
		if (readyToShoot) 
		{
			Instantiate (bullet, pos, transform.rotation);			//apply position and rotation to spawn of bullet, then spawn bullet
			readyToShoot = false;									//player just shot, can no longer shoot.
			Invoke ("ResetReadyToShoot", shotDelay);				//calls ResetReadyToShoot to set readyToShoot to true after shotDelay timer.
		}
		
	}//end of Update()
	
	void ResetReadyToShoot()
	{
		readyToShoot = true;
	}//end of ResetReadyToShoot()
}
