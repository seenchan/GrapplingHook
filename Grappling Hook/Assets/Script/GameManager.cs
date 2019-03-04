using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager> {

    protected override void Init()
    {
    }

    public float gunShootInterval;
    public float bulletSpeed = 10f;
    public float sloMoSpeed = 0.2f;
    public int chosenLevel = 1;

    public bool winGame = false;
    public bool playerDead = false;
    public bool hookAnchored = false;
    public bool isBulletExist = false;
    public bool restartGame = false;

    // Use this for initialization
    void Start () {
        ResetGameParameters();
	}
	
	// Update is called once per frame
	void LateUpdate () {
        GameStateManager();
    }

    void GameStateManager()
    {
        if (winGame == true && SceneManager.GetActiveScene().name == "MainMenu")
        {
            SceneManager.LoadScene(chosenLevel);
            ResetGameParameters();
        }
        if (restartGame == true)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            ResetGameParameters();
        }
        if (winGame == true)
        {
            if (int.Parse(SceneManager.GetActiveScene().name) + 1 == SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene("MainMenu");
            }
            else
            {
                int levelNum = int.Parse(SceneManager.GetActiveScene().name) + 1;
                SceneManager.LoadScene(levelNum.ToString());
            }

            ResetGameParameters();
        }
    }

    void ResetGameParameters()
    {
        //game related parameters
        winGame = false;
        restartGame = false;

        //player related parameters
        playerDead = false;
        hookAnchored = false;
        isBulletExist = false;

    }
}
