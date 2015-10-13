/* EnemyHealth
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

        SetUp();
    }
    void OnEnable()
    {
        if(Application.loadedLevel >= 1 && Application.loadedLevel < Application.levelCount -1)
        {
            SetUp();
        }
        
    }
    void OnTriggerEnter2D(Collider2D col) //Ensure that triggers are set to 2D
    {
        if (col.transform.gameObject.tag == "PlayerBullet")
        {
            
            UbhSimpleBullet bullet = col.transform.parent.GetComponent<UbhSimpleBullet>();
            UbhObjectPool.Instance.ReleaseGameObject(bullet.gameObject);



            if (destructible == true)
            {
                GameControl.control.totalBulletHits++;
                int randomNumber = 0;
                if (GameControl.control.playerUpgradeLevel < 1)
                {
                    randomNumber = Random.Range(1, 40);
                }
                else if (GameControl.control.playerUpgradeLevel == 1)
                {
                    randomNumber = Random.Range(1, 60);
                }
                else if (GameControl.control.playerUpgradeLevel == 2)
                {
                    randomNumber = Random.Range(1, 100);
                }
                else if (GameControl.control.playerUpgradeLevel >= GameControl.control.playerUpgradeLevelMax)
                {
                    if(GameControl.control.currentHealth == 1)
                    {
                        randomNumber = Random.Range(1, 1300);
                    }
                    else if (GameControl.control.currentHealth == 2)
                    {
                        randomNumber = Random.Range(1, 1600);
                    }
                    else if (GameControl.control.currentHealth == 3)
                    {
                        randomNumber = Random.Range(1, 2000);
                    }
                    
                }

                if (randomNumber == 1)
                {
                    UbhObjectPool.Instance.GetGameObject(GameControl.control.healthPickup, transform.position, Quaternion.identity);
                }
                if (GameControl.control.shieldReady == false && (GameControl.control.shieldTimeRemaining / GameControl.control.shieldTimeAlive) <= 0)
                {
                    GameControl.control.shieldEnergyCurrent++;
                }
                currentHealth -= bullet._Power; ; //on hit, reduce hp by 1.
                GameControl.control.score += scoreOnHit * bullet._Power;
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
                        UbhObjectPool.Instance.GetGameObject(deathEffect, col.transform.position, Quaternion.identity);
                    }
                    if(transform.parent != null && transform.parent.parent != null)
                    {
                        if (transform.parent.parent.gameObject.tag == "EnemyBullet")
                        {
                            render.material.color = normalColor;
                            UbhObjectPool.Instance.ReleaseGameObject(transform.parent.parent.gameObject);
                        }
                        else
                        {
                            Destroy(gameObject); //destroy the enemy object
                        }

                    }
                    else
                    {
                        Destroy(gameObject); //destroy the enemy object
                    }


                }
            }
        }





    }
    void SetUp()
    {

        SetupMaterial();

        scoreOnDeath = Mathf.RoundToInt(scoreOnDeath * GameControl.control.difficultyFactor);
        initialHealth = Mathf.RoundToInt(initialHealth * GameControl.control.difficultyFactor);
        currentHealth = initialHealth;                  //Set current hp to what the initial hp is.


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
