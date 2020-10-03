using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using System.Linq;
using System.Collections.Generic;

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
        Debug.Log($"count:{enemiesArr.ToList().Count}");
        StartCoroutine(GenerateSprites());
    }

    IEnumerator GenerateSprites()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            GameObject enemyToInstitate = enemiesArr[UnityEngine.Random.Range(0, enemiesArr.ToList().Count)];

            var newSpritePosition = new Vector3(UnityEngine.Random.Range(0,17f), UnityEngine.Random.Range(1.5f,2.5f), UnityEngine.Random.Range(-7f, 7f));
            var newSprite = Instantiate(enemyToInstitate, newSpritePosition, Quaternion.identity);
            var agent = newSprite.AddComponent<NavMeshAgent>();
            newSprite.AddComponent<CollisionScript>();
            newSprite.AddComponent<Rigidbody>();
            var spriteCollider = newSprite.AddComponent<BoxCollider>();
            spriteCollider.isTrigger = true;

            agent.destination = goal.position;
        }
    }
}
