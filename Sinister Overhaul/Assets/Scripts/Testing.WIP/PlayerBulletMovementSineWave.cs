/*PlayerBulletMovement
 * 
 * DO NOT USE
 * 
 * Testing still.
 * 
 * Main use is meant to cause bullets to move left and right when moving in the upwards direction
 * based upon a sine wave movement.
 * 
 * DO NOT USE
 * 
 */

using UnityEngine;
using System.Collections;

public class PlayerBulletMovementSineWave : MonoBehaviour 
{

	public float speed = 1.0f; //speed of the bullet

	public float frequency = 20.0f; //how fast it moves back and forth (look up sine waves)
	public float amplitude = 0.5f; //how far it moves back and forth (look up sine waves)

	private Vector3 axis;

	private Vector3 pos;

	void Start()
	{
		pos = transform.position;
		//DestroyObject (gameObject, 1.0f);
		axis = transform.right;

	}

	// Update is called once per frame
	void Update () 
	{
		pos += transform.up * Time.deltaTime * speed; //
		transform.position = pos + axis * Mathf.Sin (Time.time * frequency) * amplitude;
		
	}//end of Update()
}
