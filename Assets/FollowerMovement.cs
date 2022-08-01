using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FollowerMovement : MonoBehaviour
{
    public int targetIndex;
    private bool StartMoving;
    private GameObject targetClone;
    
    [SerializeField]
    private float followSpeed=0.5f;

    private List<GameObject> clones = new List<GameObject>();


    void Start(){
        StartMoving=false;
        targetIndex=0;
    }

    // Update is called once per frame
    void Update()
    {

        if(StartMoving==true){
            
            if( targetIndex>=clones.Count){
              
                StartMoving=false;
                return;
            }

            
            transform.position = Vector3.MoveTowards(transform.position,  clones[targetIndex].transform.position, 0.1f*followSpeed);

          
        
          
        }

        
      
    }

    
    
    private void UpdateTarget(GameObject mainBall, List<GameObject> _clones)
    {

        StartMoving=true;
        clones = _clones ;  
       
     
    }
    
    private void OnEnable()
    {
        CharacterMovement.OnTouchEnded += UpdateTarget;
    }

    private void OnDisable()
    {
        CharacterMovement.OnTouchEnded -= UpdateTarget;
    }

    private void OnTriggerEnter(Collider other) {

        
        if(other.gameObject.tag == "Clone"){
            Debug.Log("carptım");
            if(other.gameObject.GetInstanceID()==clones[targetIndex].GetInstanceID()){
            Destroy(other.gameObject);
            targetIndex+=1;
            }
    
        };
        
    }
}
