using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followCamara : MonoBehaviour
{

    public Transform objetoASeguir;
    public Vector3 offset;
    // Start is called before the first frame update


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (objetoASeguir != null)
        {
            // Update the position of the camera
            transform.position = objetoASeguir.position + offset;
        }
    }
}
