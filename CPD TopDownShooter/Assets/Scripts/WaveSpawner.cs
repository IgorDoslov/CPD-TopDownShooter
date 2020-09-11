using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WaveSpawner : MonoBehaviour
{


	public enum SpawnState
	{ 
		SPAWNING,
		WAITING,
		COUNTING
	};

	public Text theWaveText;
	//public Text highscoreText;


	[System.Serializable]				// allows us to change values inside of inspector
	public class Wave
	{
		public string name;				//name of wave
		public GameObject enemy;		// refrence to prehab we want to initiate
		public int count;				// amount of waves
		public float rate;				// spawn rate

	}

	public Wave[] waves;
	private int nextWave = 0;
	public int NextWave
	{
		get { return nextWave + 1; }
	}

	public Transform[] spawnPoints;

	public float timeBetweenWaves = 5f;			//time between waves 5 seconds
	private float waveCountdown;				
	public float WaveCountdown
	{
		get { return waveCountdown; }
	}

	private float searchCountdown = 1f;			//instantiate to check if all enemy are dead every 1 second instead of every frame

	private SpawnState state = SpawnState.COUNTING;
	public SpawnState State
	{
		get { return state; }
	}
		
	void Start()
	{
		theWaveText.text = "Hello";
		//highscoreText.text = "Highscore : "  + PlayerPrefs.GetFloat("Highscore").ToString() ;			//Gets highscore save and loads into string(word form)

		if (spawnPoints.Length == 0)								
		{
			Debug.LogError("No spawn points referenced.");						// checks if a spawn point of refrenced
		}

		waveCountdown = timeBetweenWaves;										//how long till next wave starts default 5seconds
	}
		
	void Update()
	{
		theWaveText.text = waves [nextWave].name;

		if (state == SpawnState.WAITING)										//checks if enimies are still alive
		{
			if (!EnemyIsAlive())
			{
				WaveCompleted();												// if all enimies dead start next wave
			}
			else
			{
				return;															// if enemies still alive return till all enimies dead
			}
		}

		if (waveCountdown <= 0)													//if <=0 then start check if spawning of not start spawning then else
		{
			if (state != SpawnState.SPAWNING)	
			{
				StartCoroutine( SpawnWave ( waves[nextWave] ) );				//start spawning wave
			}
		}
		else
		{
			waveCountdown -= Time.deltaTime;									//makes countdown relevent to time and not frames per second
		}
		//{
		//	if (PlayerPrefs.GetFloat ("Highscore") < nextWave)					//if Highscore number is less then wave number 
		//		PlayerPrefs.SetFloat ("Highscore", nextWave); 					//set Highscore to the wave number
		//}
	}

	void WaveCompleted()
	{
		Debug.Log("Wave Completed!");

		state = SpawnState.COUNTING;
		waveCountdown = timeBetweenWaves;										

		if (nextWave + 1 > waves.Length - 1)									// if no more waves start wave 1 again looping
		{
			nextWave = 0;
			Debug.Log("ALL WAVES COMPLETE! Looping...");						// if final wave not completed add 1 wave
		}
		else
		{
			nextWave++;															//increases wave by 1
		}
	}

	bool EnemyIsAlive()														
	{
		searchCountdown -= Time.deltaTime;									
		if (searchCountdown <= 0f)
		{
			searchCountdown = 1f;											//check if all enemy are dead every 1 second instead of every frame
			if (GameObject.FindGameObjectWithTag("Giant") == null &&
			GameObject.FindGameObjectWithTag("Boss") == null)				//checks if all enimies with the player tag enemy are alive or dead
			{
				return false;												// if enemy alive repeat step till enemies are dead check this every 1 second
			}
		}
		return true;														// if all enemy with tag enemy are dead then return true
	}

	IEnumerator SpawnWave(Wave _wave)
	{
		Debug.Log("Spawning Wave: " + _wave.name);
		state = SpawnState.SPAWNING;							//spawns enimes

		for (int i = 0; i < _wave.count; i++)					// = number of enimies we want to spawn
		{
			Debug.Log("Spawning Enemy: " + _wave.enemy.name);

			Instantiate(_wave.enemy, spawnPoints[i].position, _wave.enemy.transform.rotation);


																//waits for player to finish killing enimies
			yield return new WaitForSeconds( 1f/_wave.rate );	//how long we want to wait 1 / rate untill we have number of enimies we want to spawn
		}

		state = SpawnState.WAITING;

		yield break;
	}
}
	




 