using UnityEngine;
using UnityEngine.SceneManagement;

namespace Script
{
    public class MainMenuManager : MonoBehaviour
    {
        public void PlayLevel()
        {
            SceneManager.LoadScene(1);
        }
    }
}
