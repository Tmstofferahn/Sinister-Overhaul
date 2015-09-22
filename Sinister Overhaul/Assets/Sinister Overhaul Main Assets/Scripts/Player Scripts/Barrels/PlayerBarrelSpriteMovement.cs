/*PlayerBarrelSpriteMovement
 * 
 * PlayerBarrelSpriteMovement takes the position of whereh the bullets are shot from and 
 * moves the sprite of the barrel to match that position on the x coordinate plane.
 * This is mainly only needed for barrels using PlayerShootSineWave.
 * 

 * Place on Player Barrel Sprite objects.
 */

using UnityEngine;
using System.Collections;

public class PlayerBarrelSpriteMovement : MonoBehaviour
{
	// Update is called once per frame
	private PlayerShootSineWave playerShootSineWave; 	//Get script information
	private Vector3 pos;								//Setup position to hold the position of bullet spawns
	public GameObject barrel = null;					//Get barrel gameobject
	public float spriteDistanceFromBullet = 0.3f;		//distance sprite will be (y coordinate) from bullets.

	void Update () 
	{
		if (barrel != null) //if no barrel object, the sprite will not do anything.
		{
			playerShootSineWave = barrel.GetComponent<PlayerShootSineWave> (); //Get script info from barrel object
			pos.x = playerShootSineWave.Pos.x;									//get sinewave movement pos
			pos.y = playerShootSineWave.Pos.y - spriteDistanceFromBullet;		//get y coordinate +/- position behind bullets.
			pos.z = playerShootSineWave.Pos.z;
			transform.position = pos;											//apply to transform of sprite
		}
	}
}
