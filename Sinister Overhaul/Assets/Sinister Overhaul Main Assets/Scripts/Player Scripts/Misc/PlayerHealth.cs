/*PlayerHealth
 * 
 * Controls the player health and lives.
 * 
 * Player can take damage, if that happens it will become immune to damage
 * for a short period of time. During this time, the player will flash. 
 * After the time is up, the player will revert to normal.
 * 
 * Place only on Parent Player object.
 */

using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public AudioSource audio;
    public AudioClip onPickupSFX;
    public GameObject shield;
	public float invulnerableLength = 3.0f;
	private bool invulnerable = false;
	public GameObject hitEffect;
	public GameObject deathEffect;
	public Color[] collisionColor = {Color.white, Color.black};
	private Material mat;



	// Use this for initialization
	void Start ()
	{
        if(GameControl.control.currentHealth <=0 )
        {
            GameControl.control.currentHealth = GameControl.control.initialHealth;
        }
		mat = GetComponentInChildren<MeshRenderer> ().material;
        GameControl.control.playerInvulnerable = false;
	}

	void OnTriggerEnter2D(Collider2D col) //Ensure that triggers are set to 2D
	{
       
        if(col.transform.gameObject.tag == "Enemy" | col.transform.gameObject.tag == "EnemyBullet")
        {
            if (invulnerable == false && GameControl.control.loadNextLevel == false && shield.activeSelf == false && GameControl.control.playerInvulnerable == false)
            {

                GameControl.control.currentHealth--;

                Instantiate(hitEffect, transform.position, Quaternion.identity);
                if (GameControl.control.currentHealth <= 0)
                {
                    GameControl.control.currentLives--;
                    Instantiate(deathEffect, transform.position, Quaternion.identity);
                    Destroy(gameObject);

                }
                invulnerable = true;
                Invoke("ResetInvulnerable", invulnerableLength);
                StartCoroutine(Flash(invulnerableLength, 0.0f));
            }
        }
        if(col.transform.gameObject.tag == "Pickup")
        {
            audio.PlayOneShot(onPickupSFX);
            UbhObjectPool.Instance.ReleaseGameObject(col.transform.gameObject);
            if (GameControl.control.playerUpgradeLevel < GameControl.control.playerUpgradeLevelMax)
            {
                GameControl.control.playerUpgradeLevel++;
                GameControl.control.score += 100;
                return;
            }
            else if (GameControl.control.currentHealth < 3)
            {
                GameControl.control.currentHealth++;
                GameControl.control.score += 500;
                return;
            }
            else if(GameControl.control.currentLives < 3)
            {
                GameControl.control.currentLives++;
                GameControl.control.score += 1000;
                return;
            }
            else
            {
                GameControl.control.score += 5000;
            }
                
        }

	}
	void ResetInvulnerable()
	{
		mat.color = Color.white;
		invulnerable = false;
	}
	//Flashes when the player takes damage and become invulnerable
	IEnumerator Flash(float time, float intervalTime)
	{
		float elapsedTime = 0.0f;
		int index = 0;
		while (elapsedTime < time) 
		{
			mat.color = collisionColor[index %2];

			elapsedTime += Time.deltaTime;
			index++;
			yield return new WaitForSeconds(intervalTime);
		}

	}
}