using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool isPaused;
    public GameObject pauseMenu;
    public GameObject Player1;
    public GameObject PlayerModel;
    private float P1ReviveTimer = 0;
    public GameObject Player2;
    private float P2ReviveTimer = 0;
    public float ReviveTime = 3;
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
        ///暂停
        if (ReInput.players.GetSystemPlayer().GetButtonDown("Cancel"))
        {
            PauseToggle();
        }

    }
    private void FixedUpdate()
    {
        if (!Player1)
        {
            P1ReviveTimer += Time.fixedDeltaTime;
            if (P1ReviveTimer >= ReviveTime)
            {
                var player1 = Instantiate(PlayerModel, new Vector3(115, 1.5f, 335), Quaternion.identity, null);
                Player1 = player1;
                player1.GetComponent<PlayerMovement>().playerID = 0;
                P1ReviveTimer -= ReviveTime;
            }
        }
        if (!Player2)
        {
            P2ReviveTimer += Time.fixedDeltaTime;
            if (P2ReviveTimer >= ReviveTime)
            {
                var player2 = Instantiate(PlayerModel, new Vector3(125, 1.5f, 335), Quaternion.identity, null);
                Player2 = player2;
                player2.GetComponent<PlayerMovement>().playerID = 1;
                P2ReviveTimer -= ReviveTime;
            }
        }
    }


}
