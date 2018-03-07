using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingplatformScript : MonoBehaviour {

    public GameObject currPlatform;
    public int direction;
    private float xVal;
    private float yVal;
    private float zVal;
    // Use this for initialization
    void Start () {
        currPlatform = this.gameObject;
        xVal = currPlatform.transform.position.x;
        yVal = currPlatform.transform.position.y;
        zVal = currPlatform.transform.position.z;
    }
	
	// Update is called once per frame
	void Update () {
        currPlatform.transform.position = new Vector3(xVal, yVal, zVal);

        if (currPlatform.gameObject.tag == "movingPlatformX")
        {
            movePlatformX();
        }
        else if(currPlatform.gameObject.tag == "movingPlatformY")
        {
            movePlatformY();
        }
        else if(currPlatform.gameObject.tag == "movingPlatformZ")
        {
            movePlatformZ();
        }

	}

    void movePlatformX()
    {
        if(direction == 0)
        {
            xVal += 4f * Time.deltaTime;
        }
        else if(direction == 1)
        {
            xVal -= 4f * Time.deltaTime;
        }
    }
    void movePlatformY()
    {
        if(direction == 0)
        {
            yVal += 6f * Time.deltaTime;
        }
        else if (direction == 1)
        {
            yVal -= 6f * Time.deltaTime;
        }
    }
    void movePlatformZ()
    {
        if(direction == 0)
        {
            zVal += 4f * Time.deltaTime;
        }
        else if (direction == 1)
        {
            zVal -= 4f * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "boundary")
        {
            if (direction == 0)
                direction = 1;
            else if (direction == 1)
                direction = 0;
        }
    }

}
