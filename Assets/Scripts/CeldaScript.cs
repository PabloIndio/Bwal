using UnityEngine;
using System.Collections;

public class CeldaScript : MonoBehaviour {


	private Vector2 numCelda;
	private bool encendida = false;
	private PlayerScript player=null;//Jugador que se encuentra encima de la celda

    private bool isMeta = false;
    private bool local = true;

    private static bool puedeAnotar = true;

	// Use this for initialization
	void Start () {

     
	}
	
	// Update is called once per frame
	void Update () {

	
	}


	//Cuando alguien hace click en la casilla esta comunica al tablero que ha sido seleccionada
	void OnMouseDown(){

		TableroScript.Instance.setCeldaDestino(this.numCelda);
	}


	public void encenderAmarillo(){
		encendida = true;
		this.GetComponent<SpriteRenderer> ().color = Color.yellow;
	}
	public void encenderRojo(){
		this.GetComponent<SpriteRenderer> ().color = Color.red;
	}

	public void apagar(){
		encendida = false;
        if (!isMeta)
            this.GetComponent<SpriteRenderer>().color = Color.white;
        else
            encenderRojo();
	}

    public void setMeta(int i)
    {
        isMeta =true;
        if (i == 0)
            local = false;
        this.GetComponent<BoxCollider2D>().isTrigger=true;
        encenderRojo();
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

	public bool estaEncendida(){

		return encendida;
	}

	public PlayerScript getPlayer(){
		return player;
	}

	public void setPlayer(PlayerScript jugador){

		this.player = jugador;
	}

    void OnTriggerEnter2D(Collider2D col){
        if (col.gameObject.tag == "Bola" && puedeAnotar){
            puedeAnotar = false;
            GameControllerScript.Instance.anotarPunto(local);
            StartCoroutine(esperaAnotacion());
        }
    }

    IEnumerator esperaAnotacion()
    {
        yield return new WaitForSeconds(0.2f);
        puedeAnotar = true;
    }
}
