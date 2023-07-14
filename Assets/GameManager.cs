using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public int score = 0;                       // The player's current score
    public TextMeshProUGUI scoreText;           // Reference to the TextMeshPro text UI that displays the score
    public static GameManager instance;
    public TextMeshProUGUI timer;
    public float timeleft;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else

            Destroy(this);
    }
    private void Update()
    {
        timeleft-= Time.deltaTime;
        timer.text= "Time Left :" +timeleft.ToString();
        if (timeleft < 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    public void AddScore(int value)
    {
      
            // Increase the score and update the UI text
            score+= value;
            scoreText.text =  score.ToString();
        
    }
}