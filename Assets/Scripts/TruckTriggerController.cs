using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pincushion.LD46
{
    public class TruckTriggerController : MonoBehaviour
    {
        public SceneController scene;
        public GameObject truck;

        private bool moving = false;
        private float acceleration = 20f;
        private float speed = 0f;
        private float maxSpeed = 100f;

        // Update is called once per frame
        void Update()
        {
            if (moving) {
                speed += acceleration * scene.DeltaTime;

                if (speed > maxSpeed)
                {
                    speed = maxSpeed;
                }
                Vector3 pos = truck.transform.position;
                pos.x -= speed * scene.DeltaTime;
                truck.transform.position = pos;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            // no need to check anything. It's Harvey
            moving = true;
        }
    }
}