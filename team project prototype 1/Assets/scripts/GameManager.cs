using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public PlayerController player;

    public Text healthText;
    public Text ammoText;

    public GameObject resumeButton;
    public GameObject mainMenuButton;
    public GameObject quitButton;
    public GameObject restartButton;
    public GameObject pauseIndicator;
    public int enemyCounter;

    public bool paused = false;
    public float levelDelayTimer = 1f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // THis is supposed to do something
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            

            
            
        }
    }

}
