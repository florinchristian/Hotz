using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
   private bool settingsMenu=false;
   public GameObject settingsMenuInstance;
   public void PlayGame()
   {
      AudioMAnager.instance.buttonClick.Play();
      SceneManager.LoadScene("SandboxScene");
   }

   public void QuitGame()
   {
      AudioMAnager.instance.buttonClick.Play();
      Application.Quit();
   }

   public void SettingsMenu()
   {
      if (settingsMenu)
      {
         settingsMenuInstance.SetActive(false);
         settingsMenu = false;
      }
      else
      {
         settingsMenuInstance.SetActive(true);
         settingsMenu = true;
      }
   }
}
