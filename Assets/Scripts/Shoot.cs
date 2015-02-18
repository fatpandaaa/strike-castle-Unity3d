using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {

    private bool isFollowing = false;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.anyKeyDown) { 
            GetComponent<HingeJoint2D>().enabled = false;
            isFollowing = true;
        }
        if (isFollowing) {
            Vector3 tmp = new Vector3(this.transform.position.x, this.transform.position.y, Camera.main.transform.position.z);
            //Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, tmp, Time.deltaTime * 30f);
            //Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, tmp, Time.deltaTime * 10f);
        }
	}
}
