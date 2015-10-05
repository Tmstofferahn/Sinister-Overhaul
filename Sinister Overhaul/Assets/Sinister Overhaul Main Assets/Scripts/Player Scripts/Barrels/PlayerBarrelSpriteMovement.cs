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
	private PlayerShootSineWave pssw; 	//Get script information
    private PlayerShootSineWaveUBH psswUBH;
	private Vector3 pos;								//Setup position to hold the position of bullet spawns
	public GameObject barrel = null;					//Get barrel gameobject
	public float spriteDistanceFromBullet = 0.3f;		//distance sprite will be (y coordinate) from bullets.

	void Update () 
	{
        if (GameControl.control.isPaused == true)
            return;
		if (barrel != null) //if no barrel object, the sprite will not do anything.
		{

            if (pssw = barrel.GetComponent<PlayerShootSineWave>())
            {
                //Get script info from barrel object
                pos.x = pssw.Pos.x;                                 //get sinewave movement pos
                pos.y = pssw.Pos.y - spriteDistanceFromBullet;      //get y coordinate +/- position behind bullets.
                pos.z = pssw.Pos.z;
                transform.position = pos;                           //apply to transform of sprite
            }
            if(psswUBH = barrel.GetComponent<PlayerShootSineWaveUBH>())
            {
                //Get script info from barrel object
                pos.x = psswUBH.Pos.x;                                 //get sinewave movement pos
                pos.y = psswUBH.Pos.y - spriteDistanceFromBullet;      //get y coordinate +/- position behind bullets.
                pos.z = psswUBH.Pos.z;
                transform.position = pos;                           //apply to transform
            }
        									
		}
	}
}
