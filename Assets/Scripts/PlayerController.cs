using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Animator animator;

    //The moveDirection variable is important because the player character can stand still, whereas the enemy is always
    //moving. When the character is still, both Move X and Move Y will be 0, so the State Machine needs to be explicitly
    //provided with a direction, which this variable provides.
    Vector2 moveDirection = new Vector2(1, 0);

    //Variables related to player character movement
    public InputAction MoveAction;
    private Rigidbody2D rigidbody2D;
    private Vector2 move;
    public float speed = 3.0f;

    //Variables related to the health system
    public int maxHealth = 5;

    public int health
    {
        get { return currentHealth; }
    }

    public int currentHealth;

    // Variables related to temporary invincibility
    public float timeInvincible = 2.0f;
    private bool isInvincible;
    private float damageCooldown;

    public GameObject projectilePrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //Start is called before the first frame update
    void Start()
    {
        MoveAction.Enable();
        rigidbody2D = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        move = MoveAction.ReadValue<Vector2>();

        //The condition uses Mathf.Approximately to make the check instead of the equality operator (==) because the
        //way that computers store float values means that there is a tiny loss in precision.
        //This loss means that you should not test for perfect quality, because an operation that should return 0.0f
        //could end up returning 0.0000000001f instead. The bool Approximately takes the imprecision into account,
        //and returns true if the value can be considered equal minus that imprecision.
        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            moveDirection.Set(move.x, move.y);
            moveDirection.Normalize();
        }

        //These instructions pass the direction data to the PlayerCharacter GameObject’s Animator component.
        // The third instruction passes the length of the move vector to the Speed parameter. This length will be 0 if
        // the player character is stationary, or 1 if the character is moving (because the length is normalized).
        animator.SetFloat("Look X", moveDirection.x);
        animator.SetFloat("Look Y", moveDirection.y);
        animator.SetFloat("Speed", move.magnitude);

        if (isInvincible)
        {
            damageCooldown -= Time.deltaTime;
            if (damageCooldown < 0)
            {
                isInvincible = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            Launch();
        }    
    }

    //FixedUpdate has the same call rate as the physics system
    void FixedUpdate()
    {
        Vector2 position = (Vector2)rigidbody2D.position + move * speed * Time.deltaTime;
        rigidbody2D.MovePosition(position);
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (isInvincible)
            {
                return;
            }

            isInvincible = true;
            damageCooldown = timeInvincible;
            animator.SetTrigger("Hit");
        }

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        UIHandler.instance.SetHealthBar(currentHealth / (float)maxHealth);
    }

    public void Launch()
    {
        //This instruction calls Instantiate, a Unity function that takes three parameters.
        //
        // Instatiate’s first parameter is a GameObject reference. Calling Instantiate creates a copy of that GameObject
        // in a position defined by the second parameter, with the rotation defined by the third parameter.
        // In this situation, the Projectile prefab is the GameObject. You’ve defined a position at the position of the
        // PlayerCharacter GameObject’s Rigidbody component but a little up, so the Projectile prefab is placed closer
        // to the character sprite’s hands than feet. You’ve also set a rotation of Quaternion.identity.
        //
        // Quaternions are mathematical operators that can express rotation. All you need to know here is that
        // Quaternion.identity means no rotation.
        GameObject projectileObject =
            Instantiate(projectilePrefab, rigidbody2D.position + Vector2.up * 0.5f, Quaternion.identity);
        
        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(moveDirection, 300);
        animator.SetTrigger("Launch");
    }
}