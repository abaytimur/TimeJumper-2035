using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCam : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Vector3 targetOffset;
    [SerializeField] float MoveSpeed;

     // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveCam();


    }
    void MoveCam()
    {
        transform.position = Vector3.Slerp(transform.position, target.position+targetOffset, MoveSpeed * Time.deltaTime);
    }
  
}
