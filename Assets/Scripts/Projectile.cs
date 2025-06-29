using UnityEngine;
using UnityEngine.InputSystem;

public class Projectile : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    public void Launch(Vector2 direction, float force)
    {
        //The function calls AddForce on the projectile’s Rigidbody component; the calculation for this here is
        //the direction variable multiplied by the force variable.
        // When the force is applied to the Rigidbody component, the physics engine will apply that force and direction
        // to move the Projectile GameObject every frame.
        rigidbody2d.AddForce(direction * force);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Projectile collision with " + other.gameObject);
        Destroy(gameObject);
    }
    
}
