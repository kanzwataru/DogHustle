using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour {

    public Motor dog;
    public Motor dad;
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
        pausePanel.SetActive(!pausePanel.activeSelf);
        dog.canMove = !dog.canMove;
        dad.canMove = !dad.canMove;

    }

    // Update is called once per frame
    void Update () {

		if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
	}
}
