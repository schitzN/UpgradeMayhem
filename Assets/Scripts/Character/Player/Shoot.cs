using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Shoot : MonoBehaviour
{
    private float _bulletTime = 0;
    private float _curTime = 0;

    private Magazin _mag;
    
    private EnemySpawner _spawner;
    
    // Use this for initialization
    void Start()
    {
        this._mag = GameObject.Find("TheDude").GetComponent<Magazin>();

        //this._bulletTime = 1f / StatsManager.Instance.GetStat("BpS").GetCur();
        
        this._spawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
    }
	
    // Update is called once per frame
    void Update()
    {
        if (this._mag.GetNumBullets() > 0 && this._spawner.GetWaveRunning())
        {
            this._bulletTime = 1f / StatsManager.Instance.GetStat("BpS").GetCur();
            this._curTime += Time.deltaTime;

            if (this._curTime >= this._bulletTime)
            {
                // shoot
                GameObject bullet = Instantiate(Resources.Load("Player/Bullet")) as GameObject;
                bullet.transform.position = this.transform.FindChild("Weapon").position;
                this._mag.RemoveBullet();

                // reset time
                this._curTime = 0;
            }
        } else
            this._curTime = this._bulletTime;
    }
}
