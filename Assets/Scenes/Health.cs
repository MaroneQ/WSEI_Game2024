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
        // Tutaj mo¿esz dodaæ kod wykonywany po œmierci gracza, np. dezaktywacja obiektu gracza, wyœwietlenie ekranu z informacj¹ o przegranej itp.
        gameObject.SetActive(false);
    }
}
