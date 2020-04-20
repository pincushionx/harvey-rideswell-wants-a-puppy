using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Pincushion.LD46
{
    public class SceneOverlayUIController : MonoBehaviour
    {
        public SceneController scene;
        public Text soilLevel;
        public Text fertLevel;

        public GameObject escMenu;
        public Slider volumeSlider;

        public void Update()
        {
            if (!scene.Paused)
            {
                soilLevel.text = scene.player.soil.ToString();
                fertLevel.text = scene.player.fertilizer.ToString();
            }
        }

        public void EnableEscMenu(bool enable)
        {
            escMenu.SetActive(enable);
        }


        private void Awake()
        {
            volumeSlider.value = AudioListener.volume;
        }

        public void ResumeClicked()
        {
            scene.EnableEscMenu(false);
        }
        public void StartGameClicked()
        {
            GameManager.StartGame();
        }
        public void RestartLevelClicked()
        {
            GameManager.RestartScene();
        }
        public void ExitClicked()
        {
            Application.Quit();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }

        public void VolumeChanged(float value)
        {
            AudioListener.volume = volumeSlider.value;
        }
    }
}