using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject[] hazards;

	public GUIText scoreText;
	public GUIText restartText;
	public GUIText gameOverText;

	public Vector3 spawnValues;

	public float spawnWait = 0.5f;
	public float startWait = 1.0f;

	private int score = 0;
	private bool gameOver = false;
	private bool restart = false;

	void Start() {
		UpdateScore();
		StartCoroutine(SpawnWaves());
	}

	void Update() {
		if (restart && Input.GetKeyDown(KeyCode.R)) {
			Application.LoadLevel(Application.loadedLevel);
		}
	}

	IEnumerator SpawnWaves() {
		yield return new WaitForSeconds(startWait);
		while(!gameOver) {
			GameObject hazard = hazards[Random.Range(0, hazards.Length)];
			Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
			Quaternion spawnRotation = Quaternion.identity;
			Instantiate(hazard, spawnPosition, spawnRotation);
			yield return new WaitForSeconds(spawnWait);
		}

		restartText.text = "Press 'R' for Restart";
		restart = true;
	}

	public void AddScore(int value) {
		score += value;
		UpdateScore();
	}

	void UpdateScore() {
		scoreText.text = "Score: " + score;
	}

	public void GameOver() {
		gameOverText.text = "GAME OVER!";
		gameOver = true;
	}
}
