/*EnemyShoot1
 * 
 * This is the first iteration of the enemy shoot command.
 * 
 * EnemyShoot1 will allow the enemy to shoot based upon a shotDelay.
 * 
 * It will work based upon a looping structure by calling the shoot
 * command within the shoot command.
 * 
 * 
 * Place on Enemy Barrel objects.
 */


using UnityEngine;
using System.Collections;

public class EnemyShootWithForce : MonoBehaviour 
{
	
	public Rigidbody2D bullet;		//GameObject of bullet being used.
	public float shotDelay = 0.2f; 	//delay (in seconds) how long the shot will be delayed before shooting again.
	
	
	void Start()
	{
		
		Invoke ("ReadyToShoot", shotDelay); //Calls ReadyToShoot with a shotDelay
	}//end of Start()
	
	void ReadyToShoot()
	{
		Instantiate (bullet.transform, transform.position, transform.rotation); //Sets bullet up based upon position/rotation of object shooting (ie barrels)
		Invoke ("ReadyToShoot", shotDelay); //loop the call for endless firing
		
	}//end of ResetReadyToShoot
}
