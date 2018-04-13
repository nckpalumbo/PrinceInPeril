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
        iterator = GameObject.FindGameObjectsWithTag("tallEnemy").Length;
        Debug.Log(iterator);
        /*for(int i = 0; i < iterator; i++)
        {
            GameObject trapDoor = GameObject.Find("TrapDoor(Clone)");
            GameObject bishop = GameObject.Find("Bishop (" + (i + 1) + ")");
            if (!trapDoor.transform.IsChildOf(bishop.transform))
            {
                this.gameObject.transform.parent = bishop.transform;
                break;
            }
        }*/

    }

    // Update is called once per frame
    void Update () {
        //Check if player is in range, if so - anim.setBool("inRange", true)
        //This script should be set to just the trapdoor and utilized as an independent thing
        //Should be able to tell it to play Open based on when these variables become true

        if (Vector3.Distance(this.gameObject.transform.position, player.transform.position) <= 20)
        {
            anim.SetBool("inRange", true);
            inRange = true;
        }
        else
        {
            anim.SetBool("inRange", false);
            inRange = false;
        }
	}
}
