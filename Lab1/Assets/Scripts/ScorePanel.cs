using UnityEngine;
using UnityEngine.UI;

public class ScorePanel : MonoBehaviour
{
    public Sprite[] numberSprites; // Assign your 0-9 sprites in Inspector
    public Image[] digitImages; // Assign your UI Image objects in Inspector

    ScoreKeeper scoreKeeper;

    private void Awake()
    {
        scoreKeeper = FindFirstObjectByType<ScoreKeeper>();
    }

    private void Start()
    {
        UpdateScoreDisplay(scoreKeeper.GetScore().ToString("000000"));
    }

    public void UpdateScoreDisplay(string score)
    {
        string scoreString = score;
        for (int i = 0; i < digitImages.Length; i++)
        {
            if (i < scoreString.Length)
            {
                int digit = scoreString[scoreString.Length - 1 - i] - '0'; // Convert char to int
                digitImages[i].sprite = numberSprites[digit];
                digitImages[i].enabled = true; // Show the image
            }
            else
            {
                digitImages[i].enabled = false; // Hide unused digits
            }
        }
    }
}
