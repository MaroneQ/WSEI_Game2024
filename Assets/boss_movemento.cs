using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss_movemento : MonoBehaviour
{
    public float speed = 3f; // Pr�dko�� poruszania si� bossa
    public List<Transform> targetPoints; // Lista punkt�w docelowych
    private int currentTargetIndex = 0; // Indeks aktualnego punktu docelowego

    void Update()
    {
        Move(); // Wywo�anie funkcji poruszania w funkcji Update
    }

    void Move()
    {
        // Sprawd�, czy lista punkt�w docelowych nie jest pusta
        if (targetPoints.Count == 0)
        {
            Debug.LogError("Lista punkt�w docelowych jest pusta!");
            return;
        }

        Vector2 targetPosition = targetPoints[currentTargetIndex].position;

        // Sprawd�, czy istniej� jakiekolwiek przeszkody na drodze bossa
        RaycastHit2D hit = Physics2D.Raycast(transform.position, targetPosition - (Vector2)transform.position, Vector2.Distance(transform.position, targetPosition));

        if (hit.collider != null && hit.collider.gameObject != gameObject) // Je�li wykryto przeszkod�
        {
            // Przeszkoda zosta�a wykryta, przejd� do nast�pnego punktu docelowego
            currentTargetIndex = (currentTargetIndex + 1) % targetPoints.Count;
            return;
        }

        // Poruszanie bossa w kierunku celu z okre�lon� pr�dko�ci�
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Je�li boss osi�gnie cel, przejd� do nast�pnego punktu docelowego
        if ((Vector2)transform.position == targetPosition)
        {
            currentTargetIndex = (currentTargetIndex + 1) % targetPoints.Count;
        }
    }

}
