using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trapdoorScript : MonoBehaviour {

    private Animator anim;
    public bool inRange;
    private GameObject player;
    private GameObject[] tallEnemy;
    private int iterator;
	// Use this for initialization
	void Start () {
        anim = this.gameObject.GetComponent<Animator>();
        inRange = false;
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update () {
        //Check if player is in range, if so - anim.setBool("inRange", true)
        //This script should be set to just the trapdoor and utilized as an independent thing
        //Should be able to tell it to play Open based on when these variables become true
        this.gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        if (Vector3.Distance(this.gameObject.transform.position, player.transform.position) <= 20)
        {
            anim.SetBool("inRange", true);
            inRange = true;
        }
        else if(this.gameObject.transform.GetChild(6).GetChild(0) != null)
        {
            if (this.gameObject.transform.GetChild(6).GetChild(0).position.y < 1)
            {
                anim.SetBool("inRange", false);
                inRange = false;
            }
        }
	}
}
