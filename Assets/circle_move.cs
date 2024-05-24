using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class circle_move : MonoBehaviour
{
    public float speed = 10f; // La velocidad constante a la que se moverá el círculo
    private Vector3 initialPosition;
    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Movethe rock to the right always
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Know if the rock is the limits
        if (other.CompareTag("end"))
        {
            // Teleport the rock the his started place
            //speed = 0;
            this.transform.position = initialPosition;
        
        }
    }
}
