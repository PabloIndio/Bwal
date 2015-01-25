using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {


	public static float velDesplazamiento = 20.0f;


	public static bool alguienEnAccion = false;


	private Vector2 target;
	private bool tieneLaBola=false;
    private Vector2 numCeldaInicial;//Número de celda en la que empieza en el campo
	private Vector2 numCelda;//Numero de celda del tablero en la que se encuentra
	private Vector2 screenPosition;//Cosas del GUI
	private bool encenderMenu;//Controlar en el GUI si hay que mostrar el menu del jugador


	//El modo es la accion que va a hacer el jugador(lanzar, placar..)
	public enum Modo{inactivo,desplazar,lanzar,placaje};
	private Modo modoActivo;

    private bool local = true;


	//Estadisticas individuales del jugador. Se leen de fichero
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
		target = transform.position;//A donde se va a mover
		this.modoActivo = Modo.inactivo;//Al inicializar empieza sin hacer nada
	
	}
	
	// Update is called once per frame
	void Update () {
	
		//Cada vez que se cambie el destino se mueve hasta el
		transform.position = Vector2.MoveTowards (transform.position,
		                                          target, 
		                                          velDesplazamiento * Time.deltaTime);

		//Variables para el menu en GUI
		screenPosition = Camera.main.WorldToScreenPoint(this.transform.position);
		screenPosition.y = Screen.height - screenPosition.y;
	}

    public void setEquipo(bool loc, Color color)
    {
        this.local = loc;
        this.gameObject.GetComponent<SpriteRenderer>().color = color;

    }

	public Modo getModo(){
		
		return this.modoActivo;
	}

	private void setModo(Modo modo){

		int dist=0;

		switch (modo) {
			
			case Modo.desplazar: dist = this.getVelocidad();
                alguienEnAccion = false;
				break;
			case Modo.placaje:   dist = 1;
				alguienEnAccion=true;
				break;
			case Modo.lanzar: dist= this.getPase();
				 alguienEnAccion=true;
				break;
			
		}

		//Al cambiar de modo se apagan las casillas del modo anterior y se encienden las nuevas
		TableroScript.Instance.apagarTodasCasillas ();
		TableroScript.Instance.encenderCasillasAdy (this.gameObject,dist);

		//Se pone el nuevo modo como el activo
		this.modoActivo = modo;
	
	}

	//Despues de seleccionar una casilla el jugador realiza la accion dependiendo del modo activo
	public bool accion(Vector2 numCel){

        bool accionCompletada = false;

		switch (modoActivo) {

			case Modo.desplazar: accionCompletada=desplazar(numCel);
				break;
			case Modo.lanzar: accionCompletada=lanzarBola(numCel);
				break;
			case Modo.placaje: accionCompletada=placar(numCel);
				break;
			case Modo.inactivo: Debug.Log("No quiero hacer nada");//Si esta inactivo no necesita la casilla de destino
				break;

		}

        return accionCompletada;


	}

	//Cambiar el destino al que se va a mover
	public bool desplazar(Vector2 numCel){

		//Hay que comprobar que la celda a la que se quiere desplazar este encendida
		if(TableroScript.Instance.estaEncendida(numCel)){
            TableroScript.Instance.quitarJugadorDeCelda(numCelda);
			numCelda = numCel;
			target = TableroScript.Instance.getPosCelda(numCel);//Cambia el destino
            TableroScript.Instance.posicionarJudador(this.GetComponent<PlayerScript>());
            return true;
        }
        else
        {
            Debug.Log("Esa casilla esta muy lejos");
            return false;

        }
			
	}


	public bool lanzarBola(Vector2 numCel){

        if (tieneLaBola)
        {
            //Si es posible desplazar la bola..
            if (GameControllerScript.Instance.desplazarBola(numCel))
            {
                alguienEnAccion = false;
                tieneLaBola = false;
                return true;
            }
        }
        else
            Debug.Log("No tengo la bola!");

        return false;

	}

	public bool placar(Vector2 numCel){

		PlayerScript p =TableroScript.Instance.getPlayerCelda (numCel);
		if (p != null) {
				p.quitarVida (this.golpe);
                if (p.tieneLaBola)
                    p.soltarBola();
				alguienEnAccion = false;
                return true;
		} else {
			Debug.Log("No le llego");
            return false;
		}
        
	}



	public void cogerBola(){

		TableroScript.Instance.apagarTodasCasillas ();

		Vector2 numCeldaBola =GameControllerScript.Instance.bola.GetComponent<BolaScript> ().getNumCelda ();

		//La bola tiene que estar a una distancia de 1 o en la posicion del jugador
		if (numCeldaBola.x <= numCelda.x + 1 && numCeldaBola.x >= numCelda.x - 1 
						&& numCeldaBola.y <= numCelda.y + 1 && numCeldaBola.y >= numCelda.y - 1) {
						//Si el GameController le deja cojerla (si no la tiene otro)
						if(GameControllerScript.Instance.bola.GetComponent<BolaScript> ().setPoseedor (this.gameObject)){
							tieneLaBola = true;
                            alguienEnAccion = false;
                            GameControllerScript.Instance.accionTerminada();
						}else 
							Debug.Log("La tiene otro!");

		} else
				Debug.Log ("Ta muy lejos!");

        
	}

    public void soltarBola()
    {
        GameControllerScript.Instance.desplazarBola(numCelda);
        tieneLaBola = false;

    }



	//El GameController le comunica al jugador que esta seleccionado para que encienda el menu
	public void seleccionado(){

		encenderMenu = true;

	}

	//El GameController le comunica al jugador que ya no esta seleccionado y este apaga las casillas y el menu 
	//y pasa a modo inactivo
	public void desseleccionado(){
        TableroScript.Instance.apagarTodasCasillas();
		encenderMenu = false;
		this.setModo(Modo.inactivo);
	}



	void OnMouseDown(){
        
        GameControllerScript.Instance.seleccionarJugador (this.gameObject.GetComponent<PlayerScript>(),local);

	}

    public void setNumCeldaInicial(Vector2 nCelda)
    {

        numCeldaInicial = nCelda;
    }

    public void volverAPosInicial()
    {
        this.setNumCelda(numCeldaInicial);
        target = TableroScript.Instance.getPosCelda(numCeldaInicial);
        this.transform.position = TableroScript.Instance.getPosCelda(numCeldaInicial);
    }

	public Vector2 getNumCelda(){
		return numCelda;
	}

	public void setNumCelda(Vector2 celda){
        TableroScript.Instance.quitarJugadorDeCelda(numCelda);
		numCelda = celda;
        TableroScript.Instance.posicionarJudador(this.GetComponent<PlayerScript>());
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



	void OnGUI(){
		if (encenderMenu) {
				if(GUI.Button (new Rect (screenPosition.x - 20, screenPosition.y - 40, 20, 20), "C"))
					cogerBola();

				if(GUI.Button (new Rect (screenPosition.x, screenPosition.y - 40, 20, 20), "D"))
				 	setModo(Modo.desplazar);

				if(GUI.Button (new Rect (screenPosition.x+20, screenPosition.y - 40, 20, 20), "P"))
					setModo(Modo.placaje);

				if(tieneLaBola)//Solo aparece la opcion de lanzar si el jugador tiene la bola
					if(GUI.Button (new Rect (screenPosition.x + 40, screenPosition.y - 40, 20, 20), "L"))
								setModo(Modo.lanzar);
				


		}
	}

}

	



