using UnityEngine;
using System.Collections;

public class CeldaScript : MonoBehaviour {


	private Vector2 numCelda;
	private bool encendida = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	
	}

	void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.GetComponent<PlayerScript> () != null)
						encenderAmarillo ();

	}

	void OnMouseDown(){

		if (encendida) {
			GameControllerScript.Instance.desplazamiento(this.numCelda);
		}

	}

	public void encenderAmarillo(){
		encendida = true;
		this.GetComponent<SpriteRenderer> ().color = Color.yellow;
	}
	public void encenderRojo(){
		encendida = true;
		this.GetComponent<SpriteRenderer> ().color = Color.red;
	}
	public void apagar(){
		encendida = false;
		this.GetComponent<SpriteRenderer> ().color = Color.white;
	}

	public void setNumCelda(Vector2 celda){
				numCelda = celda;
	}
	public Vector2 getNumCelda(){
				return numCelda;
	}

	public Vector2 getPos(){
		return this.transform.position;
	}


}
