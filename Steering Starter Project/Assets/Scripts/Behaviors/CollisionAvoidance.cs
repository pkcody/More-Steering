using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionAvoidance : SteeringBehavior
{
    public float maxAcceleration =1f;
    public Kinematic character;

    //5 # A list of potential targets.
    //6 targets: Kinematic[]
    public Kinematic[] targets;
    //7
    //8 # The collision radius of a character (assuming all characters
    //9 # have the same radius here).
    //10 radius: float
    public float radius = 1f;
    //11
    //12 function getSteering() -> SteeringOutput:
    //13 # 1. Find the target that’s closest to collision
    //14 # Store the first collision time.
    //15 shortestTime: float = infinity
    public override SteeringOutput getSteering()
    {
        float shortestTime = float.PositiveInfinity;
        //16
        //17 # Store the target that collides then, and other data that we
        //18 # will need and can avoid recalculating.
        //19 firstTarget: Kinematic = null
        //20 firstMinSeparation: float
        //21 firstDistance: float
        //22 firstRelativePos: Vector
        //23 firstRelativeVel: Vector
        Kinematic firstTarget = null;
        float firstMinSeperation = float.PositiveInfinity;
        float firstDistance = float.PositiveInfinity;
        Vector3 firstRelativePos = Vector3.positiveInfinity;
        Vector3 firstRelativeVel = Vector3.zero;

        Vector3 relativePos = Vector3.positiveInfinity;
        //24
        //25 # Loop through each target.
        //26 for target in targets:
        foreach (Kinematic target in targets)
        {
            //27 # Calculate the time to collision.
            //28 relativePos = target.position - character.position
            //29 relativeVel = target.velocity - character.velocity
            //30 relativeSpeed = relativeVel.length()
            //31 timeToCollision = dotProduct(relativePos, relativeVel) /
            //32 (relativeSpeed* relativeSpeed)
            relativePos = target.transform.position - character.transform.position;
            Vector3 relativeVel = character.linearVelocity - target.linearVelocity;
            float relativeSpeed = relativeVel.magnitude;
            float timeToCollision = Vector3.Dot(relativePos, relativeVel) / (relativeSpeed * relativeSpeed);



            //33
            //34 # Check if it is going to be a collision at all.
            //3.3 Steering Behaviors 89
            //35 distance = relativePos.length()
            //36 minSeparation = distance - relativeSpeed* timeToCollision
            //37 if minSeparation > 2 * radius:
            //38 continue
            float distance = relativePos.magnitude;
            float minSeperation = distance - relativeSpeed * timeToCollision;
            if (minSeperation > 2 * radius)
            {
                continue;
            }

            //39
            //40 # Check if it is the shortest.
            //41 if timeToCollision > 0 and timeToCollision<shortestTime:
            //42 # Store the time, target and other data.
            //43 shortestTime = timeToCollision
            //44 firstTarget = target
            //45 firstMinSeparation = minSeparation
            //46 firstDistance = distance
            //47 firstRelativePos = relativePos
            //48 firstRelativeVel = relativeVel
            if (timeToCollision > 0 && timeToCollision < shortestTime)
            {
                shortestTime = timeToCollision;
                firstTarget = target;
                firstMinSeperation = minSeperation;
                firstDistance = distance;
                firstRelativePos = relativePos;
                firstRelativeVel = relativeVel;
            }

            //49
        }

        //50 # 2. Calculate the steering
        //51 # If we have no target, then exit.
        //52 if not firstTarget:
        //53 return null
        if (firstTarget == null)
        {
            return null;
        }
        //////////Doing it better
        //54
        //55 # If we’re going to hit exactly, or if we’re already
        //56 # colliding, then do the steering based on current position.
        //57 if firstMinSeparation <= 0 or firstDistance < 2 * radius:
        //58 relativePos = firstTarget.position - character.position
        //59
        //60 # Otherwise calculate the future relative position.
        //61 else:
        //62 relativePos = firstRelativePos +
        //63 firstRelativeVel* shortestTime
        //64
        //65 # Avoid the target.
        //66 relativePos.normalize()
        //67
        //68 result = new SteeringOutput()
        //69 result.linear = relativePos * maxAcceleration
        //70 result.anguar = 0
        //71 return result
        /////////////////
        ///

        SteeringOutput result = new SteeringOutput();

        float dotResault = Vector3.Dot(character.linearVelocity.normalized, firstTarget.linearVelocity.normalized);
        if(dotResault < -0.9)
        {
            result.linear = new Vector3(character.linearVelocity.z, 0.0f, character.linearVelocity.x);

        }
        else
        {
            result.linear = -firstTarget.linearVelocity;

        }



        result.linear.Normalize();
        result.linear *= maxAcceleration;
        result.angular = 0;

        return result;

    }

    

}
