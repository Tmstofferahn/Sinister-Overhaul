/*EnemyBulletMovement
 * 
 * This script is for enemy bullets. This is a default movement that causes the bullets to move
 * in a straight line based upon the direction of what is shooting them.
 * 
 * Generally, set up a barrel on the enemy that will shoot and position the barrel to
 * shoot in a direction
 * 
 * The bullets will move in that direction.
 * 
 * 
 * 
 * 
 * Place on Parent Bullet objects.
 */



using UnityEngine;
using System.Collections;

public class EnemyBulletMovement : MonoBehaviour 
{
	
	public float speed = 1.0f; //speed of bullet

	void Start()
	{
		GetComponent<Rigidbody2D> ().AddForce (transform.up * speed * 25 );


	}
	// Update is called once per frame
	void Update () 
	{

		
	}//end of Update()
}
