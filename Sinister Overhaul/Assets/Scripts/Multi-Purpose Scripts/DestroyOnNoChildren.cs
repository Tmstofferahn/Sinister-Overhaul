using UnityEngine;
using System.Collections;

public class DestroyOnNoChildren : MonoBehaviour
{

	
	
	// Update is called once per frame
	IEnumerator Start ()
    {

        while (gameObject.transform.childCount > 0)
        {
            yield return 0;
        }

        Destroy(gameObject);
           
    }
}
