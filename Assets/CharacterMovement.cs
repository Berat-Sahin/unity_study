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

    [SerializeField]
    private GameObject _button;
    [SerializeField]
    private GameObject _largeButton;



    private Vector3 lastPosition;

    // Start is called before the first frame update
    void Start()
    {
        currentPos=transform.position;
        newPos=transform.position;
        lastPosition = transform.position;
        count=frequency*Time.deltaTime;
        _button.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
           if (Input.touchCount > 0)
        {
            TouchInputHandler();
        }
        
        
    }



    void TouchInputHandler(){

        
            _touch = Input.GetTouch(0);

            if(_touch.phase == TouchPhase.Began){
                _movementStarted=true;


                _button.SetActive(true);
                _button.transform.position=_touch.position;

                _largeButton.SetActive(true);
                _largeButton.transform.position=_touch.position;

                _initialTouch=_touch.position;
                _currentTouch=_touch.position;

            }

            if(_movementStarted){

                if(_touch.phase == TouchPhase.Moved){

                    _currentTouch=_touch.position;    
                    _largeButton.transform.position=_touch.position;

                }

                

                if(_touch.phase == TouchPhase.Ended){
                    _movementStarted=false;
                    _button.SetActive(false);
                    _largeButton.SetActive(false);
                    OnTouchEnded?.Invoke(this.gameObject,this.clones);

                }

                _movementVector=_currentTouch-_initialTouch;


                MovementHandler();
                shadowHandler();

            }
            

          
        

    }

    void MovementHandler(){
        
        _movementVector.z=_movementVector.y;
        _movementVector.y=0;
       
     
   

        float speed=Mathf.Clamp(_movementVector.magnitude/25,0,movementSpeed);

        if(speed==movementSpeed){
            Debug.Log("maks speed");
        }

       
        transform.Translate(Vector3.forward*Time.deltaTime*speed);
        
      

        count+=Time.deltaTime;
        if(count>=(oldCount+frequency*Time.deltaTime)){
            
             count=0;

        }
       

        if (!_movementVector.Equals(Vector3.zero)) {

            Quaternion _rotationVector =Quaternion.LookRotation(_movementVector,Vector3.up);
            
            transform.rotation=Quaternion.RotateTowards(transform.rotation,_rotationVector,rotationSpeed*Time.deltaTime);
   
            }
        



    }

    void shadowHandler(){

        Debug.Log(transform.position.magnitude-currentPos.magnitude);

        Debug.Log(frequency-currentPos.magnitude);

        if((transform.position-currentPos).magnitude> frequency){
            GameObject clone=Instantiate(prefab, transform.position, Quaternion.identity);
            currentPos=transform.position;
            clones.Add(clone);

            
        }
        
    }


}
