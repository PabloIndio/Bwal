using UnityEngine;
using System.Collections;

public class BolaScript : MonoBehaviour {

	//Destino al que se mueve la bola y velocidad
	private Vector3 target;
	public float speed = 90f;

	//Numero de celda en la que se encuentra
	private Vector2 numCelda;

    //Que equipo fue el último que tuvo la bola (Para saber que equipo realizó un lanzamiento)
    private bool ultimoPoseedorLocal = false;
	private GameObject jugadorConLaBola;

    //Cuando la bola está en movimiento tras ser lanzada exclusivamente
    private bool enMovimiento = false;


	// Use this for initialization
	void Start () {

		target = transform.position;
        this.gameObject.GetComponent<SpriteRenderer>().color = Color.blue;

	}
	
	// Update is called once per frame
	void Update () {
		//Si hay un jugador que tiene la bola tiene que seguirlo
		if (jugadorConLaBola != null)
						target = new Vector3(jugadorConLaBola.transform.position.x+0.4f,
			                     jugadorConLaBola.transform.position.y,transform.position.z);

		transform.position = Vector3.MoveTowards (transform.position,
	                                          target, 
	                                          speed * Time.deltaTime);

        if (transform.position == target)
            enMovimiento = false;

	}

	//Cuando alguien lanza la bola esta ya no tiene poseedor
	public void desplazar(Vector2 destino,Vector2 celda){
        enMovimiento = true;
		jugadorConLaBola = null;
		numCelda = celda;
		target = new Vector3(destino.x,destino.y,transform.position.z);
	}


    //La bola se posiciona en una celda directamente sin tener que desplazarse
    public void posicionarEnCelda(Vector2 numCel)
    {
        this.numCelda = numCel;
        this.target = TableroScript.Instance.getPosCelda(numCel);
        this.target.z = this.transform.position.z;
        this.transform.position = TableroScript.Instance.getPosCelda(numCel);  

    }


	public Vector2 getNumCelda(){
		return numCelda;
	}
	
	public void setNumCelda(Vector2 celda){
		numCelda = celda;
	}

	//El jugador pide permiso para conseguir la bola
	public bool setPoseedor(GameObject jugador){
		if (jugadorConLaBola == null) {
				jugadorConLaBola = jugador;
                ultimoPoseedorLocal = jugador.GetComponent<PlayerScript>().isLocal();
				numCelda = jugadorConLaBola.GetComponent<PlayerScript>().getNumCelda();
				return true;
		} else {
				return false;
		}

	}

    public bool estaEnMovimiento()
    {
        return enMovimiento;
    }

    public bool getUltimoPoseedorLocal()
    {
        return ultimoPoseedorLocal;
    }
}
