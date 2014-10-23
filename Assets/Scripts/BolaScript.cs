using UnityEngine;
using System.Collections;

public class BolaScript : MonoBehaviour {

	private Vector3 target;
	public float speed = 50f;
	private Vector2 numCelda;
	private GameObject jugadorConLaBola;

	// Use this for initialization
	void Start () {

		target = transform.position;
	
	}
	
	// Update is called once per frame
	void Update () {
		if (jugadorConLaBola != null)
						target = new Vector3(jugadorConLaBola.transform.position.x+0.4f,
			                     jugadorConLaBola.transform.position.y,transform.position.z);


		transform.position = Vector3.MoveTowards (transform.position,
	                                          target, 
	                                          speed * Time.deltaTime);


	}


	public void desplazar(Vector2 destino,Vector2 celda){
		jugadorConLaBola = null;
		numCelda = celda;
		target = new Vector3(destino.x,destino.y,transform.position.z);
	}
	public Vector2 getNumCelda(){
		return numCelda;
	}
	
	public void setNumCelda(Vector2 celda){
		numCelda = celda;
	}
	public void setPoseedor(GameObject jugador){
		jugadorConLaBola = jugador;

	}
}
