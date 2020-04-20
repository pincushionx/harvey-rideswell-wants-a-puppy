using UnityEngine;
using UnityEngine.UI;

namespace Pincushion.LD46
{
    public class EscMenuController : MonoBehaviour
    {
        public SceneController scene;
        public Slider volumeSlider;

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