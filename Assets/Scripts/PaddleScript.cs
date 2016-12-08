using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Windows.Kinect;

public class PaddleScript : MonoBehaviour
{
    public float paddlespeed = 10f;
    public GameObject ballprefab;
    public GameObject powerUps;
    
    GameObject attachedBall = null;
    GameObject attachedpowerUp = null;
    int lives = 3;
    GUIText guiLives;
    int score = 0;
    int count = 0;
    public GUISkin scoreboardSkin;
    // Use this for initialization for kinect

    public GameObject BodySrcManager;
    public JointType TrackedJoint;
    private BodySourceManager bodyManager;
    private Body[] bodies;
    public float multiplier;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(GameObject.Find("BodySourceManager"));
        DontDestroyOnLoad(GameObject.Find("Object1"));
        DontDestroyOnLoad(GameObject.Find("guiLives"));

        guiLives = GameObject.Find("guiLives").GetComponent<GUIText>();
        guiLives.text = "Lives:" + lives;
        SpawnBall();


        if (BodySrcManager == null)
        {
            Debug.Log("Assign Game Object with Body source Manager");
        }
        else
        {
            bodyManager = BodySrcManager.GetComponent<BodySourceManager>();
        }

    }

    // Update is called once per frame

    public void OnLevelWasLoaded(int level)
      {
        SpawnBall();
     }

    public void loseLife()
    {
        lives--;
        guiLives.text = "Lives:" + lives;
        if (lives > 0)
        {
            SpawnBall();
        }
        else
        {
            // life lose
            Destroy(gameObject);
            SceneManager.LoadScene("gameOver");
        }
    }
    public void SpawnBall()
    {
        if (ballprefab == null)
        {
            Debug.Log("hey you forget to attach the ball frefab at the inspector");
            return;
        }

        attachedBall = (GameObject)Instantiate(ballprefab, (transform.position + new Vector3(0, .75f, 0)), Quaternion.identity);

       
        
       
    }

    public void powerUp()
    {
        if (powerUps == null)
        {
            Debug.Log("hey you forget to attach the power ups frefab at the inspector");
            return;
        }

        attachedpowerUp = (GameObject)Instantiate(powerUps, (transform.position + new Vector3(0, 10f, 0)), Quaternion.identity);

    }

    void OnGUI()
    {
        GUI.skin = scoreboardSkin;
        GUI.Label(new Rect(0, 0, 100, 100), "Score:" + score);
    }

    public void AddPoint (int v)
    {
        
        score += v;
    }
    void Update()
    {

        //kinect

            if (bodyManager == null)
                {
                    return;
                }

            bodies = bodyManager.GetData();

            if (bodies == null)
                {
                    return;
                }

        //kinect


        //Debug.Log(transform.localScale.x);
        // Debug.Log("count2 " + count);
        if (transform.localScale.x == 7)
            {
                count += 1;
            }
        if (transform.localScale.x == 7 && count == 500)
            {
                transform.localScale += new Vector3(-3f, 0, 0);
            }

        transform.Translate(paddlespeed * Time.deltaTime * Input.GetAxis("Horizontal"), 0, 0);
     

        if (attachedBall)
        {
                Rigidbody ballRigidbody = attachedBall.GetComponent<Rigidbody>();
                ballRigidbody.position = transform.position + new Vector3(0, .75f, 0);

            //the input 

            //kinect
            foreach (var body in bodies)
            {
                if (body == null)
                {
                    continue;
                }
                if (body.IsTracked)
                {
                    var pos = body.Joints[TrackedJoint].Position;
                    //gameObject.transform.position = new Vector3(pos.X * multiplier, -8.72f, 0f);

                    
                   // Debug.Log(transform.localScale.x);
                    //GameObject paddleObject = GameObject.Find("Paddle");

                    /*  if (paddleObject.transform.localScale.x == 7)
                      {
                          if (transform.position.x > 5.89f)
                          {
                              transform.position = new Vector3(5.89f, transform.position.y, transform.position.z);
                          }

                          if (transform.position.x < -5.89f)
                          {
                              transform.position = new Vector3(-5.89f, transform.position.y, transform.position.z);
                          }
                      }
                      */

                    if (pos.Y > 0f)
                    {
                        ballRigidbody.isKinematic = false;
                        ballRigidbody.AddForce(300f * Input.GetAxis("Horizontal"), 300f, 0);
                        attachedBall = null;

                    }


                }
            }

            //kinect







        }

   }

   
     void OnCollisionEnter(Collision col)
    {
        foreach (ContactPoint contact in col.contacts)
        {
            if (contact.thisCollider == GetComponent<Collider>())
            {
                //  This is the paddle's contact point
                float english = contact.point.x - transform.position.x;
                contact.otherCollider.GetComponent<Rigidbody>().AddForce(300f * english, 0, 0);
                
            }
        }

        if(col.gameObject.tag=="powerUps")
        {
            transform.localScale += new Vector3(3f, 0, 0);
            count =0;
           // Debug.Log("count" + count);


        }
    }


}
