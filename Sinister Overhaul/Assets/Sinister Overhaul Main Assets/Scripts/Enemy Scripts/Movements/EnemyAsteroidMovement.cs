using UnityEngine;
using System.Collections;

public class EnemyAsteroidMovement : MonoBehaviour {

    public float speed;
    private Vector3 tempVelocity;
    private bool isSleeping;

    void Start()
    {
        GetComponent<Rigidbody>().velocity = transform.up * -speed;
    }
    void Update()
    {
        if (GameControl.control.isPaused == true)
        {
            if (isSleeping == false)
            {
                tempVelocity = GetComponent<Rigidbody>().velocity;
                GetComponent<Rigidbody>().Sleep();
                isSleeping = true;
            }
            return;
        }
        if (GameControl.control.isPaused == false)
        {
            if (isSleeping == true)
            {

                GetComponent<Rigidbody>().WakeUp();
                GetComponent<Rigidbody>().velocity = tempVelocity;
                isSleeping = false;
            }

        }
    }

}
