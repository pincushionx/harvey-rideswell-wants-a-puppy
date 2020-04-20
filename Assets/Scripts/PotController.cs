using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pincushion.LD46
{
    public class PotController : MonoBehaviour
    {
        public SceneController scene;

        


        /*
        private void Awake()
        {
            GetComponent<Rigidbody>().isKinematic = true;

            lastPosition = transform.position;
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.name == "Road")
            {
                transform.parent = null;
            }
            
        }

        public Vector3 lastPosition;
        public Vector3 diff;
        public float frameVelocityTolerance;
        public float diffMagnetude;

        private void FixedUpdate()
        {
            diff = transform.position - lastPosition;

            float velocityTolerance = 50f;

            frameVelocityTolerance = (velocityTolerance * scene.DeltaTime);
            diffMagnetude = diff.magnitude;

            if (diffMagnetude > frameVelocityTolerance)
            {
                Fall();
            }

            lastPosition = transform.position;
        }

        private Vector3 lastRot = new Vector3();
        private void Update()
        {
            Transform bike = transform.parent;
            Vector3 rot = bike.rotation.eulerAngles;

            if (rot.x > 180)
            {
                rot.x = 360 - rot.x;
            }
            if (rot.z > 180)
            {
                rot.z = 360 - rot.z;
            }

            if (rot.x > 20 || rot.z > 20)
            {
                Fall();
            }
            else
            {
                transform.eulerAngles = lastRot;
            }

            lastRot = bike.rotation.eulerAngles;
        }

        public void Fall()
        {
            GetComponent<Rigidbody>().isKinematic = false;
            GetComponent<Rigidbody>().AddExplosionForce(1f, transform.position - Vector3.down, 1f);
        }*/
    }
}