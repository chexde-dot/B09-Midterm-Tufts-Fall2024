using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class TutorialManager : MonoBehaviour
{
     public TextMeshProUGUI tutorialText; 
    public float displayDuration = 5f; // Duration to display the text - 5s for now
    private float timer;

    //this is to change the text's position on the screen
    private RectTransform textRectTransform;

    private void Start()
    {
        // Get the RectTransform component
        textRectTransform = tutorialText.GetComponent<RectTransform>();

        // Start the tutorial sequence
        ShowWelcomeText();
    }

    private void Update()
    {
        // Update the timer for displaying the text
        if (tutorialText.gameObject.activeSelf)
        {
            timer += Time.deltaTime;
            if (timer >= displayDuration)
            {
                tutorialText.gameObject.SetActive(false);
            }
        }
    }

    public void ShowWelcomeText()
    {
        tutorialText.text = "Welcome to Extreme Ocean Rescue!";
        tutorialText.gameObject.SetActive(true);
        timer = 0f;
       
        // Call next text after a delay
        Invoke("ShowWelcomeText2", displayDuration);
    }
    public void ShowWelcomeText2()
    {
        tutorialText.gameObject.SetActive(true);

        tutorialText.text = "The goal of the game is to rescue as many drowning swimmers as possible before time runs out.";
        timer = 0f;
       
        // Call next text after a delay
        Invoke("WASDinstruct", displayDuration);
    }

    public void WASDinstruct()
    {
        tutorialText.gameObject.SetActive(true);

        tutorialText.text = "Use WASD to move your boat.";
        timer = 0f; // Reset the timer

        Invoke("ShowMenuTool1", displayDuration);
    }

    public void ShowMenuTool1()
    {
        tutorialText.gameObject.SetActive(true);

        // Change position of the text to point at health counter
        textRectTransform.anchoredPosition = new Vector2(-400, 200); 

        tutorialText.text = " ↑ Watch out for rocks! These can damage your health.";
        timer = 0f; // Reset the timer

        Invoke("ShowMenuTool2", displayDuration);
    }

    public void ShowMenuTool2()
    {
        tutorialText.gameObject.SetActive(true);

        tutorialText.text = " ↑ This counter shows how many swimmers you have in your boat. You can carry a maximum of XX swimmers.";
        timer = 0f; // Reset the timer

        // Change position of the text to point at victim counter
        textRectTransform.anchoredPosition = new Vector2(50, 200); 
        Invoke("ShowMenuTool3", displayDuration);
    }

    public void ShowMenuTool3()
    {
        tutorialText.gameObject.SetActive(true);

        tutorialText.text = " This counter ↑\nshows how many swimmers you've taken to safety.";
        timer = 0f; // Reset the timer

        // Change position of the text to point at saved victim counter
        textRectTransform.anchoredPosition = new Vector2(300, 200); 
        Invoke("ShowMenuTool4", displayDuration);
    }

    public void ShowMenuTool4()
    {
        tutorialText.gameObject.SetActive(true);

        tutorialText.text = " Take swimmers to safety by driving to dock/throwing them with xx control";
        timer = 0f; // Reset the timer

        // Change position of the text center
        textRectTransform.anchoredPosition = new Vector2(0, 0); 
        Invoke("ShowMenuTool5", displayDuration);
    }

    public void ShowMenuTool5()
    {
        tutorialText.gameObject.SetActive(true);

        tutorialText.text = " You can review the ↑ \ncontrols or \npause the game here";
        timer = 0f; // Reset the timer

        // Change position of the text center
        textRectTransform.anchoredPosition = new Vector2(380, 200); 
    }


}
