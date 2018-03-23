using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class enemyScript : MonoBehaviour {

    public GameObject currEnemy;
    public GameObject playerObj;
    public GameObject cover;
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

    // Use this for initialization
    void Start () {
        currEnemy = this.gameObject;
        moveSpeed = 4.0f;
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
            GameObject.Instantiate(cover, new Vector3(startXVal, startYVal + 4.75f, startZVal), Quaternion.Euler(90, 0, 0));
            
        }
        determinePathZ();
        
    }

    // Update is called once per frame
    void Update () {

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
        if (Vector3.Distance(currEnemy.transform.position, playerObj.transform.position) <= 20)
        {
            //determine if enemy should move up or down with y value
            if (yVal == startYVal)
                goUp = true;
            else if (yVal <= startYVal)
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
            //var lookPos = playerObj.transform.position - currEnemy.transform.position;
            //lookPos.y = 0;

            //currEnemy.transform.forward = (playerObj.transform.position - currEnemy.transform.position).normalized;
            //var target = new Vector3(playerObj.transform.position.x, playerObj.transform.position.y, playerObj.transform.position.z);

            //transform.LookAt(playerObj.transform);
            

            var lookDir = playerObj.transform.position - currEnemy.transform.position;
            lookDir.y = 0f; 

            currEnemy.transform.rotation = Quaternion.LookRotation(lookDir, Vector3.up);
            currEnemy.transform.position += (transform.forward) * 2.5f * Time.deltaTime;
            

            //currEnemy.transform.position += lookPos * 0.5f * Time.deltaTime;

            //currEnemy.transform.position = new Vector3(currEnemy.transform.position.x + currEnemy.transform.forward.x * moveSpeed * Time.deltaTime, currEnemy.transform.position.y, currEnemy.transform.position.z + currEnemy.transform.forward.z * moveSpeed * Time.deltaTime);
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
                //float force = 50;
                Vector3 force = ovrScript.getSwingVelocity();
                Vector3 direction = collision.contacts[0].point - this.gameObject.transform.position;
                direction = -direction.normalized;
                this.gameObject.GetComponent<Rigidbody>().AddForce(direction.x * force.x, 0, direction.z * force.z, ForceMode.Impulse);
                //hitWithBat = true;
                Debug.Log("hit by bat");
            }
            else if (collision.gameObject.tag == "Player")
            {
                Debug.Log("hit by " + collision.gameObject.tag);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

            }
        }

        else if (this.gameObject.tag == "tallEnemy")
        {
            if (collision.gameObject.tag == "Player")
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
            //Debug.Log("not on platform");
        }
            
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "boundary" && isGrounded)
        {
            goLeft = !goLeft;
            goRight = !goRight;
        }

        /*if (this.gameObject.tag == "shortEnemy" || this.gameObject.tag == "mediumEnemy")
        {
            if (other.gameObject.tag == "bat")
            {
                Destroy(currEnemy);
                hitWithBat = true;
                Debug.Log("hit by bat");
            }
            else if (other.gameObject.tag == "Player")
            {
                Debug.Log("hit by " + other.gameObject.tag);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

            }
        }

        else if (this.gameObject.tag == "tallEnemy")
        {
            if (other.gameObject.tag == "Player")
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }*/
    }

}
