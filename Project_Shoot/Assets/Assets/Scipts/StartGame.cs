using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public GameObject LevelSelect, previousMenu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnterLevelSelect() {
        previousMenu.SetActive(false);
        LevelSelect.SetActive(true);
    }

    public void StartDefaultLvl() {
        SceneManager.LoadScene("Default_Level");
    }
}
