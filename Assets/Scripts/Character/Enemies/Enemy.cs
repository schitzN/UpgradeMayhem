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
    public float _damage;
    public float _knockbackResistance;
    public float _meleeResistance;
    public float _rangedResistance;

    // CHANCES
    public float _chanceOfArtifact;
    public float _chanceOfItem;
    public float _chanceOfRare;

    // TODO: implement enemy types
    // ENEMY TYPE
    //private Enemytype _enemyType;
    //public bool _deflectionClickable;

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
            StatsManager.Instance.AddMoney(this._moneyWorth);
            this._spawner.RemoveEnemy(this);
            Destroy(this.gameObject);
        }
    }
    
    public void HitEnemy(float dmg)
    {
        // TODO:  Resistance checker for weapons

        this._curHealth -= dmg;
        
        Vector3 scale = this._healthBar.transform.localScale;
        this._healthBar.transform.localScale = new Vector3((this._curHealth / this._totalHealth), scale.y, scale.z);
        
        this.UpdateHealth();
    }
}