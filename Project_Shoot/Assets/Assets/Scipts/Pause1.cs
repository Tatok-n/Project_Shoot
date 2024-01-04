using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause1 : MonoBehaviour
{
    public GameObject PauseMenu,MainMenu;
    // Start is called before the first frame update
  

    

    public void ExitSettings() {
        PauseMenu.SetActive(false);
        MainMenu.SetActive(true);
    }


}
