using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EndLevel : MonoBehaviour
{

    // Use this for initialization
    public GameObject endLevel;
    public GameObject player;
    public int world;
    public int nextLevel;
    
    void Start()
    {
        nextLevel = SceneManager.GetActiveScene().buildIndex + 1;
        world = 1;
    }

    // Update is called once per frame
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Portal")
        {
            //Initiate.Fade(world + "-" + (nextLevel+1), UnityEngine.Color.black, 0.75f);
            SceneManager.LoadScene(nextLevel);
            
        }
    }

}