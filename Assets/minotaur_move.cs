using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minotaur_move : MonoBehaviour
{
    public float speed = 3f; // Prêdkoœæ poruszania siê bossa
    public float turnRadius = 1f; // Promieñ skrêtu bossa
    public float obstacleDetectionDistance = 1f; // Odleg³oœæ detekcji przeszkód
    public LayerMask obstacleLayer; // Warstwa przeszkód
    public LayerMask minotaurLayer; // Warstwa zawieraj¹ca minotaurów

    private Vector2 currentDirection; // Aktualny kierunek bossa
    private CapsuleCollider2D turnCollider; // Collider reprezentuj¹cy promieñ skrêtu


    void Start()
    {
        currentDirection = Vector2.right; // Pocz¹tkowy kierunek bossa (prawo)

        // Dodaj i skonfiguruj CapsuleCollider2D jako collider reprezentuj¹cy promieñ skrêtu
        turnCollider = gameObject.AddComponent<CapsuleCollider2D>();
        turnCollider.size = new Vector2(turnRadius * 2f, 0.1f);
        turnCollider.direction = CapsuleDirection2D.Horizontal;
        turnCollider.isTrigger = true;
    }

    void Update()
    {
        Move(); // Wywo³anie funkcji poruszania w funkcji Update
    }

    void Move()
    {
        // Oblicz now¹ pozycjê docelow¹ bossa
        Vector2 targetPosition = (Vector2)transform.position + currentDirection * speed * Time.deltaTime;

        // SprawdŸ, czy istniej¹ jakiekolwiek przeszkody w pobli¿u bossa w kierunku ruchu
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
                // Jeœli napotkano inn¹ przeszkodê, zmieñ kierunek na losowy
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
