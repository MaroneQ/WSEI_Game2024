using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor.SearchService;


public class player_move : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
    public float czas = 10f; // Pocz¹tkowa wartoœæ czasu
    public Text czasText; // Pole tekstowe do wyœwietlania czasu
    
   

    void Start()
    {
        // Pobierz komponent Rigidbody2D obiektu gracza
        rb = GetComponent<Rigidbody2D>();

        // Przypisz komponent Text z obiektu tekstowego do czasText
        czasText = GameObject.Find("CZAS").GetComponent<Text>();

        // Wywo³aj metodê zmniejszaj¹c¹ czas co sekundê, rozpoczynaj¹c od razu, z wywo³aniem co sekundê
        InvokeRepeating("ZmniejszCzas", 0f, 1f);

        InvokeRepeating("RotateClocks", 0f, 0.05f);
    }

    void FixedUpdate()
    {
        // Pobierz wartoœæ osi poziomej (prawo/lewo) i osi pionowej (góra/dó³)
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Okreœl wektor kierunku na podstawie osi
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        // Przesuñ obiekt gracza zgodnie z wektorem kierunku przy u¿yciu Rigidbody2D
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }

    void ZmniejszCzas()
    {
        // Zmniejsz czas o 1 co sekundê
        czas -= 1;

        // Aktualizuj tekst w polu tekstowym
        czasText.text = "CZAS: "  + czas.ToString();

        // Jeœli czas osi¹gn¹³ 0, zatrzymaj odliczanie
        if (czas <= 0)
        {
            CancelInvoke("ZmniejszCzas");
            
        }
    }
    void RotateClocks()
    {
        // Find all GameObjects with the tag "CLOCK"
        GameObject[] clocks = GameObject.FindGameObjectsWithTag("clock");

        // Rotate each clock object
        foreach (GameObject clock in clocks)
        {
            clock.transform.Rotate(Vector3.forward * 6f);
            clock.transform.Rotate(Vector3.up * 6f);
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("clock")) // Check if collision is with an object tagged as "CLOCK"
        {
            // Increase czas by 10
            czas += 10;

            // Destroy the clock object
            Destroy(collision.gameObject);

            // Update the text in czasText
            
        }


        
    
            else if (collision.gameObject.CompareTag("Boss")) // SprawdŸ, czy kolizja nast¹pi³a z obiektem o tagu "Boss"
            {
                // Zmniejsz czas o 1
                czas -= 10;

                // Aktualizuj tekst w polu tekstowym
                czasText.text = "CZAS: " + czas.ToString();



                // Jeœli czas osi¹gn¹³ 0, zatrzymaj odliczanie
                if (czas <= 0)
                {
                    CancelInvoke("ZmniejszCzas");
                SceneManager.LoadScene(0);

            }
            
        }
    }
}
