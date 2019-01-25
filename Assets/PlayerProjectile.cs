using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        var followerEnemy = other.gameObject.GetComponent<FollowerEnemy>();
        if (followerEnemy)
        {
            followerEnemy.TakeProjectileHit();
            Destroy(gameObject);
        }

    }
}
