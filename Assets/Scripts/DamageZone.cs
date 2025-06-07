using UnityEngine;

public class DamageZone : MonoBehaviour
{
    public int healthValue = 1;

    //OnTriggerStay2D is called every frame that the GameObject with the Rigidbody component is inside the collider,
    //not just when it enters. This change means that the damage zone won’t have a one-time impact — the function will
    //continue to be called as long as the player character is in the damage zone.
    void OnTriggerStay2D(Collider2D other)
    {
        // This instruction creates a variable that contains the reference to the Player Controller (Script) component
        // on GameObject that collides with the collectible.

        // The PlayerController script is only attached to the PlayerCharacter GameObject,
        // so the variable will only be filled with this reference if the player character collides with the collectible.

        // If a GameObject that doesn’t have this script attached collides with the collectible, this variable will
        // remain empty and return no value (null).
        PlayerController player = other.GetComponent<PlayerController>();

        if (player != null)
        {
            player.ChangeHealth(healthValue * -1);
        }
    }
}