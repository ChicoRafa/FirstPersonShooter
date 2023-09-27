using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Game manager of the game, where the time trial logic is implemented
/// </summary>
public class GameManager : MonoBehaviour
{
    [Header("Targets")]
    public int totalTargets;
    public int remainingTargets;
    [Header("Timer")]
    public float gameTimer = 0f;
    public Text timerText;

    // Start is called before the first frame update
    void Start()
    {
        totalTargets = GameObject.FindGameObjectsWithTag("Target").Length;
        remainingTargets = totalTargets;
        timerText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        StartTimeTrial();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ResetScene();
        }

    }

    /// <summary>
    /// This method starts the time trial minigame
    /// </summary>
    private void StartTimeTrial()
    {
        if (remainingTargets < totalTargets && remainingTargets != 0)
        {
            timerText.text = gameTimer.ToString("F2");
            gameTimer += Time.deltaTime;
        }
    }

    /// <summary>
    /// This method resets the current scene
    /// </summary>
    private void ResetScene()
    {
        SceneManager.LoadScene(0);
    }
}
