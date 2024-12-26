using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelSwitch : MonoBehaviour
{
    //public static LevelSwitch instance;

    private int sceneCount;
    private List<int> levels;

    private int currentLevelIndex = 0;

    private void Start()
    {
        sceneCount = SceneManager.sceneCountInBuildSettings;
        levels = new List<int>();


        for (int i = 0; i < sceneCount; i++)
        {
            levels.Add(i); 
        }
    }

    //private void Awake()
    //{
    //    if (instance != null)
    //    {
    //        Destroy(gameObject);
    //    }
    //    else
    //    {
    //        instance = this;
    //        DontDestroyOnLoad(gameObject);
    //    }
    //}

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
        AudioManager.instance.PlaySound(AudioManager.instance.SoundBank.GenericUi);
    }

    public void SwitchLevel(int index)
    {
        if (index < levels.Count - 1)
        {
            SceneManager.LoadScene(levels[index]);
            AudioManager.instance.PlaySound(AudioManager.instance.SoundBank.GenericUi);
        }
    }

    public void SwitchLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
        AudioManager.instance.PlaySound(AudioManager.instance.SoundBank.GenericUi);
    }
}
