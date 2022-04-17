using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour, ICollector<Collector>
{
    public PlayerHealth Health;
    public CharacterController controller;
    public virtual void Collect(ICollectable<Collector> collectable)
    {
        collectable.GettingCollectedAction(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        Health = GetComponent<PlayerHealth>();
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
