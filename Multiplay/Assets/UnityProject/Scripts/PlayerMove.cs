    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Rewired;
    using Unity.VisualScripting;

    public class PlayerMove : MonoBehaviour
    {
        public GameObject fallEffect;
        public float speed;
        public float jumpForce;
        public float maxSpeed;
        public int playerId = 0;
        Rigidbody playerRigid;
        AudioSource playerStepSound;
        public Animator animator;
        public Transform cameraArm;
        public Transform target;
        public float smoothSpeed = 0.125f;
        public Vector3 offset;

        private Vector3 velocity = Vector3.zero;
        private float airbornTimer = 0.0f;
        private float airbornRate = 1f;
        private Player player;
        private Vector3 moveVector;
        private bool fire;
        private bool isWalking;
        private bool isGround;
        private bool fallDamage;

        // Start is called before the first frame update
        void Start()
        {
            fire = false;
            playerRigid = GetComponent<Rigidbody>();
            player = ReInput.players.GetPlayer(playerId);
            playerStepSound = GetComponent<AudioSource>();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            animator = GetComponent<Animator>();
        }




        private void GetInput()
        {
            //moveVector.x = player.GetAxis("Move Horizontal");
            //moveVector.z = player.GetAxis("Move Vertical");
            fire = player.GetButtonDown("Attack");

        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Floor"))
            {
                isGround = true;
                airbornTimer = 0.0f;
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            if (collision.gameObject.CompareTag("Floor"))
            {
                isGround = false;
            }
        }

        private void ProcessInput()
        {

            if (fire)
            {
                Debug.Log("발사");
                // Set vibration in all Joysticks assigned to the Player
                int motorIndex = 0; // the first motor
                float motorLevel = 0.05f; // full motor speed
                float duration = 0.2f; // 2 seconds
                player.SetVibration(motorIndex, motorLevel, duration);
            
            }


        }
        private void LateUpdate()
        {
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);
            cameraArm.position = smoothedPosition;


        }
        private void LookAround()
        {
            Vector2 mouseDelta = new Vector2(player.GetAxis("Camera Horizontal"), player.GetAxis("Camera Vertical"));
            mouseDelta *= 0.2f;
            Vector3 camAngle = cameraArm.rotation.eulerAngles;
            float x = camAngle.x - mouseDelta.y;
            if (x < 180f)
            {
                x = Mathf.Clamp(x, -1f, 40f);
            }
            else
            {
                x = Mathf.Clamp(x, 335f, 361f);
            }
            cameraArm.rotation = Quaternion.Euler(x, camAngle.y + mouseDelta.x, camAngle.z);
        }
        // Update is called once per frame
        void Update()
        {
        

            LookAround();
            float moveHorizontal = player.GetAxis("Move Horizontal");
            float moveVertical = player.GetAxis("Move Vertical");
       

            Vector3 cameraForward = cameraArm.forward; // 카메라가 보고 있는 방향 벡터를 가져옴
            Vector3 cameraRight = cameraArm.right; // 카메라의 오른쪽 방향 벡터를 가져옴
            cameraForward.y = 0f; // y 축 값을 0으로 설정하여 수평 이동만 가능하게 함
            cameraRight.y = 0f; // y 축 값을 0으로 설정하여 수평 이동만 가능하게 함
            cameraForward.Normalize(); // 벡터 정규화
            cameraRight.Normalize(); // 벡터 정규화
            Vector3 movement = (cameraForward * moveVertical + cameraRight * moveHorizontal) * speed;

        float movementSpeed = Mathf.Clamp01(Mathf.Sqrt(moveHorizontal * moveHorizontal + moveVertical * moveVertical));
        animator.SetFloat("MoveSpeed", movementSpeed);
          

            
            // Rigidbody의 속도를 직접 변경하여 이동시킴
            playerRigid.velocity = new Vector3(movement.x, playerRigid.velocity.y, movement.z);

            if (playerRigid.velocity.magnitude > maxSpeed)
            {
                playerRigid.velocity = playerRigid.velocity.normalized * maxSpeed;
            }

            isWalking = movement.magnitude != 0;
        
           

            transform.LookAt(transform.position + movement);
            

        
                if (player.GetButtonDown("Player Jump"))
            {
                if (isGround)
                {
                isGround=false;
                animator.Play("PlayerJump");
                animator.SetBool("Jump", true);
                    playerRigid.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                    if (transform.position.y > 0f)
                    {
                        Vector3 gravity = new Vector3(0f, -1f, 0f);
                        playerRigid.velocity = gravity;
                    }
                }
            }
                if(isGround)
        {
            animator.SetBool("Jump", false);

        }
        GetInput();
            ProcessInput();
        }
    }

