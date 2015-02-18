using UnityEngine;
using System.Collections;

public class ProjectileFollow : MonoBehaviour {

    public Transform projectile;

    public bool shouldFollow = false;
    public bool getBackToTrebuchet = false;
    public bool getBackToHome = true;

    public float farLeft;
    public float farRight;
    public float farTop;
    public float farBottom;

    private Vector3 initPos;
    private Vector3 trebuchetPos;
	void Start () {
        shouldFollow = false;
        getBackToTrebuchet = false;
        getBackToHome = true;

        farBottom = transform.position.y;
        initPos = transform.position;
        trebuchetPos = GameObject.Find("TrebuchetPosition").transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(shouldFollow + "-------" + getBackToTrebuchet);
        if (shouldFollow && !getBackToTrebuchet && !getBackToHome) {
            Vector3 newPos = transform.position;

            if (projectile != null)
            {
                newPos.x = projectile.position.x;
                newPos.x = Mathf.Clamp(newPos.x, farLeft, farRight);
                newPos.y = projectile.position.y;
                newPos.y = Mathf.Clamp(newPos.y, farBottom, farTop);
            }
            else {
                shouldFollow = false;
                getBackToTrebuchet = true;
            }

            //transform.position = newPos;
            transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * 2f);
        }
        else if (!shouldFollow && getBackToTrebuchet && !getBackToHome)
        {
            Debug.Log(projectile);
            transform.position = Vector3.Lerp(transform.position, trebuchetPos, Time.deltaTime * 3f);
        }
        else if (!shouldFollow && !getBackToTrebuchet && getBackToHome)
        {
            //Debug.Log(projectile);
            transform.position = Vector3.Lerp(transform.position, initPos, Time.deltaTime * 3f);
        }
	}

    public void FindNewProjectile() {
        projectile = GameObject.FindGameObjectWithTag("Rock").transform;
        
    }
}
