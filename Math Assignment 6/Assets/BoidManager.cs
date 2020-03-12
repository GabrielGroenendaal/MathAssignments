using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidManager : MonoBehaviour
{
    public GameObject boidPrefab;
    public List<BoidBehavior> boidList = new List<BoidBehavior>();
    public int numOfBoids;
    
    public float boidSeparationDistance = 1f;

    [Range(0,2f)] public float weight_cohesion;
    [Range(0,2f)] public float weight_alignment;
    [Range(0,2f)] public float weight_separation;
    [Range(0,2f)] public float weight_targetSeek;

    void Start()
    {
        for (int i = 0; i < numOfBoids; i++){
            CreateBoid();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateBoid(){
        GameObject newBoid = Instantiate(boidPrefab, this.transform.position, Quaternion.identity);
        newBoid.transform.parent = this.transform;
        BoidBehavior newBoidBehavior = newBoid.GetComponent<BoidBehavior>();
        newBoidBehavior.boidManager = this;
        boidList.Add(newBoidBehavior);
    }
}
