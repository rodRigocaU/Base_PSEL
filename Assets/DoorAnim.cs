using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnim : MonoBehaviour
{
    [SerializeField] private Animator anim;

    private float Duration = 1.7f;


    [SerializeField] private GameObject message;
    [SerializeField] private GameObject bones;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void StarAnimation()
    {
        anim.SetBool("DoorAnim", true);
        Invoke("animationTime", Duration);
       
    }

    void animationTime()
    {
        gameObject.SetActive(false);
        message.SetActive(true);
        bones.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }




}
