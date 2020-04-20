using UnityEngine;

namespace Pincushion.LD46
{
    public class PlayerController : MonoBehaviour
    {
        public SceneController scene;
        public Camera cam;

        public int soil = 100; // 100 soil units in total
        public int fertilizer = 0;



        public bool grounded = false;
        public readonly float maxVelocity = 100f;
        public readonly float acceleration = 20f;
        public readonly float deceleration = -40f;
        public readonly float turnSpeed = 10f;
        private Vector3 targetVelocity = new Vector3();




        private PlayerState playerState = new PlayerState();

        class PlayerState
        {
            public readonly float fullVelocity = 0.1f;
            public readonly float acceleration = 0.01f;
            public readonly float turnSpeed = 1f;

            public float velocity = 0f;

            public readonly float leanFactor = 10f;
            public readonly float leanSpeed = 1f;
            public float lean = 0f;

            public readonly float fishtailFactor = 10f;
            public readonly float fishtailSpeed = 1f;
            public float fishtail = 0f;

            public Vector2 potAxis = new Vector2();


            public float yaw;
            public float pitch;
            public float roll;
            public float x;
            public float y;
            public float z;

            public void SetFromTransform(Transform t)
            {
                pitch = t.eulerAngles.x;
                yaw = t.eulerAngles.y;
                roll = t.eulerAngles.z;
                x = t.position.x;
                y = t.position.y;
                z = t.position.z;
            }
        }

        private Rigidbody rigid;

        private void Awake()
        {
            rigid = GetComponent<Rigidbody>();
        }
        private void OnEnable()
        {
            playerState.SetFromTransform(transform);

        }


        private void FixedUpdate()
        {
            if (!scene.Paused)
            {
                if (grounded)
                {
                    //rigid.freezeRotation = true;
                    //rigid.MovePosition(new Vector3(playerState.x, playerState.y, playerState.z));
                    //rigid.MoveRotation(Quaternion.Euler(new Vector3(playerState.pitch, playerState.yaw, playerState.roll)));
                    float additionalLean = 0f;
                    float additionalFishtail = 0f;

                    if (Input.GetKey(KeyCode.UpArrow))
                    {
                        targetVelocity.z += turnSpeed * scene.DeltaTime;
                        additionalLean += playerState.leanSpeed * scene.DeltaTime;
                    }
                    if (Input.GetKey(KeyCode.DownArrow))
                    {
                        targetVelocity.z -= turnSpeed * scene.DeltaTime;
                        additionalLean -= playerState.leanSpeed * scene.DeltaTime;
                    }
                    if (Input.GetKey(KeyCode.LeftArrow))
                    {
                        additionalFishtail = playerState.fishtailSpeed * scene.DeltaTime;
                        targetVelocity.x += deceleration * scene.DeltaTime;
                        if (targetVelocity.x < 0)
                        {
                            targetVelocity.x = 0;
                        }
                    }
                    if (Input.GetKey(KeyCode.RightArrow))
                    {
                        targetVelocity.x += acceleration * scene.DeltaTime;
                        if (targetVelocity.x > maxVelocity)
                        {
                            targetVelocity.x = maxVelocity;
                        }

                        GetComponentInChildren<Animator>().SetBool("pedalling", true);
                        GetComponentInChildren<Animator>().Play("Biking");
                    }
                    else
                    {
                        GetComponentInChildren<Animator>().SetBool("pedalling", false);
                    }

                    // limit turn speed to forward velocity
                    // change this to some ratio
                    if (targetVelocity.z > targetVelocity.x)
                    {
                        targetVelocity.z = targetVelocity.x;
                    }

                    // Fishtail
                    if (additionalFishtail == 0)
                    {
                        playerState.fishtail = 0;
                    }
                    playerState.fishtail += additionalFishtail;
                    playerState.yaw = playerState.fishtail * playerState.fishtailFactor;

                    // Lean
                    if (additionalLean == 0)
                    {
                        playerState.lean = 0; // lerp instead
                        targetVelocity.z = 0;
                    }
                    playerState.lean += additionalLean;
                    playerState.pitch = playerState.lean * playerState.leanFactor;



                    Vector3 rotatedTranslation = Quaternion.Euler(playerState.pitch * 10f, playerState.yaw * 10f, playerState.roll) * targetVelocity;


                    //rigid.MoveRotation(Quaternion.Euler(new Vector3(playerState.pitch * 10f, playerState.yaw * 10f, playerState.roll)));
                    //transform.eulerAngles = new Vector3(playerState.pitch * 10f, playerState.yaw * 10f, playerState.roll);

                    //rigid.AddTorque(new Vector3(playerState.pitch * 10f, playerState.yaw * 10f, playerState.roll), ForceMode.Force);
                    //rigid.AddTorque(

                    Vector3 velocity = rigid.velocity;
                    Vector3 velocityChange = (rotatedTranslation - velocity);
                    //velocityChange.x = Mathf.Clamp(velocityChange.x, -1f, 1f);
                    //velocityChange.z = Mathf.Clamp(velocityChange.z, -1f, 1f);
                    //velocityChange.y = 0;
                    rigid.AddForce(velocityChange, ForceMode.VelocityChange);

                    rigid.MoveRotation(Quaternion.Euler(new Vector3(playerState.pitch * 10f, playerState.yaw * 10f, playerState.roll)));

                    // Move camera
                    Vector3 campos = cam.transform.position;
                    campos.x = transform.position.x;
                    cam.transform.position = campos;
                }
                else
                {
                    // not grounded

                    if (transform.position.y < -10f)
                    {
                        // trigger the failure sequence
                        scene.FellOffTheEarth();
                    }

                   // rigid.freezeRotation = false;


                    rigid.AddForce(new Vector3(0, -10f, 0));
                }

                grounded = false;
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.tag == "ImmovableObstacle")
            {
                //playerState.velocity = 0;
                // scene.Paused = true; // temp
                scene.sound.playSound("bang");
                soil -= 100;
            }
            if (collision.collider.tag == "Ramp")
            {
                //playerState.velocity = 0;
                //scene.Paused = true; // temp
               // rigid.constraints = RigidbodyConstraints.None;
                
                Vector3 ramp = collision.collider.transform.rotation.eulerAngles;
                playerState.roll = ramp.z;

                
                //rigid.constraints = RigidbodyConstraints.None;

                if (ramp.z != playerState.roll)
                {
                    Vector3 velocity = targetVelocity;
                    Vector3 rotatedTranslation = Quaternion.Euler(playerState.pitch * 10f, playerState.yaw * 10f, playerState.roll) * rigid.velocity;

                    velocity.x = rotatedTranslation.x;
                    velocity.y = rotatedTranslation.y;
                    velocity.z = rotatedTranslation.z;

                    rigid.velocity = velocity;
                }


            }
            if (collision.collider.tag == "MovableObstacle")
            {
                soil -= 30;
                scene.sound.playSound("bang");
            }

            if (soil < 0) {
                scene.PlantDied(fertilizer, soil);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Destination")
            {
                Debug.Log("destination reached");
                scene.DestinationReached(fertilizer, soil);
            }
            else if (other.tag == "Fertilizer")
            {
                Debug.Log("+1 Fertilizer");
                Destroy(other.gameObject);

                fertilizer++;
                scene.sound.playSound("yeah");
            }
        }

        private void OnCollisionStay(Collision collision)
        {
            Vector3 ramp = collision.collider.transform.rotation.eulerAngles;

            if (ramp.z != playerState.roll)
            {
                playerState.roll = ramp.z;
            }

            grounded = true;
        }

  
    }

}
  