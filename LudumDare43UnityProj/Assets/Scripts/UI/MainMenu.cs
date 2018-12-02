using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    private void Start()
    {
        
    }

    public void OnClickPlay()
    {
        SceneManager.LoadScene("Game");
    }
    
    public void OnClickInstructions()
    {

    }

    public void OnClickCredits()
    {

    }
}
