using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{

    public float speed;
    public bool vertical;

    public float changeTime = 3.0f;
    private float timer;
    private int direction = 1;
    
    Rigidbody2D rigidbody2D;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        timer = changeTime;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        
        
        if (timer < 0)
        {
            var dirChange = Random.Range(1, 10);
            vertical = dirChange % 2 == 0;
            
            direction *= -1;
            timer = changeTime;
        }
    }

    //physics calculations need to be here rather than Update function
    //FixedUpdate has the same call rate as the physics system
    private void FixedUpdate()
    {
        //stores Enemy GameObject's rigidbody2D component's position in a Vector2 variable called position
        Vector2 position = rigidbody2D.position;

        if (vertical)
        {
            position.y += speed * direction * Time.deltaTime;
        }
        else
        {
            //increments the component's x-axis position by the speed multiplied by Time.deltatime (time it takes each frame to render)
            position.x += speed * direction * Time.deltaTime;
        }
        
        //uses MovePosition to apply the updated position info 
        rigidbody2D.MovePosition(position);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.GetComponent<PlayerController>();

        if (player != null)
        {
            player.ChangeHealth(-1);
        }
        
    }
}
