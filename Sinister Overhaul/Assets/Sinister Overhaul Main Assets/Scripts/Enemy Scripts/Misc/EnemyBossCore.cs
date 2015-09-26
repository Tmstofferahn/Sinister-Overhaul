using UnityEngine;
using System.Collections;

public class EnemyBossCore : MonoBehaviour {

    public bool isMainCore = true;
    public GameObject nextBossPhase;
    public GameObject[] hulls;
    public GameObject[] turrets;
    public GameObject[] cargo;
    public GameObject smokeTrailTurrets;
    public GameObject smokeTrailCargo;
    public GameObject hullDeathEffect;
    bool turretsDestroyed = false;
    bool cargoDestroyed = false;
    bool hullsDestroyed = false;
    

	// Use this for initialization
	void Start ()
    {
	
	}

    // Update is called once per frame
    void Update()
    {


        if (turretsDestroyed == false)
        {
            turretsDestroyed = CheckIfPartsDestroyed(turrets, smokeTrailTurrets, 0, turretsDestroyed);
        }
        if (cargoDestroyed == false)
        {
            cargoDestroyed = CheckIfPartsDestroyed(cargo, smokeTrailCargo, 1, cargoDestroyed);
        }
        if (hullsDestroyed == false)
        {
            DestroyHulls();
        }
        if(hullsDestroyed == true && nextBossPhase != null)
        {
            GameObject newBoss = Instantiate(nextBossPhase, transform.position, transform.rotation) as GameObject;
            newBoss.transform.parent = transform.parent;
            Destroy(gameObject);
        }


       

    }
    void DestroyHulls()
    {
        if (turretsDestroyed == true && cargoDestroyed == true)
        {
            if (isMainCore == true)
            {
                for (int i = 0; i < hulls.Length; i++)
                {
                    if (hulls[i] != null)
                    {
                        Instantiate(hullDeathEffect, hulls[i].transform.position, Quaternion.identity);
                        Destroy(hulls[i]);
                        
                    }

                }
                hullsDestroyed = true;
            }

        }
    }
    bool CheckIfPartsDestroyed(GameObject[] part, GameObject smokeTrail, int type, bool partDestroyed)
    {
        int objectCount = cargo.Length;

        if (partDestroyed == false)
        {
            for (int i = 0; i < part.Length; i++)
            {
                if (part[i] == null)
                {
                    objectCount--;
                }
                if (objectCount == 0)
                {
                    if (isMainCore == false)
                    {
                        SpawnSmokeTrails(smokeTrail, type);
                    }
                    return partDestroyed = true;
                }
            }
           
        }
        return partDestroyed = false;
    }

    void SpawnSmokeTrails(GameObject smokeTrail, int type)
    {
        if(type == 0)
        {
            
            GameObject smokeTurret1 = Instantiate(smokeTrail, new Vector3(transform.position.x -5,
                transform.position.y, transform.position.z -1), Quaternion.identity) as GameObject;

            smokeTurret1.transform.parent = transform;


            GameObject smokeTurret2 = Instantiate(smokeTrail, new Vector3(transform.position.x + 5,
                transform.position.y, transform.position.z - 1), Quaternion.identity) as GameObject;

            smokeTurret2.transform.parent = transform;


        }
        if(type == 1)
        {
            GameObject smokeCargo = Instantiate(smokeTrail, new Vector3(transform.position.x,
                transform.position.y, transform.position.z - 1), Quaternion.identity) as GameObject;

            smokeCargo.transform.parent = transform;

        }
    }
}
