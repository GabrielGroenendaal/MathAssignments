using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidBehavior : MonoBehaviour
{
    Vector3 velocity;
    public float speed;
    public BoidManager boidManager;
    
    // Start is called before the first frame update
    void Start()
    {
        velocity = new Vector3(Random.value, Random.value, Random.value);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentPosition = this.transform.position;

        Vector3 cohesion = boidManager.transform.position;
        Vector3 alignment = Vector3.zero;
        Vector3 separation = Vector3.zero;

        Vector3 targetSeek = boidManager.transform.position - this.transform.position;

        foreach (BoidBehavior boidFriend in boidManager.boidList) {
            if (boidFriend == this) continue;

            cohesion += boidFriend.transform.position;
            alignment += boidFriend.velocity;

            Vector3 directDifference = this.transform.position - boidFriend.transform.position;

            if (directDifference.magnitude > 0 && directDifference.magnitude < boidManager.boidSeparationDistance){
                separation += boidManager.boidSeparationDistance * (directDifference.normalized / directDifference.magnitude);
            } else if (directDifference.magnitude > 0 && directDifference.magnitude > boidManager.boidSeparationDistance){
                separation -= boidManager.boidSeparationDistance * (directDifference.normalized / directDifference.magnitude);
            }
        }

        alignment /= (boidManager.numOfBoids - 1);
        cohesion /= (boidManager.numOfBoids - 1);
        cohesion = (cohesion - this.transform.position).normalized;
        separation /= (boidManager.numOfBoids - 1);

        Vector3 newVelocity = Vector3.zero;
        newVelocity += alignment * boidManager.weight_alignment;
        newVelocity += cohesion * boidManager.weight_cohesion;
        newVelocity += separation * boidManager.weight_separation;
        newVelocity += targetSeek * boidManager.weight_targetSeek;
        newVelocity.Normalize();

        velocity = MathUtility.Limit((velocity + newVelocity), 2.5f);
        // (velocity + newVelocity) / 2f;
        
        this.transform.up = velocity.normalized;


        this.transform.position = currentPosition + velocity * (speed * Time.deltaTime);
    }
}
