using UnityEngine;

public class Explosion : MonoBehaviour
{
    private bool hasAlreadyHitPlayer = false;
    private const int LIFE_TIME_IN_SECONDS = 2;
    private float timeSinceCreated = 0;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasAlreadyHitPlayer) {
            return;

        }

        var playerController = other.gameObject.GetComponent<PlayerController>();
        if (playerController )
        {
            float distance = Vector2.Distance(transform.position, playerController.transform.position);
            if (distance > 1)
                return;


            playerController.TakeHit(1);
        }

    }

    void Update()
    {
        this.timeSinceCreated += Time.deltaTime;
        if (this.timeSinceCreated > LIFE_TIME_IN_SECONDS) {
            Destroy(this.gameObject);
        }
    }
}
