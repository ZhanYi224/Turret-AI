using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler current;
    [Header("UNITY Configuration field")]
    public List<GameObject> pooledObjects;
    public GameObject pooledObject;
    public int amountToPool;


    private void Awake()
    {
        current = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        pooledObjects = new List<GameObject>();
        for (int i = 0; i < amountToPool; i++)
        {
           GameObject obj = (GameObject)Instantiate(pooledObject);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
            if (amountToPool < 20)
            {
                amountToPool += 1;
                GameObject obj = (GameObject)Instantiate(pooledObject);
                pooledObjects.Add(obj);
                return obj;
            }
        }
        return null;
    }

    //if not enough bullet will instantiate bullet
}
