using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    private StatTracker _st;

    // bullet hit
    private float _deathAni = 0.05f;
    private bool _isDead = false;

    // Use this for initialization
    void Start()
    {
        this._st = GameObject.Find("StatTracker").GetComponent<StatTracker>();
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
            this.transform.position = new Vector3(this.transform.position.x + Time.deltaTime * this._st.GetStat("BulletSpeed").GetCur(), this.transform.position.y, this.transform.position.z);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // get hit
        Enemy hitEnemy = ((Enemy)other.GetComponent("Enemy"));

        // update health
        hitEnemy.HitEnemy((int)this._st.GetStat("Damage").GetCur());

        // set recoil
        other.transform.position = new Vector3(other.transform.position.x + this._st.GetStat("Recoil").GetCur(), other.transform.position.y, other.transform.position.z);

        // toggle dead
        this._isDead = true;
        this.transform.FindChild("explosion").gameObject.SetActive(true);
        this.transform.FindChild("bullet").gameObject.SetActive(false);
    }
}
