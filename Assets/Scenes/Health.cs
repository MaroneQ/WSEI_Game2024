using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Tutaj mo�esz doda� kod wykonywany po �mierci gracza, np. dezaktywacja obiektu gracza, wy�wietlenie ekranu z informacj� o przegranej itp.
        gameObject.SetActive(false);
    }
}
