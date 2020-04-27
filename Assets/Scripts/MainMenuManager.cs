using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    
    void Start()
    {
        
    }

   public void PlayGame(){
        SceneManager.LoadScene(1,LoadSceneMode.Single);
   }
  
}
