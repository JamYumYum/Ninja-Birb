using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathWall : MonoBehaviour
{
    [Range(0.1f, 1f)] [Tooltip("Max distance percentage of one tilemap width")]
    public float maxDistancePercent = 0.5f;
    [Tooltip("Movespeed of the deathwall in meters per second (m/v)")]
    public float moveSpeed = 2f;

    private Transform trans;
    private EndlessTerrain world;

    // Start is called before the first frame update
    void Start()
    {
        trans = transform;
        world = FindObjectOfType<EndlessTerrain>();
        trans.SetParent(world.getGrid.transform);
        trans.localPosition = new Vector3(-world.width, world.getGrid.transform.position.y, trans.position.z);

        world.On_world_shift += On_world_shift_action;
    }

    // Update is called once per frame
    void Update()
    {
        //if deathwall is farther than maxDistance permits, set position back to maxDistance
        if(trans.position.x < -(world.width * maxDistancePercent))
        {
            trans.position = new Vector3(-(world.width * maxDistancePercent), trans.position.y, trans.position.z);
        }

        //move deathwall
        trans.localPosition = new Vector3(trans.localPosition.x + moveSpeed * Time.deltaTime, trans.localPosition.y, trans.localPosition.z);
    }
    void On_world_shift_action(object sender, EndlessTerrain.On_world_shift_args args)
    {
        trans.localPosition = new Vector3(trans.localPosition.x - args.distanceX, trans.localPosition.y, trans.localPosition.z);
    }


}
