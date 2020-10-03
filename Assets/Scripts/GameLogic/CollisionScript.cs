using UnityEngine;
using UnityEngine.AI;

public class CollisionScript : MonoBehaviour
{
    //for this to work both need colliders, one must have rigid body, and the zombie must have is trigger checked.
    // col = the object which was bumped into the gameobject
    void OnTriggerEnter(Collider col)
    {
        Debug.Log($"col.gameObject.name{col.gameObject.name}");
        Debug.Log($"gameObject.name {gameObject.name}");

        var playerWasHit = gameObject.name.Contains("Player");
        var spriteWasShot = col.gameObject.name.Contains("ammo");

        if (playerWasHit || spriteWasShot)
        {
            if (gameObject.name.Contains("Enemy"))
            {
                // should --1 score
            }
            else
            {
                // should ++1 score
            }


            // destory the sprite from a shot or from bumping into the player
            if(playerWasHit)
            {
                Destroy(col.gameObject); // destory the sprite which bumped into the player
            } 
            else if(spriteWasShot)
            {
                Destroy(gameObject); // destory the actual sprite
            }
        }
    }
}
