/*DestroyOnInvisible
 * 
 * DestroyOnInvisible will use the OnBecameInvisible() function provided by unity to check
 * if an object has left all camera spaces. If it has, then the object is invisible.
 * 
 * 
 * When this has happened, destroy the object that is selected via unity.
 * 
 * If object is null, destroy the object using this script.
 * 
 * Place on anything that needs to be destroyed when it becomes invisible
 * by going off camera.
 */


using UnityEngine;
using System.Collections;

public class DestroyOnInvisible : MonoBehaviour 
{

	public GameObject destroyTarget = null;
	void OnBecameInvisible()
	{
		if (destroyTarget == null) 
		{
			Destroy (gameObject);
		} //end of if
		else 
		{
			Destroy (destroyTarget);
		}//end of else

	}//end of OnBecameInvisible()

}
