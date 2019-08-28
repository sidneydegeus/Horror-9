using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public partial class Player : MonoBehaviour {

    public float WalkSpeed = 2;
    public float RunSpeed = 6;
    public float Gravity = -12;

    public GameObject spotlight;
    public Inventory inventory;
    public ItemDatabase database;

    Animator animator;
    CharacterController controller;
    MoveDirection moveDirection;

    float velocityY;
    public bool inventoryOpen = false;
    bool phoneEquiped = false;

    Vector3 position;

    internal bool HealthChanged;
    public RawImage bloodyScreen;
    public RawImage deathScreen;
    Color currentScreenColor;

    internal PlayerStats Stats;

    // Use this for initialization
    void Start() {
        Time.timeScale = 1;
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        moveDirection = new MoveDirection();
        Stats = new PlayerStats(this);
        currentScreenColor = bloodyScreen.color;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update() {
        if (HealthChanged)
            LerpBloodyScreen();
        if (Stats.Health <= 0)
            Die();
        if (!inventoryOpen)
            PlayerInput();
    }

    void Die() {
        deathScreen.gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
    }


    float damageTaken;
    public void TakeDamage(float damage) {
        damageTaken = damage;
        Invoke("DelayDamageEffects", 1.2f);
    }

    void DelayDamageEffects() {
        Stats.Health -= damageTaken;
        HealthChanged = true;
    }

    void LerpBloodyScreen()
    {
        bloodyScreen.gameObject.SetActive(true);
        float alpha = Mathf.Lerp(0.0f, 1.0f, 1f - (Stats.Health / 100));
        currentScreenColor.a = alpha;
        bloodyScreen.color = currentScreenColor;
        HealthChanged = false;
    } 

    void PlayerMovement() {
        //Cursor.lockState = CursorLockMode.Locked;

        moveDirection.Reset();
        moveDirection.direction = transform.forward;

        float translation = Input.GetAxisRaw("Vertical");
        float strafe = Input.GetAxisRaw("Horizontal");
        Vector2 inputDir = new Vector2(translation, strafe).normalized;

        GetDirectionFromInput(translation, strafe);

        bool running = false;
        if (moveDirection.forward || moveDirection.forwardLeft || moveDirection.forwardRight) {
            running = Input.GetKey(KeyCode.LeftShift);
        }
        float speed = ((running) ? RunSpeed : WalkSpeed) * inputDir.magnitude;

        velocityY += Time.deltaTime * Gravity;
        Vector3 velocity = moveDirection.direction *
            ((moveDirection.backwardRight || moveDirection.backwardLeft || moveDirection.forwardRight || moveDirection.forwardLeft) ? speed / 1.415f : speed)
            + Vector3.up * velocityY;

        controller.Move(velocity * Time.deltaTime);
        speed = new Vector2(controller.velocity.x, controller.velocity.z).magnitude;
        if (controller.isGrounded) {
            velocityY = 0;
        }
    }

    void PlayerInput() {

        PlayerMovement();

        if (Input.GetKeyDown(KeyCode.Q)) {
            Item itemToCheck = database.FetchItemById(3);

            if (inventory.checkInInventory(itemToCheck)) {
                phoneEquiped = !phoneEquiped;

                if (spotlight.activeSelf)
                    spotlight.SetActive(false);
                else
                    spotlight.SetActive(true);

                animator.SetBool("PhoneEquiped", phoneEquiped);
            }
        }

        if (Input.GetKeyDown("escape"))
            Cursor.lockState = CursorLockMode.None;

        if (Input.GetKeyDown(KeyCode.Z)) {
            Stats.Health -= 20;
            HealthChanged = true;
        }
        if (Input.GetKeyDown(KeyCode.X)) {
            Stats.Health += 40;
            HealthChanged = true;
        }
    }




    void GetDirectionFromInput(float translation, float strafe) {
        if (translation == -1 && strafe == 0) {
            // move backward
            moveDirection.direction = -transform.forward;
            moveDirection.backward = true;
        }
        else if (translation == -1 && strafe == -1) {
            // move backward left
            moveDirection.direction = -transform.forward + -transform.right;
            moveDirection.backwardLeft = true;
        }
        else if (translation == -1 && strafe == 1) {
            //move backward right
            moveDirection.direction = -transform.forward + transform.right;
            moveDirection.backwardRight = true;
        }
        else if (translation == 1 && strafe == 0) {
            // move forward
            moveDirection.direction = transform.forward;
            moveDirection.forward = true;
        }
        else if (translation == 1 && strafe == -1) {
            // move forward left
            moveDirection.direction = transform.forward + -transform.right;
            moveDirection.forwardLeft = true;
        }
        else if (translation == 1 && strafe == 1) {
            //move forward right
            moveDirection.direction = transform.forward + transform.right;
            moveDirection.forwardRight = true;
        }
        else if (strafe == -1 && translation == 0) {
            //strafe left
            moveDirection.direction = -transform.right;
            moveDirection.left = true;
        }
        else if (strafe == 1 && translation == 0) {
            // strafe right
            moveDirection.direction = transform.right;
            moveDirection.right = true;
        }
    }

    struct MoveDirection {
        public bool forward, forwardLeft, forwardRight;
        public bool backward, backwardLeft, backwardRight;
        public bool left, right;
        public Vector3 direction;

        public void Reset() {
            forward = forwardLeft = forwardRight = false;
            backward = backwardLeft = backwardRight = false;
            left = right = false;
        }
    }
}
