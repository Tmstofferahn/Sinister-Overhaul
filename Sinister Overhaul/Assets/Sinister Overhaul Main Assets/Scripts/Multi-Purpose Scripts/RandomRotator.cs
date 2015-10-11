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


}