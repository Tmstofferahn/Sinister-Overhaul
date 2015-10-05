﻿/* EnemyHealth
 * 
 * EnemyHealth is used to initialize an enemy with a health pool. When the enemy is hit
 * it will lose health. Upon reaching zero health it will destroy itself
 * 
 * Upon taking damage it will create a particle effect at the point of collision.
 * 
 * Upon reaching zero it will create a particle effect at the transform of the enemy.
 * 
 * 
 * If the enemy is set to be a boss, it will load the next level.
 * 
 * Use this on any enemy Parent object that will have a health pool that doesn't die
 * on a single hit.
 * 
 * 
 */
using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
    public int initialHealth = 80;                      //Starting hp for enemy.
    public int currentHealth;							//Tracks current health of enemy
    public bool destructible = true;
    public GameObject hitEffect = null;                 //onHit particle effect to be used when enemy is hit.
    public GameObject deathEffect = null;				//onDeath particle effect to be used when enemy is destroyed
    public GameObject meshOfObject;
    private Color onHitColor;
    private Color normalColor;
    private bool isFlashing = false;
    private Renderer render;
    public int scoreOnHit = 10;
    public int scoreOnDeath = 100;

    // Use this for initialization
    void Start()
    {
        SetupMaterial();

        scoreOnHit = Mathf.RoundToInt(scoreOnHit * GameControl.control.difficultyFactor);
        scoreOnDeath = Mathf.RoundToInt(scoreOnDeath * GameControl.control.difficultyFactor);
        initialHealth = Mathf.RoundToInt(initialHealth * GameControl.control.difficultyFactor);
        currentHealth = initialHealth;                  //Set current hp to what the initial hp is.
    }

    void OnTriggerEnter2D(Collider2D col) //Ensure that triggers are set to 2D
    {
        if (col.transform.gameObject.tag == "PlayerBullet")
        {

            UbhSimpleBullet bullet = col.transform.parent.GetComponent<UbhSimpleBullet>();
            UbhObjectPool.Instance.ReleaseGameObject(col.transform.parent.gameObject);

            if (destructible == true)
            {
                currentHealth -= bullet._Power; ; //on hit, reduce hp by 1.
                GameControl.control.score += scoreOnHit;
                if (isFlashing == false)
                {
                    StartCoroutine(FlashOnHit());
                }

                if (hitEffect != null) //if there is an onHit particle available, create it at point of impact.
                {
                    UbhObjectPool.Instance.GetGameObject(hitEffect, col.transform.position, Quaternion.identity);
                }


                if (currentHealth <= 0) //when the enemy's health is zero or less
                {
                    GameControl.control.score += scoreOnDeath;
                    if (deathEffect != null) //if there is an onDeath particle effect available, create it at transform of enemy.
                    {
                        Instantiate(deathEffect, col.transform.position, Quaternion.identity);
                    }

                    Destroy(gameObject); //destroy the enemy object

                }
            }
        }





    }
    void SetupMaterial()
    {

        if (meshOfObject == null)
        {
            if (transform.Find("Mesh") == true)
            {
                meshOfObject = transform.Find("Mesh").gameObject;
                normalColor = meshOfObject.GetComponent<Renderer>().material.color;
            }

        }
        if (GetComponent<Renderer>() != null)
        {
            render = GetComponent<Renderer>();
            normalColor = render.material.color;
        }

        if (meshOfObject != null)
        {
            meshOfObject.GetComponent<Renderer>();
            normalColor = meshOfObject.GetComponent<Renderer>().material.color;
        }

        onHitColor = Color.red;
    }
    IEnumerator FlashOnHit()
    {
        if(render != null)
        {
            isFlashing = true;
            render.material.color = onHitColor;
            yield return new WaitForSeconds(0.2f);
            render.material.color = normalColor;
            isFlashing = false;
        }
        if (meshOfObject != null)
        {
            isFlashing = true;
            meshOfObject.GetComponent<Renderer>().material.color = onHitColor;
            yield return new WaitForSeconds(0.2f);
            meshOfObject.GetComponent<Renderer>().material.color = normalColor;
            isFlashing = false;
        }
    }


}
