using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	private Vector2 target;
	public static float velDesplazamiento = 20.0f;
	private bool tieneLaBola=false;
	private Vector2 numCelda;
	private Vector2 screenPosition;
	private bool encenderMenu;
	


	//Estadisticas individuales del jugador
	private string nombre;
	private int vidaTotal=10;
	private int vida;
	private int velocidad;
	private int fuerza;
	private int lpase;
	private int recepcion;
	private int dribbling;
	private int agilidadDefensa;
	private int intercepcion;
	private int golpe;
	private int probGolpe;

	

	// Use this for initialization

	void Start () {
		target = transform.position;
	
	}
	
	// Update is called once per frame
	void Update () {
	
		transform.position = Vector2.MoveTowards (transform.position,
		                                          target, 
		                                          velDesplazamiento * Time.deltaTime);

		screenPosition = Camera.main.WorldToScreenPoint(this.transform.position);
		screenPosition.y = Screen.height - screenPosition.y;
	}

	public void desplazar(Vector2 destino, Vector2 celda){

		apagarMenu ();
		numCelda = celda;
		target = destino;

	}


	public void quitarVida(int daño){
		this.vida -= daño;
		if (vida <= 0)
						Debug.Log ("2 turnos de espera");

	}

	public void recuperarVida(int cantidad){
		if (this.vida + cantidad >= vidaTotal)
						vida = vidaTotal;
				else
						vida += cantidad;

	}

	public void cogerBola(){
		Vector2 numCeldaBola =GameControllerScript.Instance.bola.GetComponent<BolaScript> ().getNumCelda ();

		//La bola tiene que estar a una distancia de 1 o en la posicion del jugador
		if (numCeldaBola.x <= numCelda.x+1 && numCeldaBola.y <= numCelda.y+1) {
				GameControllerScript.Instance.bola.GetComponent<BolaScript> ().setPoseedor (this.gameObject);
				tieneLaBola = true;

		}

	}


	public void lanzarBola(Vector2 destino){

		if (tieneLaBola) {

			GameControllerScript.Instance.bola.GetComponent<BolaScript>().desplazar(destino,numCelda);
			tieneLaBola=false;
		}


	}

	public void apagarMenu(){
		encenderMenu = false;
	}


	//Inicializar las estadisticas del jugador
	public void setParametros(string nombre,int vidaTotal,int velocidad, int fuerza,int pase,
	                          int recepcion, int dribbling, int agilidadDefensa,
	                          int intercepcion, int golpe, int probGolpe){


		this.nombre = nombre;
		this.vidaTotal = vidaTotal;
		this.vida = vidaTotal;
		this.velocidad = velocidad;
		this.fuerza = fuerza;
		this.lpase = pase;
		this.recepcion = recepcion;
		this.dribbling = dribbling;
		this.agilidadDefensa = agilidadDefensa;
		this.intercepcion = intercepcion;
		this.golpe = golpe;
		this.probGolpe = probGolpe;


	}

	void OnMouseDown(){

				GameControllerScript.Instance.seleccionarJugador (this.gameObject);
				encenderMenu = true;

	}

	public Vector2 getNumCelda(){
		return numCelda;
	}

	public void setNumCelda(Vector2 celda){
		numCelda = celda;
	}

	//Getters de estadisticas de jugador
	public string getNombre(){return this.nombre;}
	public int getVidaTotal(){return this.vidaTotal;}
	public int getVida(){return this.vida;}
	public int getVelocidad(){return this.velocidad;}
	public int getFuerza(){return this.fuerza;}
	public int getPase(){return this.lpase;}
	public int getRecepcion(){return this.recepcion;}
	public int getDribbling(){return this.dribbling;}
	public int getAgilidadDefensa(){return this.agilidadDefensa;}
	public int getIntercepcion(){return this.intercepcion;}
	public int getGolpe(){return this.golpe;}
	public int getProbGolpe(){return this.probGolpe;}



	void OnGUI(){
		if (encenderMenu) {
				if(GUI.Button (new Rect (screenPosition.x - 20, screenPosition.y - 40, 20, 20), "A"))
					cogerBola();
				if(GUI.Button (new Rect (screenPosition.x, screenPosition.y - 40, 20, 20), "B"))
					lanzarBola (new Vector2(0,0));
				GUI.Button (new Rect (screenPosition.x + 20, screenPosition.y - 40, 20, 20), "C");
		}
	}

}

	



