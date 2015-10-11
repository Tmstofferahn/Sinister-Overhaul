using UnityEngine;
using System.Collections;

public class DeathBarrier : MonoBehaviour {


    void OnTriggerExit2D(Collider2D col)
    {
        if (col.transform.gameObject.tag == "PlayerBullet" | col.transform.gameObject.tag == "EnemyBullet")
        {
            for(int i = 0; i < col.transform.parent.childCount; i++)
            {
                col.transform.parent.GetChild(i).gameObject.SetActive(true);
            }
            UbhObjectPool.Instance.ReleaseGameObject(col.transform.parent.gameObject);
        }
        if (col.transform.gameObject.tag == "Enemy")
        {
            if(col.transform.parent.parent.tag == "EnemyBullet")
            {
                UbhObjectPool.Instance.ReleaseGameObject(col.transform.parent.parent.gameObject);
            }
            else
            {
                Destroy(col.transform.gameObject);
            }

        }



    }

}
