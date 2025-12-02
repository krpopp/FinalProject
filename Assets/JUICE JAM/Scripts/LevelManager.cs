using UnityEngine;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{
    [SerializeField] GameObject[] levelElements;
    List<GameObject> objectsInLevel = new();

    [SerializeField] int objectBuffer = 5;
    [SerializeField] int jumpDist;

    float elementSpawnPos = 0;
    [SerializeField] Transform player;

    bool firstObstacle = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.position.x > elementSpawnPos - (objectBuffer * 10))
        {
            GameObject inst;

            if (firstObstacle)
            {
                inst = Instantiate(levelElements[0], new Vector2(elementSpawnPos, Random.Range(-1f, 1f)), Quaternion.identity);
                firstObstacle = false;
            }
            else
            {
                inst = Instantiate(levelElements[Random.Range(0, levelElements.Length)], new Vector2(elementSpawnPos, Random.Range(-1f, 1f)), Quaternion.identity);
            }

            elementSpawnPos += inst.GetComponent<Collider2D>().bounds.extents.x * 2 + jumpDist;
            objectsInLevel.Add(inst);

        }
    }
}
