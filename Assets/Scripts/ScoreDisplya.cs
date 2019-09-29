using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreDisplya : MonoBehaviour
{

    TextMeshProUGUI text;
    GameSession gamesession;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        gamesession = FindObjectOfType<GameSession>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = gamesession.GetScore().ToString();
        Debug.Log(gamesession.GetScore().ToString());
    }
}
