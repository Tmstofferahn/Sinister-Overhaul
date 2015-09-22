using UnityEngine;
using System.Collections;

public class EnemyAsteroidMovement : MonoBehaviour {

    public float speed;

    void Start()
    {
        GetComponent<Rigidbody>().velocity = transform.up * -speed;
    }
}
