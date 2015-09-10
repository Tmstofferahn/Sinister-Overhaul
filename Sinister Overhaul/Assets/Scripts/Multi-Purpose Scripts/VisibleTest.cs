/*VisibleTest
 * 
 * VisibleTest tests if an object is visible within a camera. This is used to test
 * any object that might be going out of view or something of the sort.
 * 
 * If an object becomes visible it will post a message in the debug log 
 * saying "Object" can be seen now.
 * 
 * If an object becomes invisible it will post a message in the debug log
 * saying "Object" can *not* be seen anymore.
 * 
 * Place on any object to test visibility.
 */

using UnityEngine;
using System.Collections;

public class VisibleTest : MonoBehaviour 
{
	
	public void OnBecameInvisible()
	{
		Debug.Log("'" + name + "' can *not* be seen anymore.");
	}//end of OnBecameInvisible()
	
	public void OnBecameVisible()
	{
		Debug.Log("'" + name + "' can be seen now.");
	}//end of OnBecameVisible

	
}