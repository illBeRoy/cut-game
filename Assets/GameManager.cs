using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class GameManager : MonoBehaviour
{
    [Header("Audio")]
    public AudioClip backgroundMusic;
    public AudioClip endgameCut;

    [Header("Game Settings")]
    public float increaseDifficultyEveryXSeconds = 10;
    public float increaseDifficultyBy = 1.0f;
    public float minimalSpawnInterval = .5f;

    private AudioSource audioSource;
    private FollowerEnemySpawner[] followerEnemySpawners;
    private BomberEnemySpawner[] bomberEnemySpawners;
    private float timeSinceLastDifficultyIncrease = 0;

    void PlayBackgroundMusic()
    {
        this.audioSource.clip = this.backgroundMusic;
        this.audioSource.loop = true;
        this.audioSource.Play();
    }

    void StopBackgroundMusic()
    {
        this.audioSource.Stop();
    }

    void IncreaseDifficulty()
    {
        for (int i = 0; i < this.followerEnemySpawners.Length; i += 1) {
            if (this.followerEnemySpawners[i].spawnInterval > this.minimalSpawnInterval) {
                this.followerEnemySpawners[i].spawnInterval = Mathf.Max(this.followerEnemySpawners[i].spawnInterval - this.increaseDifficultyBy, this.minimalSpawnInterval);
            }
        }

        for (int i = 0; i < this.bomberEnemySpawners.Length; i += 1) {
            if (this.bomberEnemySpawners[i].spawnInterval > this.minimalSpawnInterval) {
                this.bomberEnemySpawners[i].spawnInterval = Mathf.Max(this.bomberEnemySpawners[i].spawnInterval - this.increaseDifficultyBy, this.minimalSpawnInterval);
            }
        }

        this.timeSinceLastDifficultyIncrease = 0;
    }

    void ClearAllEnemies()
    {
        var enemyObjects = GameObject.FindObjectsOfType<Enemy>();
        for (int i = 0; i < enemyObjects.Length; i += 1) {
            Destroy(enemyObjects[i].gameObject);
        }

        for (int i = 0; i < this.followerEnemySpawners.Length; i += 1) {
            Destroy(this.followerEnemySpawners[i].gameObject);
        }

        for (int i = 0; i < this.bomberEnemySpawners.Length; i += 1) {
            Destroy(this.bomberEnemySpawners[i].gameObject);
        }
    }

    IEnumerator EndgameRoutine()
    {
        yield return new WaitForSeconds(3);
        this.audioSource.volume = 1f;
        this.audioSource.PlayOneShot(this.endgameCut);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("GameOver");
    }

    public void EndGame()
    {
        this.StopBackgroundMusic();
        this.ClearAllEnemies();
        this.StartCoroutine(this.EndgameRoutine());
    }

    // Start is called before the first frame update
    void Start()
    {
        this.audioSource = this.GetComponent<AudioSource>();
        this.PlayBackgroundMusic();
        this.followerEnemySpawners = FindObjectsOfType<FollowerEnemySpawner>();
        this.bomberEnemySpawners = FindObjectsOfType<BomberEnemySpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        this.timeSinceLastDifficultyIncrease += Time.deltaTime;
        if (this.timeSinceLastDifficultyIncrease > this.increaseDifficultyEveryXSeconds) {
            this.IncreaseDifficulty();
        }
    }
}
