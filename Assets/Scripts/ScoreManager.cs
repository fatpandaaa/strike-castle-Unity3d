using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour {
    public int shotCount = 0;
    public int killCount = 0;
    public int totalKill = 0;

    private SceneManager sceneManager;
    private Text shotsText;
    private Text goldText;
    private Text silverText;
    private Text bronzeText;
    private Text killText;
    private Animator winGameOverAnim;
    private Animator looseGameOverAnim;

    private int gold = 1;
    private int silver = 3;
    private int bronze = 5;
	void Start () {
        sceneManager = GameObject.Find("SceneManager").GetComponent<SceneManager>();
        winGameOverAnim = GameObject.Find("WinGameOverUI").GetComponent<Animator>();
        looseGameOverAnim = GameObject.Find("LooseGameOverUI").GetComponent<Animator>();

        shotsText = GameObject.Find("ShotsText").GetComponent<Text>();
        goldText = GameObject.Find("GoldText").GetComponent<Text>();
        silverText = GameObject.Find("SilverText").GetComponent<Text>();
        bronzeText = GameObject.Find("BronzeText").GetComponent<Text>();
        killText = GameObject.Find("KillText").GetComponent<Text>();

        shotCount = 0;
        killCount = 0;
        totalKill = sceneManager.numberOfCastleMember;
        
        goldText.text = gold.ToString();
        silverText.text = silver.ToString();
        bronzeText.text = bronze.ToString();

        winGameOverAnim.SetBool("fade", true);
        looseGameOverAnim.SetBool("fade", true);
    }
	
	// Update is called once per frame
	void Update () {
        shotsText.text = shotCount.ToString() + " SHOTS";
        killText.text = killCount.ToString() + "/" + totalKill.ToString() + " KILLED";

        if (killCount == totalKill) {
            if (shotCount > bronze)
            {
                sceneManager.isPause = true;
                looseGameOverAnim.SetBool("fade", false);
            }
            else {
                sceneManager.isPause = true;
                winGameOverAnim.SetBool("fade", false);
            }
        }
	}
}
