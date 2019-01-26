using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {

        var enemy = other.gameObject.GetComponent<Enemy>();

        if (enemy)
        {
            enemy.TakeProjectileHit();
            Destroy(gameObject);
        }

    }
}
