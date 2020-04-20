using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Pincushion.LD46
{

    public class MomController : MonoBehaviour
    {
        public GameObject bubble1;
        public GameObject bubble1Listening;
        public GameObject bubble2;
        public GameObject bubble3;

        public GameObject tutorial1Bubble;
        public GameObject tutorial2Bubble;
        public GameObject tutorial3Bubble;

        public GameObject level1Objectives;
        public GameObject level1NotEnoughFert;

        public GameObject level2Objectives;
        public GameObject level2NotEnoughFert;

        public GameObject level3Objectives;
        public GameObject level3NotEnoughFert;

        public GameObject gotADog;

        public GameObject deadPlant;
        public GameObject fellOffTheEarth;
        public GameObject success;


        public string currentBubbleName = "";

        public void ShowBubble(string name)
        {
            currentBubbleName = name;
            bubble1.SetActive(name == "bubble1");
            bubble1Listening.SetActive(name == "bubble1Listening");
            bubble2.SetActive(name == "bubble2");
            bubble3.SetActive(name == "bubble3");

            tutorial1Bubble.SetActive(name == "tutorial1Bubble");
            tutorial2Bubble.SetActive(name == "tutorial2Bubble");
            tutorial3Bubble.SetActive(name == "tutorial3Bubble");

            level1Objectives.SetActive(name == "level1Objectives");
            level1NotEnoughFert.SetActive(name == "level1NotEnoughFert");
            level2Objectives.SetActive(name == "level2Objectives");
            level2NotEnoughFert.SetActive(name == "level2NotEnoughFert");
            level3Objectives.SetActive(name == "level3Objectives");
            level3NotEnoughFert.SetActive(name == "level3NotEnoughFert");
            deadPlant.SetActive(name == "deadPlant");
            fellOffTheEarth.SetActive(name == "fellOffTheEarth");
            success.SetActive(name == "success");
            gotADog.SetActive(name == "gotADog");
        }
        public string GetNextBubble(bool listened)
        {
            // Room scene
            if (currentBubbleName == "")
            {
                //first
                return "bubble1";
            }
            if (currentBubbleName == "bubble1")
            {
                if (!listened)
                {
                    return "bubble1Listening";
                }
                return "bubble2";
            }
            else if (currentBubbleName == "bubble1Listening")
            {
                return "bubble2";
            }
            if (currentBubbleName == "bubble2" )
            {
                return "bubble3";
            }

            // Game Scene
            if (currentBubbleName == "tutorial1Bubble")
            {
                return "tutorial2Bubble";
            }
            else if (currentBubbleName == "tutorial2Bubble")
            {
                return "tutorial3Bubble";
            }
            else if (currentBubbleName == "tutorial3Bubble")
            {
                return "level1Objectives";
            }

            // These have no next bubble
            // level1Objectives
            // level1NotEnoughFert
            // deadPlant
            // success
            // fellOffTheEarth
            //gotADog

            return ""; // done
        }
        public KeyCode GetNextKey()
        {
            return KeyCode.Return;

            // If I play with this, it'll be the exception

            /* if (currentBubbleName == "bubble1")
             {
                 return KeyCode.RightArrow;
             }
             else if (currentBubbleName == "bubble1Listening")
             {
                 return KeyCode.RightArrow;
             }
             else if (currentBubbleName == "level1Scene")
             {
                 return KeyCode.RightArrow;
             }
             else if (currentBubbleName == "level1Objectives")
             {
                 return KeyCode.RightArrow;
             }
             else if (currentBubbleName == "level1Objectives")
             {
                 return KeyCode.RightArrow;
             }
             return KeyCode.Escape;*/
        }
    }

}