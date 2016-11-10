using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UICanvasController : MonoBehaviour {
    public Text current;
    public Text highest;

	// Use this for initialization
	void Start () {

        current.text = "Current High Score: " + GameControl.Instance.currentWave;
        highest.text = "Highest Score: " + GameControl.Instance.highScores[0];

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
