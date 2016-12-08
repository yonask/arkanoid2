using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class BrickScript : MonoBehaviour {
    static int numBricks = 0;
    public int pointValue = 1;
    public int hitpoints = 1;
	// Use this for initialization
	void Start () {
        numBricks++;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision col)
    {
        hitpoints--;
        if (hitpoints <=0)
        {
            Die();
        }

    }
    void Die () {
            Destroy(gameObject);
            PaddleScript paddleScript = GameObject.Find("Paddle").GetComponent<PaddleScript>();
            paddleScript.AddPoint(pointValue);
            numBricks--;

          //  Debug.Log(numBricks);
             
           if(numBricks ==4)
            {
            paddleScript.powerUp();
            }

            if (numBricks <= 0)
            {
            SceneManager.LoadScene("level2");
            }
        }
        

    
  }
