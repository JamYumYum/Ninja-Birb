using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreMeasure : MonoBehaviour, IMeasureScore
{
    private float Score = 0f;
    public float score { get => Score; set => Score = value; }

    private float distanceTravelled = 0f;

    private EndlessTerrain world;
    

    // Start is called before the first frame update
    void Start()
    {
        world = GetComponent<EndlessTerrain>();
        world.On_world_shift += On_world_shift_action;
    }

    // Update is called once per frame
    void Update()
    {
        
        Score = distanceTravelled - world.getGrid.transform.position.x;
        
    }

    void On_world_shift_action(object sender, EndlessTerrain.On_world_shift_args args)
    {
        distanceTravelled += args.distanceX;
    }
}
