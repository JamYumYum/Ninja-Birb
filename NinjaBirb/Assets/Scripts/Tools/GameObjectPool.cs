using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectPool<T> where T : Component
{
    public static GameObjectPool<T> instance;

    private Queue<GameObject> pool;
    private string name;

    public GameObjectPool(string name = "New GameObject")
    {
        if(instance == null)
        {
            instance = this;
        }

        pool = new Queue<GameObject>();
        this.name = name;
    }


    public void Add(GameObject item)
    {
        pool.Enqueue(item);

        item.SetActive(false);


    }

    public GameObject Get()
    {
        if(pool.Count == 0)
        {
            GameObject newItem = new GameObject(name);
            pool.Enqueue(newItem);
            newItem.SetActive(false);
            newItem.AddComponent<T>();
        }

        return pool.Dequeue();
    }
    
}

