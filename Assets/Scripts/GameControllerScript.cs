using UnityEngine;
using System.Collections;
using System.IO;



public class GameControllerScript : MonoBehaviour {

	private static int numJugadoresLocales = 2;
	private int golesL=0;
	private int golesV=0;



	public static GameControllerScript Instance;
	private GameObject pSeleccionado;

	//Bola
	public GameObject bola;

	//Jugadores Locales
	private GameObject[] locales= new GameObject[numJugadoresLocales];

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

		leerFicheroJugadores ();

		inicializarBola ();
	}
	
	// Update is called once per frame
	void Update () {
	
		/*if(Input.GetButtonDown("Fire1"))
			bola.GetComponent<BolaScript>().desplazar((Camera.main.ScreenToWorldPoint(Input.mousePosition)));*/
	

	}


	private void inicializarBola(){

		//Crear la bola
		bola = (GameObject)Instantiate (Resources.Load ("Bola"), Vector3.zero, Quaternion.identity);
		
		bola.GetComponent<BolaScript> ().setNumCelda(new Vector2(8,8));
		
		bola.transform.position = TableroScript.Instance.getPosCelda(new Vector2 (8,8));
		bola.transform.position = new Vector3 (bola.transform.position.x, bola.transform.position.y, -0.5f);

	}

	private void leerFicheroJugadores(){

		
		//Leer de fichero las estadisticas de los jugadores
		StreamReader sr = new StreamReader(Application.dataPath+"/Resources/players.txt");
		string fileContents = sr.ReadToEnd();
		sr.Close();
		
		string[] lines = fileContents.Split ("\n"[0]);
		
		//Crear jugadores 
		for(int i = 0; i<2;i++) {
			
			string[] parametros =lines[i].Split(","[0]);
			
			inicializarJugador(locales[i],parametros,new Vector2 (Random.Range(2,10),Random.Range(2,10)));
		}
	}


	private void inicializarJugador(GameObject p, string[] par,Vector2 numCelda){

		p = (GameObject)Instantiate (Resources.Load ("Player"), Vector3.zero, Quaternion.identity);
		p.GetComponent<PlayerScript> ().setParametros (par[0],int.Parse(par[1]), int.Parse(par[2]), int.Parse(par[3]), 
		                                               int.Parse(par[4]), int.Parse(par[5]),int.Parse(par[6]), int.Parse(par[7])
		                                               , int.Parse(par[8]), int.Parse(par[9]),int.Parse(par[10]));

		p.GetComponent<PlayerScript> ().setNumCelda(numCelda);
		p.transform.position =TableroScript.Instance.getPosCelda(numCelda);

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

	//Jugador seleccionado para mostrar sus estadisticas en pantalla
	public void seleccionarJugador(GameObject player){

		if (pSeleccionado != null) {
						TableroScript.Instance.apagarCasillasAdy (pSeleccionado);
						pSeleccionado.GetComponent<PlayerScript>().apagarMenu();
		}
		pSeleccionado = player;
		TableroScript.Instance.encenderCasillasAdy (pSeleccionado);

	}



	public void desplazamiento(Vector2 celda){

		TableroScript.Instance.apagarCasillasAdy (pSeleccionado);
		pSeleccionado.GetComponent<PlayerScript> ().desplazar (TableroScript.Instance.getPosCelda(celda),celda);
		pSeleccionado = null;
	}





	void OnGUI(){
		
		if (pSeleccionado != null) {
			
			GUI.Label (new Rect (0, 0, 400, 20), pSeleccionado.GetComponent<PlayerScript>().getNombre()+
			           "   Vida: "+pSeleccionado.GetComponent<PlayerScript>().getVida().ToString()+"/"+
			           pSeleccionado.GetComponent<PlayerScript>().getVidaTotal().ToString()+
			           "   Vel: "+pSeleccionado.GetComponent<PlayerScript>().getVelocidad().ToString());

			
	
		}
	}

}
