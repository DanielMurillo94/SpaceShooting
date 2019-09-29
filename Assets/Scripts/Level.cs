using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    [SerializeField] float delayInSeconds = 3f;

    public void LoadGameOver()
    {
        StartCoroutine(LoadGameOverCoroutine());
    }

    private IEnumerator LoadGameOverCoroutine()
    {
        yield return new WaitForSeconds(delayInSeconds);
        SceneManager.LoadScene("GameOver");
        var audioPlayer = FindObjectOfType<MusicPlayer>();
        if (audioPlayer)
        {
            audioPlayer.SetLowVolume();
        }
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene("CoreGame");
        var audioPlayer = FindObjectOfType<MusicPlayer>();
        if (audioPlayer)
        {
            audioPlayer.SetHighVolume();
        }
        FindObjectOfType<GameSession>().Reset();
    }

    public void LoadStartmenu()
    {
        SceneManager.LoadScene(0);
        var audioPlayer = FindObjectOfType<MusicPlayer>();
        if (audioPlayer)
        {
            audioPlayer.SetLowVolume();
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    
}
