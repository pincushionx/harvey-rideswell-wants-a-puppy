using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pincushion.LD46
{
    public class StartMenuUIController : MonoBehaviour
    {
        public void StartGameClicked()
        {
            GameManager.StartGame();
        }
    }
}