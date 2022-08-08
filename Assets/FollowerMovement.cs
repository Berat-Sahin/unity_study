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

    private List<GameObject> clonesToFollow = new List<GameObject>();


    void Start(){
        StartMoving=false;
        targetIndex=0;
    }

    // Update is called once per frame
    void Update()
    {

        if(StartMoving==true){
            
            if( targetIndex>=clonesToFollow.Count){
              
                StartMoving=false;
                return;
            }

        transform.position = Vector3.MoveTowards(transform.position,  clonesToFollow[targetIndex].transform.position, followSpeed*0.1f);
        if(transform.position.magnitude-clonesToFollow[targetIndex].transform.position.magnitude == 0){
            Destroy(clonesToFollow[targetIndex]);
            targetIndex+=1;
        }

        }

       

        
      
    }

    
    
    private void UpdateTarget(GameObject mainBall, List<GameObject> _clones)
    {

        StartMoving=true;
        clonesToFollow = new List<GameObject>(_clones) ;  
       
     
    }
    
    private void OnEnable()
    {
        CharacterMovement.OnTouchEnded += UpdateTarget;
    }

    private void OnDisable()
    {
        CharacterMovement.OnTouchEnded -= UpdateTarget;
    }

}
