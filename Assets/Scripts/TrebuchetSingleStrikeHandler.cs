using UnityEngine;
using System.Collections;

public class TrebuchetSingleStrikeHandler : MonoBehaviour {

    private int tap = 0;
	void Start () {
        GameObject.Find("TrebuchetWeightHandler").GetComponent<Rigidbody2D>().isKinematic = true;
        tap = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ReleaseStone() {
        GameObject.Find("ScoreManager").GetComponent<ScoreManager>().shotCount++;
        GameObject.Find("Rock").GetComponent<HingeJoint2D>().enabled = false;
        GameObject.Find("Rock").tag = "RockReleased";
    }

    public void ReleaseTrebuchet() {
        GameObject.Find("TrebuchetWeightHandler").GetComponent<Rigidbody2D>().isKinematic = false;
        GameObject.Find("TrebuchetStand").GetComponent<DistanceJoint2D>().enabled = false;
    }

    public void TrebuchetAction(){
        Debug.Log("Tap");
        tap++;
        if (tap == 1) {
            ReleaseTrebuchet();
        }
        else if(tap == 2){
            ReleaseStone();
            tap = 0;
        }
    }
}
