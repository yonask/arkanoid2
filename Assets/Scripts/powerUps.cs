using UnityEngine;
using System.Collections;

public class powerUps : MonoBehaviour {

  //  public GameObject Paddle;
    

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
   void OnCollisionEnter(Collision col)
    {
       // Paddle.transform.localScale += new Vector3(3f, 0, 0);
        Destroy(gameObject);
    }
}
