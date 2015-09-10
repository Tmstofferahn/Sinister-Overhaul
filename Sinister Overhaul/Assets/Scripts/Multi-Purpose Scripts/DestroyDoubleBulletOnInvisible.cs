/*DestroyDoubleBulletOnInvisible
 * 
 * This class will allow bullets to be paired with multiple bullets as childs of a bullet tree.
 * For instance, PlayerLaser has two sprites it spawns. In order to destroy itself, both bullets should be destroyed first.
 * 
 * This class will check to see if those bullets are destroyed, if so, it will destroy the parent bullet tree.
 * Bullets that are destroyed or became invisible become null. Null means it was destroyed.
 * 
 * 
 * This is only meant for instances of at least one bullet that are part of a class of bullets.
 * 
 * 
 * The bullets are initialized as null, unless otherwise assigned an object in Unity. 
 * More bullets may be added, if they are part of the same bullet tree.
 * 
 * Place on Parent Bullet object.
*/

using UnityEngine;
using System.Collections;


public class DestroyDoubleBulletOnInvisible : MonoBehaviour 
{

	public GameObject bullet1 = null; //First bullet
	public GameObject bullet2 = null; //second bullet
	
	// Update is called once per frame
	void Update () 
	{
		if (bullet1 == null && bullet2 == null) //when both are null, destroy the gameObject (main bullet parent)
			Destroy (gameObject);
	
	}//end of Update()
}
