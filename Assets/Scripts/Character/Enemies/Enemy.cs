using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    // PARENT
    private EnemySpawner _spawner;
    
    // STATS 
    public int _totalHealth;
    public int _moneyWorth;
    public float _movementSpeed;
    public int _damage;

    // CURRENT
    private float _curHealth;

    // GAMEOBJECTS
    public GameObject _healthBar;
    
    void Start()
    {
        this._spawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
        this._curHealth = this._totalHealth;
    }
    
    void Update()
    {
        this.UpdateMovement();
        this.UpdateHealth();
    }
    
    private void UpdateMovement()
    {
        this.transform.position = new Vector3(this.transform.position.x - Time.deltaTime * this._movementSpeed, this.transform.position.y, this.transform.position.z);
		
        // player reached
        if (this.transform.position.x < -6.5f)
        {
            GameObject.Find("TheDude").GetComponent<PlayerHealth>().HitPlayer(this._damage);
            this._spawner.RemoveEnemy(this);
            Destroy(this.gameObject);
        }
    }

    private void UpdateHealth()
    {
        if (this._curHealth <= 0)
        {
            GameObject.Find("StatTracker").GetComponent<StatTracker>().AddMoney(this._moneyWorth);
            this._spawner.RemoveEnemy(this);
            Destroy(this.gameObject);
        }
    }
    
    public void HitEnemy(int dmg)
    {
        this._curHealth -= dmg;
        
        Vector3 scale = this._healthBar.transform.localScale;
        this._healthBar.transform.localScale = new Vector3((this._curHealth / this._totalHealth), scale.y, scale.z);
        
        this.UpdateHealth();
    }
}