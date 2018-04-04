using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trapdoorScript : MonoBehaviour {

    public Animator anim;
    public bool inRange;
    public bool isOpen;
	// Use this for initialization
	void Start () {
        anim = this.gameObject.GetComponent<Animator>();
        anim.runtimeAnimatorController = Resources.Load("../Scenery/Model_Imports/TrapDoor/trapdoorAnimator") as RuntimeAnimatorController;
        inRange = false;
        isOpen = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (inRange && !isOpen)
        {
            anim.SetTrigger("Open");
        }
        else if (!inRange && isOpen)
        {
            anim.SetTrigger("Close");
        }
        else if (!inRange && !isOpen)
        {
            anim.SetTrigger("Close");
        }
	}
}
