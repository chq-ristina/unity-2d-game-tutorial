using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //Start is called before the first frame update
    void Start()
    {
        MoveAction.Enable();
        rigidbody2D = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        move = MoveAction.ReadValue<Vector2>();

        if (isInvincible)
        {
            damageCooldown -= Time.deltaTime;
            if (damageCooldown < 0)
            {
                isInvincible = false;
            }
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
        }
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log(currentHealth + "/" + maxHealth);
    }
}