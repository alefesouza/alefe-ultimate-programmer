using UnityEngine;
using System.Collections;

public class CompleteCameraController : MonoBehaviour {

	public GameObject player;       //Public variable to store a reference to the player game object
    public GameObject player2;       //Public variable to store a reference to the player game object

	// Use this for initialization
	void Start () 
	{
	}

    // LateUpdate is called after Update each frame
    void LateUpdate()
    {
        float min = Mathf.Min(player.transform.position.x, player2.transform.position.x) / 2;
        float size = (player.transform.position.x - player2.transform.position.x) / 2;
        Vector3 position;
        bool player1left = player.transform.position.x < player2.transform.position.x;

        float cameraDistance = -(player.transform.position.x - player2.transform.position.x) / 2;

        if (cameraDistance > 22)
        {
            size = -size;
            position = new Vector3(min, 19, -10);
        }
        else if(cameraDistance < -10)
        {
            position = new Vector3(player.transform.position.x - player2.transform.position.x - 50, 10, -10);
        }
        else
        {
            size = 16.5F;
            position = new Vector3(0, 12.5F, -10);
        }

        GetComponent<Camera>().orthographicSize = size;
        transform.position = position;
    }
}
