using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Shooter : MonoBehaviour
{
    [Header("Gun Behavior")]
    public Transform shootingPoint;
    public int shotsPerSecond = 1;
    public AudioClip shootingSound;

    [Header("Projectile Behavior")]
    public GameObject projectile;
    public float range = 2;
    public float speed = 10;

    private float cooldownLeft = 0;
    private List<ProjectileData> projectilesData = new List<ProjectileData>();
    private AudioSource audioSource;

    public void Shoot(Vector2 direction)
    {
        if (this.cooldownLeft <= 0) {
            this.audioSource.PlayOneShot(this.shootingSound);
            this.CreateProjectile(direction);
            this.cooldownLeft = 1f / this.shotsPerSecond;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        this.audioSource = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.cooldownLeft > 0) {
            this.cooldownLeft -= Time.deltaTime;
        }
        this.updateProjectiles();
    }

    private void CreateProjectile(Vector2 direction)
    {
        Quaternion projectileDirection = Quaternion.Euler(0, 0, Mathf.Rad2Deg * Mathf.Atan2(direction.y, direction.x));
        GameObject projectile = Instantiate(this.projectile, this.shootingPoint.position, projectileDirection);

        ProjectileData projectileData = new ProjectileData();
        projectileData.gameObject = projectile;
        projectileData.direction = direction;
        projectileData.originPoint = projectile.transform.position;

        this.projectilesData.Add(projectileData);
    }

    private void updateProjectiles()
    {
        int i = 0;
        while (i < this.projectilesData.Count) {
            ProjectileData projectileData = this.projectilesData[i];

            if (projectileData.gameObject != null) {
                projectileData.gameObject.transform.position += projectileData.direction * this.speed * Time.deltaTime;
            } else {
                this.projectilesData.RemoveAt(i);
                continue;
            }


            if (Vector2.Distance(projectileData.gameObject.transform.position, projectileData.originPoint) > this.range) {
                Destroy(projectileData.gameObject);
                this.projectilesData.RemoveAt(i);
            } else {
                i += 1;
            }
        }
    }
}

struct ProjectileData {
    public GameObject gameObject;
    public Vector3 direction;
    public Vector2 originPoint;
}
