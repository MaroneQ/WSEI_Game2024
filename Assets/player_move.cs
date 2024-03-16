using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_move : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;

    void Start()
    {
        // Pobierz komponent Rigidbody2D obiektu gracza
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Pobierz warto�� osi poziomej (prawo/lewo) i osi pionowej (g�ra/d�)
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Okre�l wektor kierunku na podstawie osi
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        // Przesu� obiekt gracza zgodnie z wektorem kierunku przy u�yciu Rigidbody2D
        rb.velocity = movement * speed;
    }
}
