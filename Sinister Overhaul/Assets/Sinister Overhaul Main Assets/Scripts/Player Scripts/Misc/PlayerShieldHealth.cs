using UnityEngine;
using System.Collections;

public class PlayerShieldHealth : MonoBehaviour
{

    //public int health;
    //public int currentHealth;
    public float timeAlive = 5.0f;

    void Start()
    {
        //currentHealth = health;
    }
    void OnEnable()
    {
        StartCoroutine(DeactivateAfterSeconds());
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //currentHealth--;
        //if (currentHealth <= 0)
        //{
        //    currentHealth = health;
        //    gameObject.SetActive(false);
        //}

    }

    IEnumerator DeactivateAfterSeconds()
    {
        yield return StartCoroutine(UbhUtil.WaitForSeconds(timeAlive));
        gameObject.SetActive(false);
    }


}
