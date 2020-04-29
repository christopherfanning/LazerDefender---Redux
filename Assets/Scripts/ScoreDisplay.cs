using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{

    Text scoreText;
    GameStatus gameStatus;

    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponent<Text>();
        gameStatus = FindObjectOfType<GameStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = gameStatus.GetScore().ToString();
    }
}
