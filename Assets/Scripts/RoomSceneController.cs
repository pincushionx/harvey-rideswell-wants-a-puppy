using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Pincushion.LD46
{
    public class RoomSceneController : MonoBehaviour
    {
        public MomController mom;

        // Start is called before the first frame update
        void Start()
        {
            if (SceneManager.GetActiveScene().name == "RoomScene")
            {
                string nextBubble = mom.GetNextBubble(true);
                mom.ShowBubble(nextBubble);
            }
            else if (SceneManager.GetActiveScene().name == "SuccessScene")
            {
                mom.ShowBubble("gotADog");
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                GameManager.GoToNextLevel();
            }

            if (Input.GetKeyDown(mom.GetNextKey()))
            {
                string nextBubble = mom.GetNextBubble(true);
                if (nextBubble == "")
                {
                    GameManager.GoToNextLevel();
                }
                else
                {
                    mom.ShowBubble(nextBubble);
                }
            }
            else if(mom.currentBubbleName == "bubble1" && Input.anyKeyDown) // do the "are you listening"
            {
                mom.ShowBubble("bubble2");
            }
        }
    }
}