using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject PauseMenu, SettingsMenu;
    public FirstPersonCameraRotation fps;
    public bool GameEND = false;
    public Pause PAPA;
    public Scoring scoreBoi;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    public void Restart()
    {
        PAPA.ExitPauseMenu();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
    }

    public void StartPauseMenu() {
        scoreBoi.isPaused = true;
        if (GameEND) { return; }
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        PauseMenu.SetActive(true);
        Time.timeScale = 0f;
        fps.enabled = false;
    }

    public void ExitPauseMenu() {
        scoreBoi.isPaused = false;
        if (GameEND) { return; }
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
        fps.enabled = true;
    }
    public void EnterSettings() {
        PauseMenu.SetActive(false);
        SettingsMenu.SetActive(true);
}
    public void Mainmenu() {
        SceneManager.LoadScene("IntroScreen");
        Time.timeScale = 1f;
    }
}
