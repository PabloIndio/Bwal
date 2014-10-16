using UnityEngine;
using System.Collections;

public class BolaScript : MonoBehaviour {

	private Vector2 target;
	public float speed = 20.0f;
	private Vector2 numCelda;

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


	public void desplazar(Vector2 destino,Vector2 celda){
		numCelda = celda;
		target = destino;
	}
	public Vector2 getNumCelda(){
		return numCelda;
	}
	
	public void setNumCelda(Vector2 celda){
		numCelda = celda;
	}
}
