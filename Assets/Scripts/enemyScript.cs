using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class enemyScript : MonoBehaviour {

    public GameObject currEnemy;
    public GameObject playerObj;
    public GameObject cover;
    public GameObject trapDoor;
    public GameObject bat;
    
    private float moveSpeed;
    private float xVal;
    private float yVal;
    private float zVal;
    private float startXVal;
    private float startYVal;
    private float startZVal;
    private bool goLeft;
    private bool goRight;
    private bool goUp;
    private bool goDown;
    public bool isGrounded;
    private bool hitWithBat;
    private OVRPlayerController ovrScript;
    private trapdoorScript tdScript;
    private int iterator = 0;
    // Use this for initialization
    void Start () {
        currEnemy = this.gameObject;
        moveSpeed = 5.0f;
        startXVal = currEnemy.transform.position.x;
        startYVal = currEnemy.transform.position.y;
        startZVal = currEnemy.transform.position.z;
        xVal = currEnemy.transform.position.x;
        yVal = currEnemy.transform.position.y;
        zVal = currEnemy.transform.position.z;
        goLeft = false;
        goRight = false;
        goUp = false;
        goDown = false;
        hitWithBat = false;
        isGrounded = true;
        ovrScript = playerObj.GetComponent<OVRPlayerController>();
        if(currEnemy.gameObject.tag == "tallEnemy")
        {
            GameObject TrapDoor = Instantiate(trapDoor, new Vector3(startXVal, startYVal + 4.75f, startZVal), Quaternion.Euler(0, 0, 0));
            GameObject Cover = Instantiate(cover, new Vector3(startXVal, startYVal + 4.75f, startZVal), Quaternion.Euler(90, 0, 0));
            currEnemy.transform.parent = Cover.transform;
            Cover.transform.parent = TrapDoor.transform;
            //TrapDoor.transform.parent = currEnemy.transform;
            //Cover.transform.parent = currEnemy.transform;
            iterator++;
        }
        determinePathZ();
        
    }

    // Update is called once per frame
    void Update () {
        /*if (currEnemy.transform.tag == "tallEnemy")
        {
            currEnemy.transform.GetChild(12).position = new Vector3(startXVal, startYVal + 4.75f, startZVal);
            currEnemy.transform.GetChild(13).position = new Vector3(startXVal, startYVal + 4.75f, startZVal);
        }*/
        if (currEnemy.gameObject.tag == "shortEnemy")
        {
            if (isGrounded)
            {
                currEnemy.transform.position = new Vector3(xVal, yVal, zVal);
                isHit();
            }
            if (goLeft)
                moveLeft();
            else if (goRight)
                moveRight();
        }
        else if(currEnemy.gameObject.tag == "tallEnemy")
        {
            currEnemy.transform.position = new Vector3(xVal, yVal, zVal);
            determinePathY();
            if (goUp)
                popUp();
            else if (goDown)
                popDown();
        }
        else if(currEnemy.gameObject.tag == "mediumEnemy")
        {
            if(isGrounded)
            {
                determinePathing();
                isHit();
            }
        }
	}

    void moveLeft()
    {
        zVal += moveSpeed * Time.deltaTime;
    }

    void moveRight()
    {
        zVal -= moveSpeed * Time.deltaTime;
    }

    void popUp()
    {
        yVal += 1.8f * Time.deltaTime;
    }
    void popDown()
    {
        yVal -= 1.8f * Time.deltaTime;
    }
    void determinePathZ()
    {
        // determine which way enemy should path first for z value
        if (startZVal > 0)
            goLeft = true;
        else if (startZVal < 0)
            goRight = true;
    }

    void determinePathY()
    {
        tdScript = trapDoor.GetComponent<trapdoorScript>();
        if (Vector3.Distance(currEnemy.transform.position, playerObj.transform.position) <= 20)
        {
            tdScript.inRange = true;
            //determine if enemy should move up or down with y value
            if (yVal == startYVal)
            {
                goUp = true;
            }
            else if (yVal <= startYVal + .1f)
            {
                goDown = false;
                goUp = true;
            }
            else if (yVal >= startYVal + 4.75)
            {
                goUp = false;
                goDown = true;
            }
        }
        else
        {
            tdScript.inRange = false;
            if (goUp)
            {
                if (yVal >= startYVal + 4.75)
                {
                    goUp = false;
                    goDown = true;
                }
            }
            else if(goDown)
            {
                if (yVal <= startYVal)
                {
                    goDown = false;
                    yVal = startYVal;
                }
            }
        }
    }

    void determinePathing()
    {
        // determine if enemy should seek the player 
        if (Vector3.Distance(currEnemy.transform.position, playerObj.transform.position) <= 15)
        {
            var lookDir = playerObj.transform.position - currEnemy.transform.position;
            lookDir.y = 0f; 

            currEnemy.transform.rotation = Quaternion.LookRotation(lookDir, Vector3.up);
            currEnemy.transform.position += (transform.forward) * moveSpeed * Time.deltaTime;
        }
    }

    void isHit()
    {
        if(hitWithBat)
        {
            isGrounded = false;
            hitWithBat = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (this.gameObject.tag == "shortEnemy" || this.gameObject.tag == "mediumEnemy")
        {
            if (collision.gameObject.tag == "bat")
            {
                Vector3 force = ovrScript.getSwingVelocity();
                Vector3 direction = collision.contacts[0].point - this.gameObject.transform.position;
                direction = -direction.normalized;
                this.gameObject.GetComponent<Rigidbody>().AddForce(direction.x * (force.x + 10), direction.y * force.y, direction.z * (force.z + 10), ForceMode.Impulse);
                //hitWithBat = true;
            }
            else if (collision.gameObject.tag == "Player")
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

            }
            else if(collision.gameObject.tag == "platform")
            {
                isGrounded = true;
            }
        }

        else if (this.gameObject.tag == "tallEnemy")
        {
            if (collision.gameObject.tag == "Player")
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            else if(collision.gameObject.tag == "bat")
            {
                Vector3 force = new Vector3(0, 0, 0);
                Vector3 direction = collision.contacts[0].point - this.gameObject.transform.position;
                direction = -direction.normalized;
                this.gameObject.GetComponent<Rigidbody>().AddForce(direction.x * (force.x + 10), direction.y * force.y, direction.z * (force.z + 10), ForceMode.Impulse);
            }
        }
        else if(this.gameObject.tag == "platform")
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision other)
    {
        if(other.gameObject.tag == "platform")
        {
            isGrounded = false;
        }
            
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "boundary" && isGrounded)
        {
            goLeft = !goLeft;
            goRight = !goRight;
        }
    }

}
