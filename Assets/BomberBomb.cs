using UnityEngine;

public class BomberBomb : MonoBehaviour
{
    public GameObject explosion;

    private void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.GetComponent<BomberBomb>())
            return;

        if (other.gameObject.GetComponent<BomberEnemy>())
            return;

        Debug.Log(other.gameObject.name);

        Instantiate(explosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }

}
