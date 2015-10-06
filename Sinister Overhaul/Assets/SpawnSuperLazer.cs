using UnityEngine;
using System.Collections;

public class SpawnSuperLazer : MonoBehaviour {

    public GameObject superLazer;

    void OnEnable()
    {
        Instantiate(superLazer, transform.position, transform.rotation);
    }
	
}
