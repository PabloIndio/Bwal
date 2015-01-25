using UnityEngine;
using System.Collections;
using System.IO;



public class GameControllerScript : MonoBehaviour {

	private static int numJugadoresLocales = 5;
    private static int numJugadoresVisitantes = 5;
    private static string EQUIPO_LOCAL_FILE = "equipoLocal.txt";
    private static string EQUIPO_VISITANTE_FILE = "equipoVisitante.txt";
    private static int turnosPorEquipo = 4;

    private int turnosActuales;
    private bool turnoLocal=true;


	private int golesL=0;
	private int golesV=0;


	public static GameControllerScript Instance;
	private PlayerScript pSeleccionado;


	//Bola
	public GameObject bola;

	//Jugadores Locales
	private GameObject[] locales= new GameObject[numJugadoresLocales];
    private GameObject[] visitantes = new GameObject[numJugadoresVisitantes];

	//Jugadores Visitantes



	void Awake(){

		if (Instance != null)
						Debug.LogError ("No deberia de haber un GameController");
				else
						Instance = this;

	}


	// Use this for initialization
	void Start () {

		TableroScript.Instance.inicializarTablero ();

        //Leer e inicializar equipo local
		leerFicheroJugadores (EQUIPO_LOCAL_FILE,numJugadoresLocales,locales,true);

        //Leer e inicializar equipo visitante
        leerFicheroJugadores(EQUIPO_VISITANTE_FILE, numJugadoresVisitantes, visitantes,false);

		inicializarBola ();

        turnosActuales = turnosPorEquipo;
	}
	
	// Update is called once per frame
	void Update () {
	
	
	}

	private void inicializarBola(){

		//Crear la bola
		bola = (GameObject)Instantiate (Resources.Load ("Bola"), Vector3.zero, Quaternion.identity);
		
		bola.GetComponent<BolaScript> ().setNumCelda(TableroScript.Instance.getCentroTablero());
		
		bola.transform.position = TableroScript.Instance.getPosCelda(TableroScript.Instance.getCentroTablero());
		bola.transform.position = new Vector3 (bola.transform.position.x, bola.transform.position.y, -0.5f);

	}



	private void leerFicheroJugadores(string nombreArchivo, int numJugadores, GameObject[] array, bool local){

		
		//Leer de fichero las estadisticas de los jugadores
		StreamReader sr = new StreamReader(Application.dataPath+"/Resources/"+nombreArchivo);
		string fileContents = sr.ReadToEnd();
		sr.Close();
		
		string[] lines = fileContents.Split ("\n"[0]);

        //La primera línea es para las posiciones de los jugadores en el campo
        string[] aux = lines[0].Split(" "[0]);
        Vector2[] posiciones = new Vector2[aux.Length];
        for (int i = 0; i < aux.Length; i++)
		{
            string[] aux2 = aux[i].Split(","[0]);
            posiciones[i].x = float.Parse(aux2[0]);
            posiciones[i].y = float.Parse(aux2[1]);

		}

		//Crear jugadores 
		for(int i = 0; i<numJugadores;i++) {

            string[] parametros = lines[i + 1].Split(","[0]);
			
			array[i]=inicializarJugador(array[i],
                parametros,
                posiciones[i],
                local);
		}

	}


	private GameObject inicializarJugador(GameObject p, string[] par,Vector2 numCelda, bool local){

		p = (GameObject)Instantiate (Resources.Load ("Player"), Vector3.zero, Quaternion.identity);
		p.GetComponent<PlayerScript> ().setParametros (par[0],int.Parse(par[1]), int.Parse(par[2]), int.Parse(par[3]), 
		                                               int.Parse(par[4]), int.Parse(par[5]),int.Parse(par[6]), int.Parse(par[7])
		                                               , int.Parse(par[8]), int.Parse(par[9]),int.Parse(par[10]));

        p.GetComponent<PlayerScript>().setNumCeldaInicial(numCelda);
		p.GetComponent<PlayerScript> ().setNumCelda(numCelda);

        Color color =Color.white;
        if (local)
            color = Color.white;
        else
            color = Color.blue;
        
        p.GetComponent<PlayerScript>().setEquipo(local, color);
		p.transform.position =TableroScript.Instance.getPosCelda(numCelda);

        return p;
	}
	

	public int getGolesL(){
		return golesL;
	}

	public int getGolesV(){

		return golesV;
	}

	public void golLocal(){

		golesL += 1;
	}
	public void golVisitante(){
		
		golesV += 1;
	}

	//Lo llama el jugador cuando se hace click en él
	public void seleccionarJugador(PlayerScript player, bool local){


        if (!PlayerScript.alguienEnAccion && local==turnoLocal)
        {
            if(pSeleccionado!=null)
                pSeleccionado.desseleccionado();
            pSeleccionado = player;
            pSeleccionado.seleccionado();

        }
        else
        {
            if (pSeleccionado!=null && pSeleccionado.accion(player.getNumCelda()))//Si realiza la acción y la termina..
            {
                accionTerminada();
            }
        }


	}
	
	//Lo llama el tablero cuando se selecciona una celda para avisar al jugador actual a que realice la accion
	public void accionJugador(Vector2 numCel){

        if (this.pSeleccionado != null)
            if (pSeleccionado.accion(numCel))
                accionTerminada();

	}

    public void anotarPunto(bool local)
    {
        if (local)
        {
            golesL += 1;
            turnoLocal = false;
        }
        else
        {
            golesV += 1;
            turnoLocal = true;
        }
        turnosActuales = turnosPorEquipo;
        StartCoroutine(saqueDeCentro());
        
    }

    IEnumerator saqueDeCentro()
    {
        Vector2 centro = TableroScript.Instance.getCentroTablero();
        yield return new WaitForSeconds(2.0f);
        foreach (GameObject p in locales)
        {
            p.GetComponent<PlayerScript>().volverAPosInicial();
        }
        foreach (GameObject p in visitantes)
        {
            p.GetComponent<PlayerScript>().volverAPosInicial();
        }
        this.bola.GetComponent<BolaScript>().desplazar(TableroScript.Instance.getPosCelda(centro),
            centro);
    }



	public bool desplazarBola(Vector2 numCel){

		if (TableroScript.Instance.estaEncendida (numCel)) {
						TableroScript.Instance.apagarTodasCasillas ();
						this.bola.GetComponent<BolaScript> ().desplazar (TableroScript.Instance.getPosCelda (numCel), numCel);
						pSeleccionado.GetComponent<PlayerScript>().desseleccionado();
						return true;
		} else {
				Debug.Log ("No puede desplazar tan lejos");
				return false;
		}
	}


    public void accionTerminada()
    {
        pSeleccionado.desseleccionado();
        pSeleccionado = null;
        gastarTurno();
    }

    public void gastarTurno()
    {
        turnosActuales -= 1;
        if (turnosActuales == 0)
        {
            turnoLocal = !turnoLocal;
            turnosActuales = turnosPorEquipo;
        }

    }

  

	void OnGUI(){

		//Mostrar las estadisticas del jugador seleccionado en pantalla
		if (pSeleccionado != null) {
			
			GUI.Label (new Rect (200, 100, 400, 20), pSeleccionado.GetComponent<PlayerScript>().getNombre()+
			           "   Vida: "+pSeleccionado.GetComponent<PlayerScript>().getVida().ToString()+"/"+
			           pSeleccionado.GetComponent<PlayerScript>().getVidaTotal().ToString()+
			           "   Vel: "+pSeleccionado.GetComponent<PlayerScript>().getVelocidad().ToString());

		}

        GUI.Label (new Rect(0,0,400,20), "Local "+golesL+"-"+golesV+" Visitante");

        if(turnoLocal)
            GUI.Label(new Rect(200, 0, 400, 20), "Turno LOCAL turnos Restantes " + turnosActuales);
        else
            GUI.Label(new Rect(200, 0, 400, 20), "Turno VISIT. turnos Restantes " + turnosActuales);




	}

}
