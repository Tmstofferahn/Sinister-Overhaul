/*PlayerShootSineWave
 * 
 * DO NOT USE
 * 
 * PlayerShootSineWave is used to make the bullets look like they are moving
 * in a wave like motion.
 * 
 * This is applied to the barrel shooting the bullets and will cause the initial position
 * of the bullets to move back and forth along a wave. 
 * 
 * Works like the standard PlayerShoot except with added sine wave movement.
 *
 * 
 * 
 * DO NOT USE
 * 
 * Place on Parent Player Barrel objects.
 */

using UnityEngine;
using System.Collections;

public class PlayerBarrelSineWave : MonoBehaviour 
{
	

	public float frequency = 20.0f; 	//how fast it moves back and forth (look up sine waves)... use negative value for a mirrored effect.
	public float amplitude = 0.5f; 		//how far it moves back and forth (look up sine waves)
	private Vector3 pos;				//used to hold new position of the inital bullet location.
	
	// Update is called once per frame
	void Update () 
	{

			pos = transform.position;								//Get position of barrel.
			pos.x += Mathf.Cos (Time.time * frequency)*amplitude;	//apply sine wave movement in X coordinate for the initial position of the bullet
			transform.position = pos;
		
	}//end of Update()

}
