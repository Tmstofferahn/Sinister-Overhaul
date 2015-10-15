using UnityEngine;
using System.Collections;

public class PlayerShieldHealth : MonoBehaviour
{

    //public int health;
    //public int currentHealth;
    private float timeAlive = 5.0f;

    void Start()
    {
        //currentHealth = health;
        timeAlive = GameControl.control.shieldTimeAlive;
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
        if (col.transform.gameObject.tag == "EnemyBullet")
        {
            GameControl.control.score += 10;
            UbhObjectPool.Instance.ReleaseGameObject(col.transform.parent.gameObject);
        }

    }

    IEnumerator DeactivateAfterSeconds()
    {
        yield return StartCoroutine(UbhUtil.WaitForSeconds(timeAlive));
        gameObject.SetActive(false);
    }


}
