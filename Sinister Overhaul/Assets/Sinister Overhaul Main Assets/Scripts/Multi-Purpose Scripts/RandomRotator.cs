using UnityEngine;
using System.Collections;

public class RandomRotator : MonoBehaviour
{
    public float tumble;
    private Vector3 currentAngVel;


    void Start()
    {
        currentAngVel = GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * tumble;
    }
    void OnEnable()
    {
        currentAngVel = GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * tumble;
    }
    void Update()
    {
        if (GameControl.control.isPaused == true)
        {
            GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
        }
        if (GameControl.control.isPaused == false)
        {
            GetComponent<Rigidbody>().angularVelocity = currentAngVel;
        }


    }

}