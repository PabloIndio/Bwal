using UnityEngine;
using System.Collections;

public class CeldaScript : MonoBehaviour {


	private Vector2 numCelda;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	
	}



	public void encenderAmarillo(){
		this.GetComponent<SpriteRenderer> ().color = Color.yellow;
	}
	public void encenderRojo(){
		this.GetComponent<SpriteRenderer> ().color = Color.red;
	}
	public void apagar(){
		this.GetComponent<SpriteRenderer> ().color = Color.white;
	}

	public void setNumCelda(Vector2 celda){
				numCelda = celda;
	}
	public Vector2 getNumCelda(){
				return numCelda;
		}


}
