using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBubbleLauncher : MonoBehaviour {

    [SerializeField] TimeBubbleControl timeBubblePrefab;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.U))
        {
            TimeBubbleControl newTimeBubble = Instantiate(timeBubblePrefab,transform.position,transform.rotation);
            newTimeBubble.InitializeBubble(gameObject);
        }
	}
}
