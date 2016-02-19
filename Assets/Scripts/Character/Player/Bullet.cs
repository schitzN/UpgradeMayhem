using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    // bullet hit
    private float _deathAni = 0.05f;
    private bool _isDead = false;

    // Use this for initialization
    void Start()
    {

    }
	
    // Update is called once per frame
    void Update()
    {
        // destroy when dead
        if (this._isDead)
        {
            _deathAni -= Time.deltaTime;

            if (this._deathAni <= 0)
                Destroy(this.gameObject);
        } else
        {
            this.transform.position = new Vector3(this.transform.position.x + Time.deltaTime * StatsManager.Instance.GetStat("BulletSpeed").GetCur(), this.transform.position.y, this.transform.position.z);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // get hit
        Enemy hitEnemy = ((Enemy)other.GetComponent("Enemy"));

        // update health
        hitEnemy.HitEnemy((int)StatsManager.Instance.GetStat("Damage").GetCur());

        // set recoil
        other.transform.position = new Vector3(other.transform.position.x + StatsManager.Instance.GetStat("Recoil").GetCur(), other.transform.position.y, other.transform.position.z);

        // toggle dead
        this._isDead = true;
        this.transform.FindChild("explosion").gameObject.SetActive(true);
        this.transform.FindChild("bullet").gameObject.SetActive(false);
    }
}
