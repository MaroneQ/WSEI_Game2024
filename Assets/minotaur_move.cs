using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minotaur_move : MonoBehaviour
{
    public float speed = 3f; // Pr�dko�� poruszania si� bossa
    public float turnRadius = 1f; // Promie� skr�tu bossa
    public float obstacleDetectionDistance = 1f; // Odleg�o�� detekcji przeszk�d
    public LayerMask obstacleLayer; // Warstwa przeszk�d
    public LayerMask minotaurLayer; // Warstwa zawieraj�ca minotaur�w

    private Vector2 currentDirection; // Aktualny kierunek bossa
    private CapsuleCollider2D turnCollider; // Collider reprezentuj�cy promie� skr�tu


    void Start()
    {
        currentDirection = Vector2.right; // Pocz�tkowy kierunek bossa (prawo)

        // Dodaj i skonfiguruj CapsuleCollider2D jako collider reprezentuj�cy promie� skr�tu
        turnCollider = gameObject.AddComponent<CapsuleCollider2D>();
        turnCollider.size = new Vector2(turnRadius * 2f, 0.1f);
        turnCollider.direction = CapsuleDirection2D.Horizontal;
        turnCollider.isTrigger = true;
    }

    void Update()
    {
        Move(); // Wywo�anie funkcji poruszania w funkcji Update
    }

    void Move()
    {
        // Oblicz now� pozycj� docelow� bossa
        Vector2 targetPosition = (Vector2)transform.position + currentDirection * speed * Time.deltaTime;

        // Sprawd�, czy istniej� jakiekolwiek przeszkody w pobli�u bossa w kierunku ruchu
        RaycastHit2D hit = Physics2D.Raycast(transform.position, currentDirection, obstacleDetectionDistance, obstacleLayer);

        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("boss"))
            {
                // Odbicie od przeszkody z tagiem "boss"
                currentDirection = Vector2.Reflect(currentDirection, -hit.normal);
            }
            else if (hit.collider.CompareTag("Boss"))
            {
                // Odbicie od przeszkody z tagiem "minotaur"
                currentDirection = Vector2.Reflect(currentDirection, -hit.normal);
            }
            else
            {
                // Je�li napotkano inn� przeszkod�, zmie� kierunek na losowy
                currentDirection = Random.insideUnitCircle.normalized;
            }
        }
        else
        {
            // Poruszanie bossa w aktualnym kierunku
            transform.position = targetPosition;
        }
    }
}
