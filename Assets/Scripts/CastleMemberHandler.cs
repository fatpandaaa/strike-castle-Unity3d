using UnityEngine;
using System.Collections;

public class CastleMemberHandler : MonoBehaviour {

    private ScoreManager scoreManager;
    private bool active = false;
    private float timer = 0f;
	void Start () {
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        timer = 0f;
        active = false;
    }

    void Update() {
        if (!active && timer < 1f) {
            timer += Time.deltaTime;
        }
        else if(!active && timer >= 1f){
            active = true;
            timer = 0f;
        }
    }
	
	// Update is called once per frame
    void OnCollisionEnter2D(Collision2D other) {
        if ((other.gameObject.tag == "RockReleased" || other.gameObject.tag == "Castle" || other.gameObject.tag == "CastleMember") && active)
        {
            if (active) {
                scoreManager.killCount++;
                active = false;
                this.gameObject.GetComponent<CastleMemberHandler>().enabled = false;
            }
        }
    }
}
