using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuEvents : MonoBehaviour{
    
    public void LoadScene(int i){
        UnityEngine.SceneManagement.SceneManager.LoadScene(i);
    }

    public void QuitGame(){
        Application.Quit();
    }

}
