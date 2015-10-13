using UnityEngine;
using System.Collections;

public class PickupController : MonoBehaviour {

    public float speed = 2.0f;
    public float rotationSpeed = 75.0f;
    private GameObject mesh;
	// Use this for initialization
	void Start () {
        mesh = transform.GetChild(0).gameObject;
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.position -= transform.up * speed * Time.deltaTime;
        mesh.transform.Rotate(Vector3.up * (rotationSpeed * Time.deltaTime));
	
	}
}
