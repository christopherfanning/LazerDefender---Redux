using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using TMPro;

public class GameStatus : MonoBehaviour
{

    [SerializeField] int score = 0;
    // [SerializeField] TextMeshProUGUI scoreText;
    // [SerializeField] int points = 50;

    private void Awake() 
    {
        SetupSingleton();
    }

    void SetupSingleton()
    {
        if ( FindObjectsOfType(GetType()).Length > 1 )
        {
            //Debug.Log("I Totally destroyed the copy..");
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            //Debug.Log("I kept the session alive. ");
            DontDestroyOnLoad(gameObject);
        }
    }
    
    private void Start() {
       // scoreText.text = score.ToString();
    }
    
    public int GetScore(){
        return score;
    }

    public void AddToScore(int scoreValue){
        score += scoreValue;
        // scoreText.text = score.ToString();
    }

    public void ResetGame(){
        Destroy(gameObject);
    }
}
