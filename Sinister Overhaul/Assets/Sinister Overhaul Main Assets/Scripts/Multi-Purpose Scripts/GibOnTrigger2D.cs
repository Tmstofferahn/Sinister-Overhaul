/*GibOnTrigger2D
 * 
 * GibOnTrigger2D is a collision detection that destroys objects as well as does GibEffects on objects.
 * 
 * A Gib is essentially what happens when something is hit with enough force it ruptures 
 * (explosions from bullets hitting an enemy, player explosion when shot, etc).
 * 
 * OnTriggerEnter2D() is used specifically for detecting when an object enters a trigger
 * detection of a 2D collider.
 * 
 * The gib will first see if there is a gibEffect that will be applied when the object using 
 * GibOnTrigger2D detects a collision, it will then play that particle effect.
 * 
 * Once the gibEffect is played on that location, the object that has the script for
 * GibOnTrigger2D will destroy itself.
 * 
 * Place on any object that should be destroyed on a collision/trigger a gibEffect on collision.
 */

using UnityEngine;
using System.Collections;

public class GibOnTrigger2D : MonoBehaviour
{
	public GameObject gib = null;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void OnTriggerEnter2D()
	{
		if(gib != null)
		{
			Instantiate(gib,transform.position,gib.transform.rotation); //apply transform to gib effect if available
		}//end of if
		Destroy(gameObject);

	}//end of OnTriggerEnter2D()
}
