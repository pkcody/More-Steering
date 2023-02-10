using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : Face
{
    public float wanderOffset;
    float wanderRadious;

    float wanderRate;
    float wanderOrientation;
    public float maxAcceleration;
    public override SteeringOutput getSteering()
    {
        SteeringOutput result = new SteeringOutput();
        Vector3 newPos = character.transform.position + (Random.insideUnitSphere * wanderOffset);
        Vector3 targetPosition = new Vector3(newPos.x, 0, newPos.z);
        //Debug.Log(targetPosition);

        result.linear = targetPosition - character.transform.position;
        

        // give full acceleration along this direction
        result.linear.Normalize();
        //Debug.Log(result.linear);
        result.linear *= maxAcceleration;
        result.linear.y = 0f;


        //result.angular = base.getTargetAngle();
        return result;
    }
    //public override float getSteering()
    //{
    //    wanderOrientation += Random() * wanderRate;
    //    target.transform.rotation = wanderOrientation + character.transform.rotation;
    //    target = character.transform.position + wanderOffset * character.transform.rotation.eulerAngles;


    //    return result;

    //}
}
