using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerStart : MonoBehaviour {

    public GameObject player;
    public GameObject cam;
    private movingplatformScript mScript;
	// Use this for initialization
	void Start () {
        //player.transform.localEulerAngles = new Vector3 (0, 80, 0);
        //cam.transform.localEulerAngles = new Vector3(0, 80, 0);
	}
	
	// Update is called once per frame
	void Update () {
        //player.transform.localEulerAngles = new Vector3(player.transform.localEulerAngles.x, cam.transform.localEulerAngles.y, player.transform.localEulerAngles.z);
        player.transform.rotation = Quaternion.LookRotation(player.transform.forward);
    }

   
    private void OnCollisionStay(Collision collision)
    {
        /*if (collision.gameObject.tag == "movingPlatformZ")
        {
            mScript = collision.gameObject.GetComponent<movingplatformScript>();
            if(mScript.direction == 0)
                player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z + .1f);
            else if(mScript.direction == 1)
                player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z - .1f);

        }*/
    }    

}
