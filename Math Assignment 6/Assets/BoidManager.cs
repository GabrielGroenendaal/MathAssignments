using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidManager : MonoBehaviour
{
    public GameObject boidPrefab;
    public List<BoidBehavior> boidList = new List<BoidBehavior>();
    public int numOfBoids;
    
    public float boidSeparationDistance = 1f;
    [Range(0,2f)] public float weight_cohesion; public float cohesion_speed;
    [Range(0,2f)] public float weight_alignment; public float alignment_speed;
    [Range(0,2f)] public float weight_separation; public float separation_speed;
    [Range(0,2f)] public float weight_targetSeek; public float targetSeek_speed;

    public float maxSpeed; public float minSpeed;
    public float timer; public float randomTimer;

    void Start()
    {
        for (int i = 0; i < numOfBoids; i++){
            CreateBoid();
        }
        oscillateValues(true);
        randomTimer = Random.Range(.2f,1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 eular = this.transform.eulerAngles;
        Vector3 rotate = new Vector3(0,0,1f);
        this.transform.Rotate(rotate, Space.World);

        weight_alignment += alignment_speed;
        weight_cohesion += cohesion_speed;
        weight_separation += separation_speed;
        weight_targetSeek += targetSeek_speed;

        timer += Time.deltaTime;
        if (timer >= randomTimer) {
            timer = 0;
            randomTimer = Random.Range(.5f,2f);
            oscillateValues(false);
        }
    }

    void CreateBoid(){
        GameObject newBoid = Instantiate(boidPrefab, this.transform.position, Quaternion.identity);
        newBoid.transform.parent = this.transform;
        BoidBehavior newBoidBehavior = newBoid.GetComponent<BoidBehavior>();
        newBoidBehavior.boidManager = this;
        boidList.Add(newBoidBehavior);
    }

    void oscillateValues(bool start){

        if (start){
            weight_cohesion = Random.Range(0f,2f);
            weight_alignment = Random.Range(0f,2f);
            weight_separation = Random.Range(0f,2f);
            weight_targetSeek = Random.Range(0f,2f);
            cohesion_speed = Random.Range(minSpeed, maxSpeed) * (Random.Range(0,2)*2-1);
            alignment_speed = Random.Range(minSpeed, maxSpeed) * (Random.Range(0,2)*2-1);
            separation_speed = Random.Range(minSpeed, maxSpeed) * (Random.Range(0,2)*2-1);
            targetSeek_speed = Random.Range(minSpeed, maxSpeed) * (Random.Range(0,2)*2-1);
        }
        else {
            if (weight_cohesion >= 2f){
                 cohesion_speed = Random.Range(minSpeed, maxSpeed) * -1;
             }

            else if (weight_cohesion <= .1f){
                cohesion_speed = Random.Range(minSpeed, maxSpeed);
            }


            if (weight_separation >= 2f){
                separation_speed = Random.Range(minSpeed, maxSpeed) * -1;
            }
            else if (weight_separation <= .1f){
                separation_speed = Random.Range(minSpeed, maxSpeed) ;
            }


            if (weight_alignment >= 2f){
                alignment_speed = Random.Range(minSpeed, maxSpeed)  * -1;
            }
            else if (weight_alignment <= .1f){
                alignment_speed = Random.Range(minSpeed, maxSpeed);
            }


            
            if (weight_targetSeek >= 2f){
                targetSeek_speed = Random.Range(minSpeed, maxSpeed) * -1;
            }
            else if (weight_targetSeek <= 0.1f){
                targetSeek_speed = Random.Range(minSpeed, maxSpeed);
            }
            
        }
       
    }
}
