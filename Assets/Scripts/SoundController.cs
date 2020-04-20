using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pincushion.LD46
{
    public class SoundController : MonoBehaviour
    {
        public AudioSource potCracking;
        public AudioSource yeah;
        public AudioSource bang;

        public void playSound(string soundName)
        {
            if (soundName == "potCracking")
            {
                potCracking.Play();
            }
            else if (soundName == "yeah")
            {
                yeah.Play();
            }
            else if (soundName == "bang")
            {
                bang.Play();
            }
        }
    }
}