using UnityEngine;
using System.Collections;

public class BolaScript : MonoBehaviour {

	private Vector2 target;
	public float speed = 20.0f;

	// Use this for initialization
	void Start () {

		target = transform.position;
	
	}
	
	// Update is called once per frame
	void Update () {


		transform.position = Vector2.MoveTowards (transform.position,
		                                          target, 
		                                          speed * Time.deltaTime);
	
	}


	public void desplazar(Vector2 destino){

		target = destino;
		

		
	}
}
