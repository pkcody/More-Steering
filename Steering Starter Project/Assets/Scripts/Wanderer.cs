using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wanderer : Kinematic
{
    Wander myMoveType;
    Face myRotateType;
    public float offSet;


    // Start is called before the first frame update
    void Start()
    {
        myMoveType = new Wander();
        myMoveType.character = this;
        myMoveType.target = myTarget;
        myMoveType.wanderOffset = offSet;
        myMoveType.maxAcceleration = maxSpeed;

        myRotateType = new Face();
        myRotateType.character = this;
        myRotateType.target = myTarget;
    }

    // Update is called once per frame
    protected override void Update()
    {
        steeringUpdate = new SteeringOutput();
        steeringUpdate.linear = myMoveType.getSteering().linear;
        steeringUpdate.angular = myRotateType.getSteering().angular;
        //steeringUpdate = myMoveType.getSteering();
        base.Update();
    }
}
