using UnityEngine;
using System.Collections;
using System.IO;



public class GameControllerScript : MonoBehaviour {

	private static int numJugadoresLocales = 2;
	private static int numCeldasX=20,numCeldasY=30;
	private int golesL=0;
	private int golesV=0;
	public Vector2 comienzoTablero = new Vector2 (+10,+10);



	public static GameControllerScript Instance;
	private GameObject pSeleccionado;


	//Celdas

	private GameObject[,] celdas = new GameObject[numCeldasY,numCeldasX];
	public Transform celda;

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


		inicializarTablero ();

		leerFicheroJugadores ();


		//Crear la bola
		bola = (GameObject)Instantiate (Resources.Load ("Bola"), Vector3.zero, Quaternion.identity);

		bola.GetComponent<BolaScript> ().setNumCelda(new Vector2(8,8));

		bola.transform.position = celdas [8, 8].transform.position;

	}
	
	// Update is called once per frame
	void Update () {
	
		/*if(Input.GetButtonDown("Fire1"))
			bola.GetComponent<BolaScript>().desplazar((Camera.main.ScreenToWorldPoint(Input.mousePosition)));*/


	}

	private void inicializarTablero(){

		/*Inicializacion de tablero*/
		
		float tamCelda = (celda.GetComponent<BoxCollider2D> ().size.x)/2;
		
		
		float posY = comienzoTablero.y;
		
		for (int i=0; i<numCeldasY; i++) {
			float posX = comienzoTablero.x;
			for(int j=0; j<numCeldasX;j++){
				
				celdas[i,j] = (GameObject)Instantiate (Resources.Load ("Celda"), Vector3.zero, Quaternion.identity);		
				celdas[i,j].transform.position = new Vector3 (posX, posY,0.5f);
				celdas[i,j].GetComponent<CeldaScript>().setNumCelda(new Vector2(i,j));
				posX+=tamCelda;
				
			}
			posY+=tamCelda;
			
		}


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
		p.transform.position = celdas [(int)numCelda.x, (int)numCelda.y].transform.position;

	}


	public void encenderCasillasAdy(Vector2 pos, int lPase){

		int inicioX = (int)pos.x - lPase;
		int finalX = (int)pos.x + lPase;
		int inicioY = (int)pos.y - lPase;
		int finalY = (int)pos.y + lPase;


		for (int i = inicioX; i<=finalX && i<numCeldasY; i++) {

				if(i>=0){
					for (int j= inicioY; j<=finalY && j<numCeldasX; j++) {
							if(j>=0){
								if(i==inicioX || i==finalX || j== inicioY || j== finalY){
									celdas [i, j].GetComponent<CeldaScript> ().encenderRojo ();
								}else {
									celdas [i, j].GetComponent<CeldaScript> ().encenderAmarillo ();
								}
							}
					}
				}
		}

	
	}


	public void apagarCasillasAdy(Vector2 pos, int lPase){
		int inicioX = (int)pos.x - lPase;
		int finalX = (int)pos.x + lPase;
		int inicioY = (int)pos.y - lPase;
		int finalY = (int)pos.y + lPase;
		
		
		for (int i = inicioX; i<=finalX && i<numCeldasY; i++) {
			
			if(i>=0){
				for (int j= inicioY; j<=finalY && j<numCeldasX; j++) {
					if(j>=0){

						celdas [i, j].GetComponent<CeldaScript> ().apagar ();
						
					}
				}
			}
		}

	
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

		pSeleccionado = player;

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
