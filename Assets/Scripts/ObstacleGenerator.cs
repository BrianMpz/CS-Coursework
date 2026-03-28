// procedurally generates obstacles while the game is running
// fulfills criterion 6
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

class ObstacleGenerator : Singleton<ObstacleGenerator>
{
    [SerializeField] private GameObject StartingPlatform; // the object that will be spawned
    [SerializeField] private GameObject Prefab; // the object that will be spawned
    public Queue<GameObject> ObstaclePrefabs; // keeps track of spawned obstacles
    public float scrollRate;

    private void Start()
    {
        ObstaclePrefabs = new(); // initialise the queue
    }

    private GameObject SpawnObject()
    {
        GameObject _obstacle = Instantiate(Prefab, transform); // create the obstacle
        _obstacle.transform.localPosition = Vector2.zero; // reset position
        return _obstacle;
    }

    private void FixedUpdate()
    {
        if (!GameManager.Instance.IsPlaying || PauseUI.Instance.IsPaused) return;

        if (ObstaclePrefabs.Count == 0) // if the queue is empty then add one
        {
            ObstaclePrefabs.Enqueue(SpawnObject());
        }

        foreach (GameObject obstacle in ObstaclePrefabs) // iterate through the obstacles and shift it down
        {
            obstacle.transform.localPosition += scrollRate * Time.fixedDeltaTime * Vector3.left;
        }
        StartingPlatform.transform.localPosition += scrollRate * Time.fixedDeltaTime * Vector3.left;

        if (ObstaclePrefabs.Peek().transform.localPosition.x < -4200) // if the obstacle is definitely out of frame then destroy it
        {
            GameObject _dequeuedObstacle = ObstaclePrefabs.Dequeue();
            Destroy(_dequeuedObstacle);
        }

        else if (ObstaclePrefabs.Last().transform.localPosition.x < -3900) // if the obstable is almost out of frame then spawn a new one
        {
            ObstaclePrefabs.Enqueue(SpawnObject());
        }
    }

}