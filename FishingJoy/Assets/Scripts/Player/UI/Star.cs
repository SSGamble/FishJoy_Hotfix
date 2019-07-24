using UnityEngine;
using System.Collections;

public class Star : MonoBehaviour {

	public float moveSpeed=1;

	// Use this for initialization
	void Start () {
		Destroy(gameObject,Random.Range(0.4f,1));
	}
	
	// Update is called once per frame
	void Update () {
		//transform.Translate(-transform.right*moveSpeed*Time.deltaTime,Space.World);
	}
}
