using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour {

    public string menuScene;
    public GameObject pausePanel;

    public void ResumeGame()
    {
        TogglePause();
    }

    public void QuitGame()
    {
        SceneManager.LoadScene(menuScene);
    }

    private void TogglePause()
    {
        EventBus.Emit<PauseEvent>(new PauseEvent()); //stop movements
        pausePanel.SetActive(!pausePanel.activeSelf);
    }

    // Update is called once per frame
    void Update () {

		if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SceneManager.LoadScene("Menu");
        }
	}

}
