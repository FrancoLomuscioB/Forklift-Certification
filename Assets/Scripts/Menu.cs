using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
   
       public void ExitButton()
        {
            Application.Quit();
            Debug.Log("Game closed");
        }

        public void StartGame()
        {
            SceneManager.LoadScene("Levels");
        Debug.Log("cai en la locura");
        }
 
   
}
