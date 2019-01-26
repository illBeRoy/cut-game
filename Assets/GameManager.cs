using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class GameManager : MonoBehaviour
{
    public AudioClip backgroundMusic;
    public AudioClip endgameCut;

    private AudioSource audioSource;

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

    void ClearAllEnemies()
    {
        var enemyObjects = GameObject.FindObjectsOfType<Enemy>();
        for (int i = 0; i < enemyObjects.Length; i += 1) {
            Destroy(enemyObjects[i].gameObject);
        }

        var followerSpawners = GameObject.FindObjectsOfType<FollowerEnemySpawner>();
        for (int i = 0; i < followerSpawners.Length; i += 1) {
            Destroy(followerSpawners[i].gameObject);
        }

        var bomberSpawners = GameObject.FindObjectsOfType<BomberEnemySpawner>();
        for (int i = 0; i < bomberSpawners.Length; i += 1) {
            Destroy(bomberSpawners[i].gameObject);
        }
    }

    IEnumerator EndgameRoutine()
    {
        yield return new WaitForSeconds(5);
        this.audioSource.PlayOneShot(this.endgameCut);
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
