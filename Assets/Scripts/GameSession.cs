using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{

    private int score = 0;

    private void Awake()
    {
        SetSingleton();
    }

    void SetSingleton()
    {
        if (FindObjectsOfType<GameSession>().Length > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void AddScore(int score)
    {
        this.score += score;
    }

    public int GetScore()
    {
        return score;
    }

    public void Reset()
    {
        Destroy(gameObject);
    }

    private void Update()
    {
        Debug.Log(score.ToString());
    }
}
