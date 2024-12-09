using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public static GameSceneManager instance;

    [Header("Scene Names")]
    [SerializeField] string nextLevelSceneName; // Name of the next level scene
    [SerializeField] string loseSceneName;      // Name of the lose scene

    int pigCount;

    void Awake()
    {
        // Singleton setup
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Count how many pigs are currently in the scene
        GameObject[] pigs = GameObject.FindGameObjectsWithTag("Pig");
        pigCount = pigs.Length;
    }

    public void PigDied()
    {
        pigCount--;

        if (pigCount <= 0)
        {
            // All pigs are dead, go to next level
            LoadNextLevel();
        }
    }

    public void GoToLoseScene()
    {
        // If out of birds and there are still pigs alive -> lose
        if (pigCount > 0)
        {
            SceneManager.LoadScene(loseSceneName);
        }
    }

    private void LoadNextLevel()
    {
        SceneManager.LoadScene(nextLevelSceneName);
    }

}
