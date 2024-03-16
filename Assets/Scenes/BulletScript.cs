using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private Vector3 direction;
    private float speed;
    private float lifetime;

    public void ShootInDirection(Vector3 dir, float spd, float lt)
    {
        direction = dir;
        speed = spd;
        lifetime = lt;
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
        lifetime -= Time.deltaTime;
        if (lifetime <= 0f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Jeœli pocisk trafia w gracza lub œcianê, zniszcz go
        if (other.CompareTag("Player") || other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
