using System;
using System.Collections.Generic;
using Game;
using UnityEngine;

[Serializable]
public class ObjectToPool
{
    public string name;
    public GameObject gameObject;
    public int amount;
    public Transform parent;
}

public class PooledObject
{
    public string name;
    public GameObject gameObject;
    public Transform transform;
    public Target target;
}

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance;
    public List<ObjectToPool> objectToPool;
    public Queue<PooledObject> pooledObjectsQ;
    public Dictionary<string, Queue<PooledObject>> poolDictionary;
    public bool isSet;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        poolDictionary = new Dictionary<string, Queue<PooledObject>>();
        foreach (var item in objectToPool)
        {
            pooledObjectsQ = new Queue<PooledObject>();
            for (var i = 0; i < item.amount; i++)
            {
                var obj = Instantiate(item.gameObject, item.parent);
                obj.transform.rotation = Quaternion.LookRotation(transform.parent.up);

                obj.SetActive(false);

                pooledObjectsQ.Enqueue(new PooledObject()
                {
                    name = item.name,
                    gameObject = obj,
                    transform = obj.transform,
                    target = obj.GetComponent<Target>()
                });
            }

            poolDictionary.Add(item.name, pooledObjectsQ);
        }
        isSet = true;
    }


    public PooledObject GetPooledObject(string objectName)
    {
        if (!poolDictionary.ContainsKey(objectName))
        {
            return null;
        }

        var obj = poolDictionary[objectName].Dequeue();
        poolDictionary[objectName].Enqueue(obj);
        obj.gameObject.transform.rotation = Quaternion.identity;
        obj.transform.rotation = Quaternion.identity;

        return obj;
    }

    public List<PooledObject> GetAllPooledObjects(string objectName)
    {
        if (!poolDictionary.ContainsKey(objectName))
        {
            return null;
        }

        var l = new List<PooledObject>();
        foreach (var item in poolDictionary[objectName])
        {
            l.Add(item);
        }

        return l;
    }
}