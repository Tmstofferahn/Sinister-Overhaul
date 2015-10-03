/*PlayerBulletMovement
 * 
 * PlayerBulletMovement is the default movement for player bullets.
 * 
 * This causes the bullets to move in a straight line from the 
 * direction the barrel was facing at time of initial shot.
 * 
 * 
 * 
 * Place on Parent Player Bullet objects.
 */

using UnityEngine;
using System.Collections;

public class PlayerBulletMovement : MonoBehaviour 
{

	public float speed = 1.0f; //speed of bullet
	
	// Update is called once per frame
	void Update ()
    {
        transform.position += transform.up * speed * Time.deltaTime;
	
	}//end of Update()
}
