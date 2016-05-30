using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CanvasElements : MonoBehaviour {
    public Text time;
    int timeRemaining = 99;

	// Use this for initialization
	void Start () {
        InvokeRepeating("DecreaseTime", 1, 1);
    }

    void DecreaseTime()
    {
        timeRemaining--;
        time.text = timeRemaining.ToString();
    }

	// Update is called once per frame
	void Update () {
	}
}
