using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{
    public enum SpawnState { SPAWNING, WAITING, COUNTING };
    public PlayerHealth playerHealth;

	[System.Serializable]
	public class Wave
	{
    
		public string name;
//		public Transform enemy;
		public int count;
		public float rate;
        public int waveWeight;
        
        public Enemies[] spawnEnemies;
           
        [SerializeField]
        public MonoBehaviour factory;
        IFactory Factory { get { return factory as IFactory; } }
        
        public GameObject GetEnemy(int _spawnEnemy) {
            return Factory.FactoryMethod(_spawnEnemy);
        }
	}
    
    [System.Serializable]
    public class Enemies
    {
        public int idx;
        public int enemyWeight;
        public bool isBoss;
    }

	public Wave[] waves;
	private int nextWave = 0;
    public Text waveText;
    public Text scoreText;
	public int NextWave
	{
		get { return nextWave + 1; }
	}

	public Transform[] spawnPoints;

	public float timeBetweenWaves = 5f;
	private float waveCountdown;
	public float WaveCountdown
	{
		get { return waveCountdown; }
	}

	private float searchCountdown = 1f;

	private SpawnState state = SpawnState.COUNTING;
	public SpawnState State
	{
		get { return state; }
	}

	WeaponUpgradeManager weaponUpgradeManager;

	void Start()
	{
        waveText.enabled = true;
        scoreText.enabled = true;
        
		if (spawnPoints.Length == 0)
		{
			Debug.LogError("No spawn points referenced.");
		}

		waveCountdown = timeBetweenWaves;
		weaponUpgradeManager = GameObject.Find("WeaponUpgradeManager").GetComponent<WeaponUpgradeManager> ();
	}

	void Update()
	{
		if (state == SpawnState.WAITING)
		{
			if (!EnemyIsAlive())
			{
				WaveCompleted();
			}
			else
			{
				return;
			}
		}

		if (waveCountdown <= 0)
		{
			if (state != SpawnState.SPAWNING)
			{
				StartCoroutine( SpawnWave ( waves[nextWave] ) );
			}
		}
		else
		{
			waveCountdown -= Time.deltaTime;
		}
	}

	void WaveCompleted()
	{
		Debug.Log("Wave Completed!");

		state = SpawnState.COUNTING;
		waveCountdown = timeBetweenWaves;

		if ((nextWave+1)%3 == 0 && nextWave != 1){
			weaponUpgradeManager.showButtons();
		}

		if (nextWave + 1 > waves.Length - 1)
		{
            Debug.Log("ALL WAVES COMPLETE! Looping...");
            PlayerPrefs.SetInt("wave", nextWave + 1);
			PlayerPrefs.Save();
            
            playerHealth.TakeDamage(playerHealth.currentHealth);
            
			//nextWave = 0;
			
		}
		else
		{
            PlayerPrefs.SetInt("wave", nextWave + 1);
			PlayerPrefs.Save();
			nextWave++;
            
		}


	}

	bool EnemyIsAlive()
	{
		searchCountdown -= Time.deltaTime;
		if (searchCountdown <= 0f)
		{
			searchCountdown = 1f;
			if (GameObject.FindGameObjectWithTag("Enemy") == null)
			{
				return false;
			}
		}
		return true;
	}

	IEnumerator SpawnWave(Wave _wave)
	{
		Debug.Log("Spawning Wave: " + _wave.name);
		state = SpawnState.SPAWNING;
        
        waveText.text = "Wave " + (nextWave+1);

        //New Spawn method
        while (_wave.count < _wave.waveWeight) {
            int enemyIdx = Random.Range (0, _wave.spawnEnemies.Length);
            Enemies spawnEnemy = _wave.spawnEnemies[enemyIdx];
            
            bool shouldSpawn = Random.Range (0, 10) > 5;

            
            if (shouldSpawn && _wave.count + spawnEnemy.enemyWeight <= _wave.waveWeight
                && !spawnEnemy.isBoss) {
                SpawnEnemy(_wave.GetEnemy(spawnEnemy.idx));
                yield return new WaitForSeconds( 1f/_wave.rate );
                _wave.count += spawnEnemy.enemyWeight;
                Debug.Log("Enemy weight: " + spawnEnemy.enemyWeight);
                Debug.Log("Spawned weight: " +_wave.count);
                Debug.Log("Wave weight: " + _wave.waveWeight);
            }
            
            
        }
        
        //Spawn boss at the end of boss waves
        if ((nextWave+1)%3 == 0) {
            SpawnEnemy(_wave.GetEnemy(4));
            yield return new WaitForSeconds( 1f/_wave.rate );
        }
        
        //Reset counter
        _wave.count = 0;

		state = SpawnState.WAITING;

		yield break;
	}

	void SpawnEnemy(GameObject _enemy)
	{
		Debug.Log("Spawning Enemy: " + _enemy.name);

		Transform _sp = spawnPoints[ Random.Range (0, spawnPoints.Length) ];
		Instantiate(_enemy, _sp.position, _sp.rotation);
	}
}
