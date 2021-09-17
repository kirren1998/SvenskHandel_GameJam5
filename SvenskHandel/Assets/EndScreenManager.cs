using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreenManager : MonoBehaviour
{
   public void BackToMenu()
   {
      SceneManager.LoadScene(0);
   }

   public void Replay()
   {
      SceneManager.LoadScene(1);
   }
}
