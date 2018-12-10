using UnityEngine;
using System.Collections;

public class TrailAnimation : MonoBehaviour
{

    Animator anim ;
    public static bool moving;
    // Use this for initialization
    void Awake()
    {
        moving = false;
    }

    void Start()
    {
    
//        anim = GetComponent<Animator>();
    }
    
    // Update is called once per frame
    void Update()
    {
    
//        if (moving)
//            anim.Play();
    }
}
