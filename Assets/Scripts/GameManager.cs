using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool isPaused;
    public GameObject pauseMenu;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
    public void PauseToggle()
    {
        if (isPaused)
        {
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
            isPaused = false;
        }
        //Unpause
        else 
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
            isPaused = true;
        }
        //pause
    }
    void Update()
    {
        if (ReInput.players.GetSystemPlayer().GetButtonDown("Cancel"))
        {
            PauseToggle();
        }
    }


}
