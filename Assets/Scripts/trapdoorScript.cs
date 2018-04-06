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
        //anim.runtimeAnimatorController = Resources.Load("../Scenery/Model_Imports/TrapDoor/trapdoorAnimator") as RuntimeAnimatorController;
        inRange = false;
        isOpen = false;
	}
	
	// Update is called once per frame
	void Update () {
        //Check if player is in range, if so - anim.setBool("inRange", true)
        //This script should be set to just the trapdoor and utilized as an independent thing
        //Enemy script can spawn them but this should handle the animation independent of it
        //Should be able to tell it to play Open based on when these variables become true
        //Open animation currently set to legacy animation type and on 'ping pong' meaning it would just reverse its animation when it's false

        if (inRange && !isOpen)
        {
            anim.SetBool("isOpen", true);
        }
        else if (!inRange && isOpen)
        {
            anim.SetBool("isOpen", false);
        }
        else if (!inRange && !isOpen)
        {
            anim.SetBool("isOpen", false);
        }
	}
}
