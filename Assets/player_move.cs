using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class player_move : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
    public float czas = 10f;
    public Text czasText;
    private float score;
    public Text scoreText;
    public Text plusText;

    private bool plusTextActive = false; // Flaga okreœlaj¹ca, czy plusText jest aktywny
    private float plusTextDuration = 1f; // Czas trwania plusText w sekundach

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        czasText = GameObject.Find("CZAS").GetComponent<Text>();
        scoreText = GameObject.Find("SCORE").GetComponent<Text>();
        plusText = GameObject.Find("PLUS").GetComponent<Text>();

        plusText.gameObject.SetActive(false); // Wy³¹cz plusText na pocz¹tku gry

        InvokeRepeating("ZmniejszCzas", 0f, 1f);
        InvokeRepeating("RotateClocks", 0f, 0.05f);
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }

    void ZmniejszCzas()
    {
        czas -= 1;
        czasText.text = "CZAS: " + czas.ToString();

        if (czas <= 0)
        {
            CancelInvoke("ZmniejszCzas");
            SceneManager.LoadScene(0);
        }
    }

    void RotateClocks()
    {
        GameObject[] clocks = GameObject.FindGameObjectsWithTag("clock");
        foreach (GameObject clock in clocks)
        {
            clock.transform.Rotate(Vector3.forward * 4f);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("clock"))
        {
            czas += 10;
            Destroy(collision.gameObject);
            score += 1;
            scoreText.text = "SCORE: " + score.ToString() + "/26";
            plusText.text = "+1";
            plusText.gameObject.SetActive(true); // W³¹cz plusText

            // Uruchom funkcjê dezaktywuj¹c¹ plusText po okreœlonym czasie
            StartCoroutine(DeactivatePlusText());
        }

        // (Pozosta³y kod obs³ugi kolizji z elementem "Boss")
    }

    IEnumerator DeactivatePlusText()
    {
        if (!plusTextActive)
        {
            plusTextActive = true;
            yield return new WaitForSeconds(plusTextDuration);
            plusText.gameObject.SetActive(false);
            plusTextActive = false;
        }
    }
}
