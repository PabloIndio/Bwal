﻿using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	private Vector2 target;
	public static float velDesplazamiento = 20.0f;
	private bool tieneLaBola=false;
	private Vector2 numCelda;
	private bool seleccionado = false;
	


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

	}

	public void desplazar(Vector2 destino, Vector2 celda){

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

		GameControllerScript.Instance.bola.GetComponent<BolaScript> ().desplazar (this.transform.position,numCelda);
		tieneLaBola = true;

	}


	public void lanzarBola(Vector2 destino){

		if (tieneLaBola) {

			GameControllerScript.Instance.bola.GetComponent<BolaScript>().desplazar(destino,numCelda);

		}

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


		seleccionado=!seleccionado;
		if (seleccionado) {
				GameControllerScript.Instance.seleccionarJugador (this.gameObject);
				GameControllerScript.Instance.encenderCasillasAdy (numCelda, lpase);
		}
		else{
			GameControllerScript.Instance.seleccionarJugador (null);
			GameControllerScript.Instance.apagarCasillasAdy (numCelda,lpase);
		}
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


	
}

	



