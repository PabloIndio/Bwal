using UnityEngine;
using System.Collections;

public class CeldaScript : MonoBehaviour {


	private Vector2 numCelda;
	private bool encendida = false;
    private Color colorOn;

    private Color colorAlpha;
    private float countAlpha = 1.0f;
    private bool apagando = true;


	private PlayerScript player=null;//Jugador que se encuentra encima de la celda

    private bool isMeta = false;
    private bool local = true;

    private static bool puedeAnotar = true;

	// Use this for initialization
	void Start () {

        colorOn = this.GetComponent<SpriteRenderer>().color;
        colorAlpha = Color.yellow;
     
	}
	
	// Update is called once per frame
	void Update () {
        if (encendida)
        {
            parpadeo();
        }

	}


	//Cuando alguien hace click en la casilla esta comunica al tablero que ha sido seleccionada
	void OnMouseDown(){

		TableroScript.Instance.setCeldaDestino(this.numCelda);
	}


	public void encenderCelda(){
		encendida = true;
		this.GetComponent<SpriteRenderer> ().color = Color.yellow;
	}
	public void encenderRojo(){
		this.GetComponent<SpriteRenderer> ().color = Color.red;
	}

	public void apagar(){
		encendida = false;
        if (!isMeta)
            this.GetComponent<SpriteRenderer>().color = colorOn;
        else
            encenderRojo();

        //Hay que reiniciar estas dos variables para el parpadeo
        apagando = true;
        countAlpha = 1.0f;
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


    private void parpadeo()
    {
        if (countAlpha > 0.5f && apagando)
        {
            apagando = true;
            countAlpha -= 0.009f;
        }
        else if (countAlpha < 1.0f)
        {
            countAlpha += 0.009f;
            apagando = false;
        }else
        {
            apagando = true;
        }

        colorAlpha.a = countAlpha;
        this.GetComponent<SpriteRenderer>().color = colorAlpha;
    }

}
