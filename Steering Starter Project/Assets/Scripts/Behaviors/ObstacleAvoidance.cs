using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleAvoidance : Seek
{
    //    detector: CollisionDetector
    //3
    //4 # The minimum distance to a wall (i.e., how far to avoid
    //5 # collision) should be greater than the radius of the character.
    //6 avoidDistance: float
    public float avoidDistance = 4f;
    //7
    //8 # The distance to look ahead for a collision
    //        # (i.e., the length of the collision ray).
    //10 lookahead: float
    public float lookahead = 6f;
    //11
    //12 # ... Other data is derived from the superclass ...
    //13
    protected override Vector3 getTargetPosition()
    {
        //14 function getSteering():
        //15 # 1. Calculate the target to delegate to seek
        //16 # Calculate the collision ray vector.
        RaycastHit hit;

        if (Physics.Raycast(character.transform.position, character.linearVelocity, out hit, lookahead))
        {
            //Debug.Log("Raycast hit: " + hit.collider.name);
            return hit.point + (hit.normal * avoidDistance);
        }
        else
        {
            return base.getTargetPosition();
        }
        //17 ray = character.velocity
        //18 ray.normalize()
        //19 ray *= lookahead
        //20
        //21 # Find the collision.
        //22 collision = detector.getCollision(character.position, ray)
        //23
        //24 # If have no collision, do nothing.
        //25 if not collision:
        //26 return null
        //27
        //28 # 2. Otherwise create a target and delegate to seek.
        //29 target = collision.position + collision.normal* avoidDistance
        //30 return Seek.getSteering()
    }
}
