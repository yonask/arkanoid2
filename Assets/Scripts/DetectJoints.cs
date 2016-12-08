using UnityEngine;
using System.Collections;
using Windows.Kinect;


public class DetectJoints : MonoBehaviour
{
    public GameObject BodySrcManager;
    public JointType TrackedJoint;
    private BodySourceManager bodyManager;
    private Body[] bodies;
    public float multiplier;
  
    // Use this for initialization
    void Start()
    {

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
    void Update()
    {
        if (bodyManager == null)
        {
            return;
        }

        bodies = bodyManager.GetData();

        if (bodies == null)
        {
            return;
        }
        foreach (var body in bodies)
        {
            if (body == null)
            {
                continue;
            }
            if (body.IsTracked)
            {
              var pos = body.Joints[TrackedJoint].Position;
                gameObject.transform.position = new Vector3 (pos.X * multiplier, -8.72f, 0f);

                if (transform.position.x >= 7.4f)
                {
                    transform.position = new Vector3(7.4f, transform.position.y, transform.position.z);
                }

                if (transform.position.x <= -7.4f)
                {
                    transform.position = new Vector3(-7.4f, transform.position.y, transform.position.z);
                }
                Debug.Log(transform.localScale.x);
                GameObject paddleObject = GameObject.Find("Paddle");
               
                if(paddleObject.transform.localScale.x == 7)
                {
                    if (transform.position.x >= 5.89f)
                    {
                        transform.position = new Vector3(5.89f, transform.position.y, transform.position.z);
                    }

                    if (transform.position.x <= -5.89f)
                    {
                        transform.position = new Vector3(-5.89f, transform.position.y, transform.position.z);
                    }
                }
            }
        }



    }
}