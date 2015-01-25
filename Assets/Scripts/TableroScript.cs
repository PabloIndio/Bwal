using UnityEngine;
using System.Collections;

public class TableroScript : MonoBehaviour {


	public static TableroScript Instance;

	private static int numCeldasX=19,numCeldasY=27;
	private Vector2 comienzoTablero = new Vector2 (-15,-15);
    private Vector2 centroTablero = new Vector2(8, 8);


	//Celdas
	private GameObject[,] celdas = new GameObject[numCeldasY,numCeldasX];
	public Transform celda;


	//Numero de celda seleccionada
	private Vector2 celdaDestino;
	
	void Awake(){
		
		if (Instance != null)
			Debug.LogError ("No deberia de haber un Tablero aún");
		else
			Instance = this;
        
        centroTablero = new Vector2((numCeldasY-1) / 2, (numCeldasX-1) / 2);
		
	}


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

	
	}

	public void inicializarTablero(){
		
		/*Inicializacion de tablero*/
		
		float tamCelda = (celda.GetComponent<BoxCollider2D> ().size.x)/2;
		
		
		float posY = comienzoTablero.y;
		
		for (int i=0; i<numCeldasY; i++) {
			float posX = comienzoTablero.x;
			for(int j=0; j<numCeldasX;j++){
				
				celdas[i,j] = (GameObject)Instantiate (Resources.Load ("Celda"), Vector3.zero, Quaternion.identity);		
				celdas[i,j].transform.position = new Vector3 (posX, posY,0.5f);
				celdas[i,j].GetComponent<CeldaScript>().setNumCelda(new Vector2(i,j));
                if ((i == 0 || i==numCeldasY-1) && j > 5 && j <= 12)
                    celdas[i, j].GetComponent<CeldaScript>().setMeta(i);
				posX+=tamCelda;
				
			}
			posY+=tamCelda;
			
		}
		
		
	}



	public void encenderCasillasAdy(GameObject pSeleccionado, int dist){

		Vector2 pos = pSeleccionado.GetComponent<PlayerScript>().getNumCelda();
	
		if (dist == 1) {
						for (int i = -dist; i<=dist; i++) 
								for (int j= -dist; j<=dist; j++) 
										celdas [(int)pos.x + i, (int)pos.y + j].GetComponent<CeldaScript> ().encenderAmarillo ();

		} else {

				for (int i = -dist; i<=dist; i++) {

						for (int j= -dist; j<=dist; j++) {
			
								if ((Mathf.Abs (i) + Mathf.Abs (j) < (dist * 2 - ((int)(dist / 3)))) && Mathf.Abs (i) <= dist && Mathf.Abs (j) <= dist
										&& (pos.x + i >= 0 && pos.y + j >= 0) && (pos.x + i < numCeldasY && pos.y + j < numCeldasX))

										celdas [(int)pos.x + i, (int)pos.y + j].GetComponent<CeldaScript> ().encenderAmarillo ();

			
						}
				}
		}

	}

	public void apagarTodasCasillas(){

		for (int i =0; i<numCeldasY; i++)
						for (int j=0; j<numCeldasX; j++)
								celdas [i, j].GetComponent<CeldaScript> ().apagar ();

	}


	public Vector2 getPosCelda(Vector2 numCelda){

		return celdas [(int)numCelda.x,(int)numCelda.y].transform.position;

	}

	public void setCeldaDestino(Vector2 numCel){
		celdaDestino = numCel;
		GameControllerScript.Instance.accionJugador (numCel);
	}

	public bool estaEncendida(Vector2 numCel){

		return celdas [(int)numCel.x, (int)numCel.y].GetComponent<CeldaScript> ().estaEncendida ();
	}


	public GameObject getCeldaObject(Vector2 numCel){

		return celdas[(int)numCel.x,(int)numCel.y];

	}

    public Vector2 getCentroTablero()
    {
        return centroTablero;
    }

	//Devuelve (si lo hay) un player que esta posicionado en ese numero de celda
	//Si la celda no esta encendida no devuelve al jugador
	public PlayerScript getPlayerCelda(Vector2 numCel){
        if (estaEncendida(numCel))
            return getCeldaObject(numCel).GetComponent<CeldaScript>().getPlayer();
        else
            return null;
	}

	//Almacenar en la propia celda el jugador que esta posicionado en ella
	public void posicionarJudador(PlayerScript player){
		getCeldaObject (player.getNumCelda()).
            GetComponent<CeldaScript> ().setPlayer (player);
	}

    public void quitarJugadorDeCelda(Vector2 numCelda)
    {
        getCeldaObject(numCelda).GetComponent<CeldaScript>().setPlayer(null);
    }


}
