using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour {

    public enum TrebuchetState { Single, Multi};

    public GameObject singleTrebuchet;
    public GameObject multiTrebuchet;
    public TrebuchetState currentTrebuchetState = TrebuchetState.Single;
    
    public int numberOfCastleMember = 0;
    public bool isPause = false;

    private int tapCounter = 0;
    private GameObject currentTrebuchet;
    private ScoreManager scoreManager;

    private ProjectileFollow cameraFollow;
    private Animator pauseMenuAnimator;
    private Animator hudFadeAnimator;
	void Start () {
        numberOfCastleMember = GameObject.FindGameObjectsWithTag("CastleMember").Length;

        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        pauseMenuAnimator = GameObject.Find("PauseMenuPanel").GetComponent<Animator>();
        hudFadeAnimator = GameObject.Find("HUD").GetComponent<Animator>();

        cameraFollow = Camera.main.GetComponent<ProjectileFollow>();
        InstantiateTrebuchet();
        SetProjectileOnCamera();

        isPause = false;

        cameraFollow.shouldFollow = false;
        cameraFollow.getBackToHome = true;
        cameraFollow.getBackToTrebuchet = false;

        scoreManager.totalKill = numberOfCastleMember;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
            PauseMenu();
	}

    public void RestartLevel() {
        Time.timeScale = 1f;
        Application.LoadLevel(Application.loadedLevel);
    }
    
    public void LoadMenu() {
        Time.timeScale = 1f;
        Application.LoadLevel(0);
    }

    public void QuitApplication() {
        Application.Quit();
    }
    void InstantiateTrebuchet() {
        if (currentTrebuchetState == TrebuchetState.Single)
        {
            currentTrebuchet = Instantiate(singleTrebuchet, singleTrebuchet.transform.position, singleTrebuchet.transform.rotation) as GameObject;
        }
        else if (currentTrebuchetState == TrebuchetState.Multi)
        {
            currentTrebuchet = Instantiate(multiTrebuchet, multiTrebuchet.transform.position, multiTrebuchet.transform.rotation) as GameObject;
        }
    }

    void SetProjectileOnCamera() {
        cameraFollow.shouldFollow = true;
        cameraFollow.getBackToTrebuchet = false;
        cameraFollow.getBackToHome = false;
        cameraFollow.FindNewProjectile();
    }

    void DestroyTrebuchet() {
        cameraFollow.shouldFollow = false;
        cameraFollow.getBackToTrebuchet = true;
        cameraFollow.getBackToHome = false;

        Destroy(currentTrebuchet);
    }

    public void InputProcessing() {
        if (!isPause)
        {
            tapCounter++;
            if (tapCounter <= 3)
            {
                if (tapCounter == 1)
                {
                    cameraFollow.shouldFollow = false;
                    cameraFollow.getBackToTrebuchet = true;
                    cameraFollow.getBackToHome = false;
                }
                else if (tapCounter == 2 || tapCounter == 3)
                {
                    cameraFollow.shouldFollow = true;
                    cameraFollow.getBackToTrebuchet = false;
                    cameraFollow.getBackToHome = false;
                    if (currentTrebuchetState == TrebuchetState.Single)
                    {
                        currentTrebuchet.GetComponent<TrebuchetSingleStrikeHandler>().TrebuchetAction();
                    }
                    else if (currentTrebuchetState == TrebuchetState.Multi)
                    {
                        currentTrebuchet.GetComponent<TrebuchetMultiStrikeHandler>().TrebuchetAction(); ;
                    }
                }

            }
            else
            {
                tapCounter = 0;
                DestroyTrebuchet();
                InstantiateTrebuchet();
                SetProjectileOnCamera();
            }
        }
        else {
            PauseMenu();
        }
    }

    public void PauseMenu() {
        if (isPause)
        {
            Time.timeScale = 1f;
            pauseMenuAnimator.SetBool("open", false);
            hudFadeAnimator.SetBool("fade", false);
            isPause = false;
        }
        else {
            Time.timeScale = 0f;
            pauseMenuAnimator.SetBool("open", true);
            hudFadeAnimator.SetBool("fade", true);
            isPause = true;
        }
    }
}
