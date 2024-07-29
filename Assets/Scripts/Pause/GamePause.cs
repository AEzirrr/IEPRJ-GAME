using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePause : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject pauseContainer;
    [SerializeField] private GameObject optionsPanel;
    [SerializeField] private MonoBehaviour playerController; // Reference to the player control script
    [SerializeField] private MonoBehaviour playerAbility;
    private bool isPaused = false;
    private bool isOptionOpen;

    void Start()
    {
        pausePanel.SetActive(false);
        optionsPanel.SetActive(false);
        isOptionOpen = false;
    }

    void Update()
    {
        if(isOptionOpen == false) {

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                TogglePause();
            }
        }       


    }

    private void TogglePause()
    {
        isPaused = !isPaused;
        pausePanel.SetActive(isPaused);
        playerController.enabled = !isPaused; 
        playerAbility.enabled = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f; 
        }
        else
        {
            Time.timeScale = 1f; 
        }
    }

    public void OnResumeClicked()
    {
        TogglePause();
    }

    public void OnOptionsClicked()
    {
        optionsPanel.SetActive(true);
        pauseContainer.SetActive(false);
        isOptionOpen = true;
    }

    public void OnMainMenuClicked()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu", LoadSceneMode.Single);
    }

    public void OnQuitClicked()
    {
        Time.timeScale = 1f;
        Application.Quit();
    }

    public void OnCancelClicked()
    {
        optionsPanel.SetActive(false);
        pauseContainer.SetActive(true);
        isOptionOpen = false;
    }
}
