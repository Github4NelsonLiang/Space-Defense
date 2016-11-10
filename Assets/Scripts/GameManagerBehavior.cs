using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManagerBehavior : MonoBehaviour {
    private bool paused = false;
    public Text button;
    private GameData dataObject;
    public Text goldLabel;
	private int gold;
	public int Gold {
  		get { return gold; }
  		set {
			gold = value;
    		goldLabel.GetComponent<Text>().text = "GOLD: " + gold;
		}
	}

	public Text waveLabel;
	public GameObject[] nextWaveLabels;

	public bool gameOver = false;

	private int wave;
	public int Wave {
		get { return wave; }
		set {
			wave = value;
			if (!gameOver) {
				for (int i = 0; i < nextWaveLabels.Length; i++) {
					nextWaveLabels[i].GetComponent<Animator>().SetTrigger("nextWave");
				}
			}
			waveLabel.text = "WAVE: " + (wave + 1);
		}
	}

	public Text healthLabel;
	public GameObject[] healthIndicator;

	private int health;
	public int Health {
		get { return health; }
		set {
			// 1
			if (value < health) {
				Camera.main.GetComponent<CameraShake>().Shake();
			}
			// 2
			health = value;
			healthLabel.text = "HEALTH: " + health;
			// 2
			if (health <= 0 && !gameOver) {
				gameOver = true;
				GameObject gameOverText = GameObject.FindGameObjectWithTag ("GameWon");
				gameOverText.GetComponent<Animator>().SetBool("gameOver", true);
                GameOver();
            }
			// 3 
			for (int i = 0; i < healthIndicator.Length; i++) {
				if (i < Health) {
					healthIndicator[i].SetActive(true);
				} else {
					healthIndicator[i].SetActive(false);
				}
			}
		}
	}

	// Use this for initialization
	void Start () {
		Gold = 500;
		Wave = 0;
		Health = 5;
        dataObject = new GameData();
    }
	
	// Update is called once per frame
	void Update () {
        if (paused)
        {
            Time.timeScale = 0.0F;
        }
        else
        {
            Time.timeScale = 1.0F;
        }
	}

    void GameOver()
    {
        dataObject.updateData(Wave);
        GameControl.Instance.currentWave = Wave;
        GameControl.Instance.updateSavedScores(Wave);
        SceneManager.LoadScene("GameOver");

    }

    public void pause()
    {
        if (button.text.Equals("Pause"))
        {
            button.text = "Continue";
            paused = true;
        }
        else
        {
            button.text = "Pause";
            paused = false;
        }

    }

    public void quit()
    {
        Application.Quit();
    }
}
