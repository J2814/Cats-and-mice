using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelSwitch : MonoBehaviour
{
    public static LevelSwitch instance;

    private List<int> levels = new List<int> { 0, 1, 2, 3, 4 };
    private int currentLevelIndex = 0;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void SwitchLevel()
    {
        if( currentLevelIndex < levels.Count - 1)
        {
            currentLevelIndex++;
        }
        else
        {
            currentLevelIndex = 0;
        }

        SceneManager.LoadScene(levels[currentLevelIndex]);
    }

    public void SwitchLevel(int index)
    {
        if (index < levels.Count - 1)
        {
            SceneManager.LoadScene(levels[index]);
        }
    }
}
