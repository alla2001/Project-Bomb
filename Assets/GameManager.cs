using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int score = 0;                       // The player's current score
    public TextMeshProUGUI scoreText;           // Reference to the TextMeshPro text UI that displays the score
    public static GameManager instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else

            Destroy(this);
    }

    public void AddScore(int value)
    {
      
            // Increase the score and update the UI text
            score+= value;
            scoreText.text =  score.ToString();
        
    }
}