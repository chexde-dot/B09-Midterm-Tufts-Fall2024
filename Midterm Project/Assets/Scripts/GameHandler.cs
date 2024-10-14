using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{

    private GameObject player;
    public static int playerHealth = 100;
    public int StartPlayerHealth = 100;
    public GameObject healthText;

    public static int victimInBoat = 0;
    public GameObject victimInBoatText;
    public static int liveSaved = 0;
    public GameObject liveSavedText;

    public bool isDefending = false;

    public static bool stairCaseUnlocked = false;
    //this is a flag check. Add to other scripts: GameHandler.stairCaseUnlocked = true;

    private string sceneName;
    public static string lastLevelDied;  //allows replaying the Level where you died

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        sceneName = SceneManager.GetActiveScene().name;
        //if (sceneName=="MainMenu"){ //uncomment these two lines when the MainMenu exists
        playerHealth = StartPlayerHealth;
        //}
        updateStatsDisplay();
    }

    public void playerGetVictims(int victimCount)
    {
        victimInBoat += victimCount;
        updateStatsDisplay();
    }

    public void playerGetScore(int score)
    {
        victimInBoat -= score;
        liveSaved += score;
        updateStatsDisplay();
    }

    public void playerGetHit(int damage)
    {
        if (isDefending == false)
        {
            playerHealth -= damage;
            if (playerHealth >= 0)
            {
                updateStatsDisplay();
            }
            if (damage > 0)
            {
                //player.GetComponent<PlayerHurt>().playerHit();       //play GetHit animation
            }
        }

        if (playerHealth > StartPlayerHealth)
        {
            playerHealth = StartPlayerHealth;
            updateStatsDisplay();
        }

        if (playerHealth <= 0)
        {
            playerHealth = 0;
            updateStatsDisplay();
            playerDies();
        }
    }

    //ROBERTO ADD//
    public void IncreaseLiveSaved(int amount)
    {
        // Ensure the amount is positive
        if (amount > 0)
        {
            liveSaved += amount; // Increase live saved
            updateStatsDisplay(); // Update the display
        }
    }

    public void DecreaseVictimInBoat(int amount)
    {
        // Ensure the amount is positive
        if (amount > 0)
        {
            victimInBoat -= amount;
            updateStatsDisplay(); // Update the display
        }
    }
    //END OF ROBERTO ADD//

    public void updateStatsDisplay()
    {
        Text healthTextTemp = healthText.GetComponent<Text>();
        healthTextTemp.text = "HEALTH: " + playerHealth;

        Text VIBTextTemp = victimInBoatText.GetComponent<Text>();
        VIBTextTemp.text = "Victim In Boat: " + victimInBoat;

        Text SavedTextTemp = liveSavedText.GetComponent<Text>();
        SavedTextTemp.text = "SAVED: " + liveSaved;
    }

    public void playerDies()
    {
        //player.GetComponent<PlayerHurt>().playerDead();       //play Death animation
        lastLevelDied = sceneName;       //allows replaying the Level where you died
        StartCoroutine(DeathPause());
    }

    IEnumerator DeathPause()
    {
        //player.GetComponent<PlayerMove>().isAlive = false;
        //player.GetComponent<PlayerJump>().isAlive = false;
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("EndLose");
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Level1");
    }

    // Return to MainMenu
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
        // Reset all static variables here, for new games:
        playerHealth = StartPlayerHealth;
    }

    // Replay the Level where you died
    public void ReplayLastLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(lastLevelDied);
        // Reset all static variables here, for new games:
        playerHealth = StartPlayerHealth;
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }
}