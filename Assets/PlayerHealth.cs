using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 3;                            // The amount of health the player starts the game with.
    public int currentHealth;                                   // The current health the player has.
    public GameObject healthGui;

    bool isDead;                                                // Whether the player is dead.

    void Awake ()
    {
        currentHealth = startingHealth;
        healthGui.GetComponent<Text>().text = "Health: " + currentHealth + " / 3";
    }

    public void ApplyDamage (int amount)
    {
        Debug.Log("applying damage");
        // Reduce the current health by the damage amount.
        currentHealth -= amount;
        healthGui.GetComponent<Text>().text = "Health: " + currentHealth + " / 3";

        // If the player has lost all it's health and the death flag hasn't been set yet...
        if (currentHealth <= 0 && !isDead)
        {
            // ... it should die.
            Death ();
        }
    }


    void Death ()
    {
        // Set the death flag so this function won't be called again.
        isDead = true;
        SceneManager.LoadScene("GameOver");
    }
}
