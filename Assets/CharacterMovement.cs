using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterMovement : MonoBehaviour
{

    private Vector3 newPos;
    private Vector3 currentPos;
    public static Action<GameObject,List<GameObject>> OnTouchEnded;


    private Touch _touch;

    private bool _movementStarted;
    private Vector3 _initialTouch;
    private Vector3 _currentTouch;

    private Vector3 _movementVector;
    private  Quaternion _rotationVector;


    private float count;
    
    private float oldCount=0;


    private bool _isMoving;

    private Vector3 _dragPos;
 
 

    private List<GameObject> clones = new List<GameObject>();
    private List<GameObject> _clones = new List<GameObject>();
    [SerializeField]
    private GameObject prefab;
    [SerializeField]
    private float movementSpeed,rotationSpeed;
    [SerializeField]
    private float frequency;


    private Vector3 lastPosition;

    // Start is called before the first frame update
    void Start()
    {
        currentPos=transform.position;
        newPos=transform.position;
        lastPosition = transform.position;
        count=frequency*Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        
           if (Input.touchCount > 0)
        {
            TouchInputHandler();
            MovementHandler();
         
        }

        currentPos=transform.position;
       
       
        newPos=currentPos;


        // if(currentPos.x>newPos.x+frequency || currentPos.x<newPos.x-frequency || currentPos.z>newPos.z+frequency || currentPos.z<newPos.z-frequency){    
        //     GameObject clone=Instantiate(prefab, currentPos, Quaternion.identity);
        //     clones.Add(clone);
        //     newPos=currentPos;


        // }

   
        
        
    }



    void TouchInputHandler(){

        
            _touch = Input.GetTouch(0);

            if(_touch.phase == TouchPhase.Began){
                _movementStarted=true;
                _initialTouch=_touch.position;

            }

            if(_movementStarted){

                if(_touch.phase == TouchPhase.Moved){
   
                    _currentTouch=_touch.position;
                    
                   
  
                }

                _movementVector=_currentTouch-_initialTouch;

                if(_touch.phase == TouchPhase.Ended){
                    _movementStarted=false;
                    OnTouchEnded?.Invoke(this.gameObject,this.clones);

                }

            }
            

          
        

    }

    void MovementHandler(){
        
        _movementVector.z=_movementVector.y;
        _movementVector.y=0;
       
     
        Quaternion _rotationVector =Quaternion.LookRotation(_movementVector,Vector3.up);
       
    
    
        transform.Translate(Vector3.forward* Time.deltaTime*movementSpeed);
        count+=Time.deltaTime;
        if(count>=(oldCount+frequency*Time.deltaTime)){
             Debug.Log(count);
             count=0;
            GameObject clone=Instantiate(prefab, transform.position, Quaternion.identity);
            clones.Add(clone);
        }
       
        transform.rotation=Quaternion.RotateTowards(transform.rotation,_rotationVector,rotationSpeed*Time.deltaTime);



    }


}
