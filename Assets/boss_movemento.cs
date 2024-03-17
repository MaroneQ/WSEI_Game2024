using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss_movemento : MonoBehaviour
{
    public float speed = 3f; // Prêdkoœæ poruszania siê bossa
    public List<Transform> targetPoints; // Lista punktów docelowych
    private int currentTargetIndex = 0; // Indeks aktualnego punktu docelowego

    void Update()
    {
        Move(); // Wywo³anie funkcji poruszania w funkcji Update
    }

    void Move()
    {
        // SprawdŸ, czy lista punktów docelowych nie jest pusta
        if (targetPoints.Count == 0)
        {
            Debug.LogError("Lista punktów docelowych jest pusta!");
            return;
        }

        Vector2 targetPosition = targetPoints[currentTargetIndex].position;

        // SprawdŸ, czy istniej¹ jakiekolwiek przeszkody na drodze bossa
        RaycastHit2D hit = Physics2D.Raycast(transform.position, targetPosition - (Vector2)transform.position, Vector2.Distance(transform.position, targetPosition));

        if (hit.collider != null && hit.collider.gameObject != gameObject) // Jeœli wykryto przeszkodê
        {
            // Przeszkoda zosta³a wykryta, przejdŸ do nastêpnego punktu docelowego
            currentTargetIndex = (currentTargetIndex + 1) % targetPoints.Count;
            return;
        }

        // Poruszanie bossa w kierunku celu z okreœlon¹ prêdkoœci¹
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Jeœli boss osi¹gnie cel, przejdŸ do nastêpnego punktu docelowego
        if ((Vector2)transform.position == targetPosition)
        {
            currentTargetIndex = (currentTargetIndex + 1) % targetPoints.Count;
        }
    }

}
