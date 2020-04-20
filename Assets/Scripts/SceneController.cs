using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Pincushion.LD46
{
    public class SceneController : MonoBehaviour
    {
        public SceneOverlayUIController overlay;
        public SoundController sound;
        public MomController mom;

        private float timeElapsed = 0f;
        public float TimeElapsed
        {
            get
            {
                return timeElapsed;
            }
        }
        private float deltaTime = 0f;
        public float DeltaTime
        {
            get
            {
                return deltaTime;
            }
        }

        private bool endOfLevel_succeeded = false;
        private bool endOfLevel_failed = false;
        private bool momTalking = false;
        private bool paused = false;
        public bool Paused {
            get {
                return paused || momTalking;
            }
            set {
                paused = value;
            }
        }



        public PlayerController player;

        private void Awake()
        {
            EnableEscMenu(false);
        }
        private void Start()
        {
            StartLevel();
        }

        // Update is called once per frame
        void Update()
        {
            if (momTalking)
            {
                if (Input.GetKeyDown(mom.GetNextKey())|| Input.GetKeyDown(KeyCode.KeypadEnter))
                {
                    string nextBubble = mom.GetNextBubble(true);
                    if (nextBubble == "")
                    {
                        // start level
                        mom.ShowBubble("");
                        momTalking = false;
                    }
                    else
                    {
                        mom.ShowBubble(nextBubble);
                    }
                }

                // This is next level stuff
                if (!momTalking)
                {
                    if (endOfLevel_failed)
                    {
                        endOfLevel_failed = false;
                        GameManager.RestartScene();
                    }
                    if (endOfLevel_succeeded)
                    {
                        endOfLevel_succeeded = false;
                        GameManager.GoToNextLevel();
                    }
                }
            }
            else if (!Paused)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    Paused = true;
                    EnableEscMenu(true);
                }
                else
                {
                    deltaTime = Time.deltaTime;
                    timeElapsed += Time.deltaTime;
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    Paused = false;
                }
            }
        }

        public void EnableEscMenu(bool enable)
        {
            overlay.EnableEscMenu(enable);
        }

        public void StartLevel()
        {
            if (SceneManager.GetActiveScene().name == "Level1Scene") {
                momTalking = true;
                mom.ShowBubble("tutorial1Bubble");
            }
            else if (SceneManager.GetActiveScene().name == "Level2Scene")
            {
                momTalking = true;
                mom.ShowBubble("level2Objectives");
            }
            else if (SceneManager.GetActiveScene().name == "Level3Scene")
            {
                momTalking = true;
                mom.ShowBubble("level3Objectives");
            }
        }

        public void FellOffTheEarth()
        {
            momTalking = true;

            endOfLevel_failed = true;
            mom.ShowBubble("fellOffTheEarth");
        }

        public void PlantDied(int fert, int soil)
        {
            momTalking = true;

            endOfLevel_failed = true;
            mom.ShowBubble("deadPlant");
        }

        public void DestinationReached(int fert, int soil)
        {
            momTalking = true;

            if (SceneManager.GetActiveScene().name == "Level1Scene")
            {
                if (fert < 1)
                {
                    endOfLevel_failed = true;
                    mom.ShowBubble("level1NotEnoughFert");
                }
                else
                {
                    // success
                    endOfLevel_succeeded = true;
                    mom.ShowBubble("success");
                }
            }
            else if (SceneManager.GetActiveScene().name == "Level2Scene")
            {
                if (fert < 2)
                {
                    endOfLevel_failed = true;
                    mom.ShowBubble("level2NotEnoughFert");
                }
                else
                {
                    // success
                    endOfLevel_succeeded = true;
                    mom.ShowBubble("success");
                }
            }
            else if (SceneManager.GetActiveScene().name == "Level3Scene")
            {
                if (fert < 3)
                {
                    endOfLevel_failed = true;
                    mom.ShowBubble("level3NotEnoughFert");
                }
                else
                {
                    // success
                    endOfLevel_succeeded = true;
                    mom.ShowBubble("success");
                }
            }
        }
    }
}