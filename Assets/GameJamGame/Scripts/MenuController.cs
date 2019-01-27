using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

    public string gameScene;
    public RawImage instructions;

	public void PlayGame()
    {
        instructions.enabled = true;
        StartCoroutine(InstructionsToGame());
    }

    IEnumerator InstructionsToGame()
    {
        yield return new WaitForSeconds(10f);
        SceneManager.LoadScene(gameScene);

    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space) && instructions.enabled == true)
        {
            SceneManager.LoadScene(gameScene);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
