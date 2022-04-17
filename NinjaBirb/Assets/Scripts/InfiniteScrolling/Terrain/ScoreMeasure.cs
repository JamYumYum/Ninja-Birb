using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreMeasure : MonoBehaviour, IMeasureScore
{
    private float Score = 0f;
    public float score { get => Score; set => Score = value; }

    private float distanceTravelled = 0f;

    private EndlessTerrain endless;
    private Rigidbody2D rb;
    

    // Start is called before the first frame update
    void Start()
    {
        endless = GetComponent<EndlessTerrain>();
        rb = endless.getGrid.GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
        Score = distanceTravelled;
        
    }

    void FixedUpdate()
    {
        distanceTravelled -= rb.velocity.x*Time.fixedDeltaTime;
    }
}
