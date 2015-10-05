using UnityEngine;
using System.Collections;

public class DeathBarrier : MonoBehaviour {


    void OnTriggerExit2D(Collider2D col)
    {
        if (col.transform.gameObject.tag == "PlayerBullet" | col.transform.gameObject.tag == "EnemyBullet")
        {
            UbhObjectPool.Instance.ReleaseGameObject(col.transform.parent.gameObject);
        }
        if (col.transform.gameObject.tag == "Enemy")
        {
            Destroy(col.transform.gameObject);
        }

    }

}
