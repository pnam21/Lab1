using UnityEngine;
using UnityEngine.UI;

public class UIDisplay : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] Slider healthSlider;
    [SerializeField] Health playerHealth;

    [Header("Score")]
    ScorePanel scorePanel;
    ScoreKeeper scoreKeeper;

    private void Awake()
    {
        scorePanel = FindFirstObjectByType<ScorePanel>();
        scoreKeeper = FindFirstObjectByType<ScoreKeeper>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        healthSlider.maxValue = playerHealth.GetHealth();
    }

    // Update is called once per frame
    void Update()
    {
        healthSlider.value = playerHealth.GetHealth();
        scorePanel.UpdateScoreDisplay(scoreKeeper.GetScore().ToString("000000"));
    }
}
