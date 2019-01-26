using UnityEngine;

public class Explosion : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        var playerController = other.gameObject.GetComponent<PlayerController>();
        if (playerController )
        {
            float distance = Vector2.Distance(transform.position, playerController.transform.position);
            if (distance > 1)
                return;


            playerController.TakeHit(1- distance);
        }

    }
}
