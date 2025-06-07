using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    public int healthValue = 1;

    //Specific Unity function that is called on in the first frame when the physics system detects
    //a GameObject w/ a Rigidbody component hitting the GameObject collider that is a trigger
    //Param: other, will contain the reference to the collider component that just entered the trigger
    void OnTriggerEnter2D(Collider2D other)
    {
        // This instruction creates a variable that contains the reference to the Player Controller (Script) component
        // on GameObject that collides with the collectible.

        // The PlayerController script is only attached to the PlayerCharacter GameObject,
        // so the variable will only be filled with this reference if the player character collides with the collectible.

        // If a GameObject that doesn’t have this script attached collides with the collectible, this variable will
        // remain empty and return no value (null).
        PlayerController player = other.GetComponent<PlayerController>();

        if (player != null && player.health < player.maxHealth)
        {
            player.ChangeHealth(healthValue);
            Destroy(gameObject);
        }
    }
}