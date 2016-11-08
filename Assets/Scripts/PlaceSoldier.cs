using UnityEngine;
using System.Collections;

public class PlaceSoldier : MonoBehaviour {

	public GameObject soldierPrefab;
	private GameObject soldier;
	private GameManagerBehavior gameManager;

	// Use this for initialization
	void Start () {
		gameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehavior>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	private bool canPlaceSoldier() {
		int cost = soldierPrefab.GetComponent<SoldierData> ().levels[0].cost;
		return soldier == null && gameManager.Gold >= cost;
	}
	
	//1
	void OnMouseUp () {
  		//2
		if (canPlaceSoldier ()) {
	    	//3
		    soldier = (GameObject) Instantiate(soldierPrefab, transform.position, Quaternion.identity);
		    //4
    		AudioSource audioSource = gameObject.GetComponent<AudioSource>();
			audioSource.PlayOneShot(audioSource.clip);
 
			gameManager.Gold -= soldier.GetComponent<SoldierData>().CurrentLevel.cost;
		} else if (canUpgradeSoldier()) {
			soldier.GetComponent<SoldierData>().increaseLevel();
			AudioSource audioSource = gameObject.GetComponent<AudioSource>();
			audioSource.PlayOneShot(audioSource.clip);

			gameManager.Gold -= soldier.GetComponent<SoldierData>().CurrentLevel.cost;
		}
	}

	private bool canUpgradeSoldier() {
		if (soldier != null) {
			SoldierData soldierData = soldier.GetComponent<SoldierData> ();
			SoldierLevel nextLevel = soldierData.getNextLevel();
			if (nextLevel != null) {
				return gameManager.Gold >= nextLevel.cost;
 			}
  		}
		return false;
	}
}
