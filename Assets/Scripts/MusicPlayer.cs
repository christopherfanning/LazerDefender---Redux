using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    // MusicPlayer musicPlayer;
    // Start is called before the first frame update
    private void Awake() 
    {
        
        if (FindObjectsOfType(GetType()).Length > 1 )
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        } 
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
