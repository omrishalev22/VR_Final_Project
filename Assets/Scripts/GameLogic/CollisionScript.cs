using Assets.Scripts.GameLogic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CollisionScript : MonoBehaviour
{
    private int score;
    private int health;

    [SerializeField]
    public GameObject healthBar;

    [SerializeField]
    public GameObject scoreText;

    private void Start()
    {
        score = 0;
        health = 10;
    }

    public void SetHealth(bool increase)
    {
        if (increase)
        {
            health += 1;
        } else
        {
            health -= 1;
        }
        healthBar.GetComponent<Text>().text = new StringBuilder(2 * health).Insert(0, "+ ", health).ToString();
    }

    public void SetScoreText(bool increase)
    {
        if (increase)
        {
            score += 1;
        } else
        {
            score = Mathf.Max(0, score - 1);
        }
        scoreText.GetComponent<Text>().text = score.ToString();
    }

    //for this to work both need colliders, one must have rigid body, and the enemies must have is trigger checked.
    // col = the object which was bumped into the gameobject
    void OnTriggerEnter(Collider col)
    {
        var playerWasHit = gameObject.name.Contains("Player");
        var spriteWasShot = col.gameObject.name.Contains("ammo");

        if (playerWasHit || spriteWasShot)
        {
            // destory the sprite from a shot or from bumping into the player
            if(playerWasHit)
            {
                var eatingSound = col.gameObject.GetComponent<AudioSource>(); // play eating sound
                eatingSound.Play(0);

                // In case the player got hit - if the object which hitted the player is an enemy, reduce health
                // Otherwise - improve score
                if (col.gameObject.name.Contains("Enemy"))
                {
                    SetHealth(false);
                }
                else
                {
                    SetScoreText(true);
                }

                Destroy(col.gameObject); // destory the sprite which bumped into the player
            } 
            else if(spriteWasShot)
            {
                Destroy(gameObject); // destory the  sprite
                Destroy(col.gameObject); // destory the ammo

                //// In case the player shot an object, if the object is an enemy - improve score
                //// Otherwise, reduce score (score can not be under zero)
                //if (gameObject.name.Contains("Enemy"))
                //{
                //    SetScoreText(true);

                //}
                //else
                //{
                //    SetScoreText(false);
                    
                //}
            }

            // In case the player health is 0 - Stop the game and move to scores board
            if (health == 0)
            {
                Debug.Log("Game finished");
                GameManager.isGameOver = true;
                ScoresController.instance.AddHighscoreEntry(score);
                SceneManager.LoadScene(2);
            }
        }
    }
}
