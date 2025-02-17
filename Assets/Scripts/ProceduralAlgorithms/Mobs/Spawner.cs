using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<GameObject> mapObjects;

    public Spawner()
    {
        mapObjects = new List<GameObject>();
    }

    public IEnumerator SpawnObjects(List<Vector2Int> spawnLocations, GameObject gameObj)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        foreach (var location in spawnLocations)
        {
            Vector3 spawnPosition = new Vector3(location.x, location.y, 0);

            // Check the distance between the player and the spawn position
            if (Vector3.Distance(player.transform.position, spawnPosition) > 6f)
            {
                GameObject newObj = Instantiate(gameObj, spawnPosition, Quaternion.identity);
                mapObjects.Add(newObj);
            }
            yield return null;
        }
    }

    public void ClearObjects()
    {
        foreach (var obj in mapObjects)
        {
            bool canDestroy = true;
            if (obj.CompareTag("Duck"))
            {
                FollowParent followParent = obj.GetComponent<FollowParent>();
                if (followParent.isFollowing)
                {
                    canDestroy = false;
                }

            }

            if (canDestroy)
            {
                DestroyImmediate(obj);
            }

        }
        mapObjects.Clear();
        Destroy(this);
    }
}