using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour
{
    [SerializeField] float delay = 3f;
    ScoreKeeper scoreKeeper;

    void Awake()
    {
        scoreKeeper = FindFirstObjectByType<ScoreKeeper>();
    }

    public void LoadStage1()
    {
        scoreKeeper.ResetScore();
        scoreKeeper.SetCurrentStage(1);
        SceneManager.LoadScene("Stage1");
    }

    public void LoadStage2()
    {
        scoreKeeper.SetCurrentStage(2);
        StartCoroutine(WaitAndLoad("Stage2", delay));
    }

    public void LoadStage3()
    {
        scoreKeeper.SetCurrentStage(3);
        StartCoroutine(WaitAndLoad("Stage3", delay));
    }

    public void LoadMainMenu()
    {
        scoreKeeper.SetCurrentStage(0);
        scoreKeeper.ResetScore();
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadGameOver()
    {
        scoreKeeper.SetCurrentStage(4);
        StartCoroutine(WaitAndLoad("GameOver", delay));
    }

    public void LoadWin()
    {
        scoreKeeper.SetCurrentStage(5);
        SceneManager.LoadScene("Win");
    }

    public void QuitGame()
    {
        Application.Quit(); //Work for standalone builds
    }

    IEnumerator WaitAndLoad(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }
}
