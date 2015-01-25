using UnityEngine;
using System.Collections;

public class BolaScript : MonoBehaviour {

	//Destino al que se mueve la bola y velocidad
	private Vector3 target;
	public float speed = 50f;

	//Numero de celda en la que se encuentra
	private Vector2 numCelda;
	private GameObject jugadorConLaBola;

	// Use this for initialization
	void Start () {

		target = transform.position;

	}
	
	// Update is called once per frame
	void Update () {
		//Si hay un jugador que tiene la bola tiene que seguirlo
		if (jugadorConLaBola != null)
						target = new Vector3(jugadorConLaBola.transform.position.x+0.4f,
			                     jugadorConLaBola.transform.position.y,transform.position.z);

		transform.position = Vector3.MoveTowards (transform.position,
	                                          target, 
	                                          speed * Time.deltaTime);

	}

	//Cuando alguien lanza la bola esta ya no tiene poseedor
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

	//El jugador pide permiso para conseguir la bola
	public bool setPoseedor(GameObject jugador){
		if (jugadorConLaBola == null) {
				jugadorConLaBola = jugador;
				numCelda = jugadorConLaBola.GetComponent<PlayerScript>().getNumCelda();
				return true;
		} else {
				return false;
		}

	}
}
