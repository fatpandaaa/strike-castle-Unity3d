using UnityEngine;
using System.Collections;

public class TrebuchetMultiStrikeHandler : MonoBehaviour {

    private int tap = 0;
    void Start()
    {
        GameObject.Find("TrebuchetWeightHandler").GetComponent<Rigidbody2D>().isKinematic = true;
        tap = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ReleaseStone()
    {
        GameObject.Find("ScoreManager").GetComponent<ScoreManager>().shotCount++;
        GameObject[] tmpObj = GameObject.FindGameObjectsWithTag("Rock");
        for (int i = 0; i < tmpObj.Length; i++) {
            DistanceJoint2D[] tmpDist = tmpObj[i].GetComponents<DistanceJoint2D>();
            for (int j = 0; j < tmpDist.Length; j++) {
                tmpDist[j].enabled = false;
            }
                
            tmpObj[i].GetComponent<HingeJoint2D>().enabled = false;
            tmpObj[i].tag = "RockReleased";
        }
    }

    public void ReleaseTrebuchet()
    {
        GameObject.Find("TrebuchetWeightHandler").GetComponent<Rigidbody2D>().isKinematic = false;
        GameObject.Find("TrebuchetStand").GetComponent<DistanceJoint2D>().enabled = false;
    }

    public void TrebuchetAction()
    {
        tap++;
        if (tap == 1)
        {
            ReleaseTrebuchet();
        }
        else if (tap == 2)
        {
            ReleaseStone();
            tap = 0;
        }
    }
}
