using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterMovement : MonoBehaviour
{

    private Vector3 newPos;
    private Vector3 currentPos;
    public static Action<GameObject,List<GameObject>> OnTouchEnded;

 

    private List<GameObject> clones = new List<GameObject>();
    private List<GameObject> _clones = new List<GameObject>();
    [SerializeField]
    private GameObject prefab;
    [SerializeField]
    private float frequency;
    // Start is called before the first frame update
    void Start()
    {
        currentPos=transform.position;
        Debug.Log(currentPos);
        Debug.Log(currentPos.magnitude);
      
    }

    // Update is called once per frame
    void Update()
    {
             if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            transform.position=transform.position+ new Vector3(touch.deltaPosition.x,0,touch.deltaPosition.y)*0.5f*Time.deltaTime;
            newPos=transform.position;
    
              if(currentPos.x>newPos.x+frequency || currentPos.x<newPos.x-frequency || currentPos.z>newPos.z+frequency || currentPos.z<newPos.z-frequency){    
            GameObject clone=Instantiate(prefab, currentPos, Quaternion.identity);
            clones.Add(clone);
            currentPos=newPos;


        }

             _clones = new List<GameObject>(clones);

            if(touch.phase == TouchPhase.Ended){
           
               
                OnTouchEnded?.Invoke(this.gameObject,this._clones);
                
            }
            
           
        }

        


     
     
       
    }
}
