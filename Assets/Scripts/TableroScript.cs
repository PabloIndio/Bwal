using UnityEngine;
using System.Collections;

public class TableroScript : MonoBehaviour {


	public static TableroScript Instance;

	public static int numCeldasX=18,numCeldasY=27;
	public Vector2 comienzoTablero = new Vector2 (+10,+10);
	
	//Celdas
	
	private GameObject[,] celdas = new GameObject[numCeldasY,numCeldasX];
	public Transform celda;

	
	void Awake(){
		
		if (Instance != null)
			Debug.LogError ("No deberia de haber un Tablero aun");
		else
			Instance = this;
		
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
				posX+=tamCelda;
				
			}
			posY+=tamCelda;
			
		}
		
		
	}



	public void encenderCasillasAdy(GameObject pSeleccionado){

		Vector2 pos = pSeleccionado.GetComponent<PlayerScript>().getNumCelda();
		int lPase = pSeleccionado.GetComponent<PlayerScript>().getPase();
		
	
		for (int i = -lPase; i<=lPase ; i++) {
		
				for (int j= -lPase; j<=lPase; j++) {
					
				if((Mathf.Abs(i)+Mathf.Abs(j)<(lPase*2-((int)(lPase/3)))) && Mathf.Abs(i)<=lPase && Mathf.Abs(j)<=lPase
				   && (pos.x+i>=0 && pos.y+j>=0) && (pos.x+i<numCeldasY && pos.y+j<numCeldasX))

					 	celdas[(int)pos.x+i,(int)pos.y+j].GetComponent<CeldaScript> ().encenderAmarillo ();

					
				}
		}

	}

	
	
	public void apagarCasillasAdy(GameObject pSeleccionado){
		
				Vector2 pos = pSeleccionado.GetComponent<PlayerScript> ().getNumCelda ();
				int lPase = pSeleccionado.GetComponent<PlayerScript> ().getPase ();
		
				int inicioX = (int)pos.x - lPase;
				int finalX = (int)pos.x + lPase;
				int inicioY = (int)pos.y - lPase;
				int finalY = (int)pos.y + lPase;
		
		
				for (int i = inicioX; i<=finalX && i<numCeldasY; i++) {
			
						if (i >= 0) {
								for (int j= inicioY; j<=finalY && j<numCeldasX; j++) {
										if (j >= 0) {
						
												celdas [i, j].GetComponent<CeldaScript> ().apagar ();
						
										}
								}
						}
				}
	}

	public Vector2 getPosCelda(Vector2 numCelda){

		return celdas [(int)numCelda.x,(int)numCelda.y].transform.position;

	}



}
