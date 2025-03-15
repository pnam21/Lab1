using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ScoreKeeper : MonoBehaviour
{
    int score;
    [SerializeField] int requiredScoreStage = 1000;
    int currentStage;
    static ScoreKeeper instance;
    LevelManager levelManager;

    private void Awake()
    {
        ManageSingleton();
        SceneManager.sceneLoaded += OnSceneLoaded; // Subscribe to scene change event
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // Unsubscribe to avoid memory leaks
    }

    void Start()
    {
        StartCoroutine(CheckNextStage());
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        levelManager = FindFirstObjectByType<LevelManager>(); // Reassign LevelManager
        Debug.Log($"LevelManager reassigned in scene: {scene.name}");
    }

    IEnumerator CheckNextStage()
    {
        while (true)
        {
            LoadNextStage();
            yield return new WaitForSeconds(1f);
        }
    }

    void ManageSingleton()
    {
        if (instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void SetCurrentStage(int value)
    {
        currentStage = value;
    }

    public int GetScore()
    {
        return score;
    }

    public void ModifyScore(int value)
    {
        score += value;
        score = Mathf.Clamp(score, 0, int.MaxValue);
    }

    public void ResetScore()
    {
        score = 0;
    }

    void LoadNextStage()
    {
        if (levelManager == null) return; // Prevent null reference errors

        switch (currentStage)
        {
            case 1:
                if (score >= requiredScoreStage) levelManager.LoadWin();
                break;
        }
    }
}
