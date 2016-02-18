using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    // TIMER
    private float _t = 0;
    private float _cooldownRound = 3f;
    private float _cooldown = 2f;
    private bool _roundRunning = false;
    private Text _warmupTimer;
    private Text _warmupWave;
    
    // WAVE
    private List<Enemy> _enemies;
    private int _wave = 0;
    private int _maxSpawn = 2;
    private int _leftSpawn = 0;
    private int _kills = 0;
    private Text _waveText;
    private Text _waveLeft;
	
    // Use this for initialization
    void Start()
    {
        this._enemies = new List<Enemy>();
        this._warmupTimer = GameObject.Find("WarmUpTimer").GetComponent<Text>();
        this._warmupWave = GameObject.Find("WarmUpWave").GetComponent<Text>();
        this._waveText = GameObject.Find("Wave").GetComponent<Text>();
        this._waveLeft = GameObject.Find("EnemiesLeft").GetComponent<Text>();
        
        this._waveLeft.gameObject.SetActive(false);
        this._waveText.gameObject.SetActive(false);
    }
	
    // Update is called once per frame
    void Update()
    {
        this._t += Time.deltaTime;
        
        if (this._roundRunning)
            this.UpdateWave();
        else
            this.UpdateWarmUp();
    }
    
    private void UpdateWarmUp()
    {
        this._warmupTimer.text = "Wave " + (this._wave + 1) + " in " + (this._cooldownRound - this._t).ToString("0.0");
        
        if (this._t >= this._cooldownRound)
        {
            this._roundRunning = true;
            this._t = 0;

            this.SetNextWave();
        }
    }
    
    private void UpdateWave()
    {
        // spawn new enemies
        if (this._t >= this._cooldown)
        {
            if (this._leftSpawn > 0)
            {
                // spawn creep
                GameObject creep = Instantiate(Resources.Load("Enemies/0")) as GameObject;
                creep.transform.position = this.transform.position;
                this._enemies.Add(creep.GetComponent<Enemy>());
                this._leftSpawn--;
            }
            
            // reset time
            this._t = 0;
        }
        
        if (this._leftSpawn == 0 && this._enemies.Count == 0)
        {
            this._roundRunning = false;
            this.ToggleText();
        }
    }
    
    private void SetNextWave()
    {
        this._kills = 0;
        this._wave++;
        this._maxSpawn = this._wave;
        this._leftSpawn = this._maxSpawn;
        
        this._waveText.text = "Wave: " + this._wave.ToString();
        this._waveLeft.text = "Enemies left: " + (this._maxSpawn);
        // TODO Set enemies for this wave
        this.ToggleText();
    }
    
    private void ToggleText()
    {
        this._waveLeft.gameObject.SetActive(!this._waveLeft.gameObject.activeSelf);
        this._waveText.gameObject.SetActive(!this._waveText.gameObject.activeSelf);
        this._warmupTimer.gameObject.SetActive(!this._warmupTimer.gameObject.activeSelf);
        this._warmupWave.gameObject.SetActive(!this._warmupWave.gameObject.activeSelf);
    }
    
    public void RemoveEnemy(Enemy e)
    {
        this._enemies.Remove(e);
        this._kills++;
        this._waveLeft.text = "Enemies left: " + (this._maxSpawn - this._kills);
    }
    
    public int GetCurrentWave()
    {
        return this._wave;
    }
    public bool GetWaveRunning()
    {
        return this._roundRunning;
    }
}
