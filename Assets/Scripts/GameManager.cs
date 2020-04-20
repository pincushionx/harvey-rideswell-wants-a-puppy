using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pincushion.LD46
{
    public static class GameManager
    {
        public static string GetCurrentScene()
        {
            return UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        }

        public static void RestartScene()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(GetCurrentScene());
        }

        public static void GoToStartScreen()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("StartScreen");
        }

        public static void StartGame()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("RoomScene");
        }

        public static void GoToNextLevel()
        {
            string currentScene = GetCurrentScene();
            if (currentScene == "StartScreen")
            {
                currentScene = "RoomScene";
            }
            else if (currentScene == "RoomScene")
            {
                currentScene = "Level1Scene";
            }
            else if (currentScene == "Level1Scene")
            {
                currentScene = "Level2Scene";
            }
            else if (currentScene == "Level2Scene")
            {
                currentScene = "Level3Scene";
            }
            else if (currentScene == "Level3Scene")
            {
                currentScene = "SuccessScene";
            }
            else if (currentScene == "SuccessScene")
            {
                currentScene = "StartScreen";
            }
            else
            {
                // TODO make an end screen
                currentScene = "StartScreen";
            }

            UnityEngine.SceneManagement.SceneManager.LoadScene(currentScene);
        }
    }
}