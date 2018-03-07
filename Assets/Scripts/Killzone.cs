using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Killzone : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "playerMesh")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Debug.Log("Dead");
        }
        else if(collision.gameObject.tag == "shortEnemy" || collision.gameObject.tag == "mediumEnemy" || collision.gameObject.tag == "tallEnemy" )
        {
            GameObject.Destroy(collision.gameObject);
        }
    }
}
