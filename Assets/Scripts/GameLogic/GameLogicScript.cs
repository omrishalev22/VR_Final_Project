using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using System.Linq;
using System.Collections.Generic;
using Assets.Scripts.GameLogic;
using UnityEngine.Audio;

public class GameLogicScript : MonoBehaviour
{
    private Transform goal;
    private NavMeshAgent agent;
    public List<GameObject> enemies;
    public GameObject[] enemiesArr;

    // Use this for initialization
    void Start()
    {
        goal = Camera.main.transform;
        agent = GetComponent<NavMeshAgent>();

        enemiesArr = Resources.LoadAll<GameObject>("Food");
        StartCoroutine(GenerateSprites());
    }

    IEnumerator GenerateSprites()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            if (!GameManager.isGameOver)
            {
                GameObject enemyToInstitate = enemiesArr[Random.Range(0, enemiesArr.ToList().Count)];
                var newSpritePosition = new Vector3(Random.Range(0, 10f), 4f, Random.Range(-7f, 7f));

                var newSprite = Instantiate(enemyToInstitate, newSpritePosition, Quaternion.identity);
                var agent = newSprite.AddComponent<NavMeshAgent>();
                newSprite.AddComponent<CollisionScript>();
                newSprite.AddComponent<Rigidbody>();
                var audio = newSprite.AddComponent<AudioSource>();
                audio.clip = GetComponent<AudioSource>().clip;
                audio.playOnAwake = false;

                var spriteCollider = newSprite.AddComponent<BoxCollider>();
                spriteCollider.isTrigger = true;
                agent.destination = goal.position;
            }
            else
            {
                StopAllCoroutines();
            }
        }
    }
}
