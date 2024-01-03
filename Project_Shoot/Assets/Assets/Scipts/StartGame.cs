using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public GameObject StartMenu, LevelSelect, Title;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnterLevelSelect() {
        StartMenu.SetActive(false);
        Title.SetActive(false);
        LevelSelect.SetActive(true);
    }

    public void StartDefaultLvl() {
        SceneManager.LoadScene("Default_Level");
    }
}
