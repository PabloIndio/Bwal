using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class MensajeScript : MonoBehaviour {

    public Text log;
    public Text marcador;
    public Text turno;
    public static MensajeScript Instance;
    private static int numLineasMax = 7;


    string loc;
    string vis;
    int golesL=0;
    int golesV=0;
  


    void Awake()
    {

        if (Instance != null)
            Debug.LogError("No debería de haber un MensajeScript");
        else
            Instance = this;

        log.text = "";
        for (int i = 0; i < numLineasMax ; i++)
        {
            log.text += " \n";
        }
        crearMensaje("Empieza la partida");


    }

    void Start()
    {
        
    }

    public void crearMensaje(Vector2 pos,string mensaje)
    {

        crearMensaje(mensaje);

    }

    public void crearMensaje(string nombre, string mensaje)
    {

        crearMensaje(nombre + ": " + mensaje);
        
    }

    public void crearMensaje(string mensaje)
    {
        
        borrarLinea();
        log.text +=DateTime.Now.ToLongTimeString() + " - " + mensaje+"\n";
   
    }

    private void borrarLinea()//Borra la última línea
    {

        string[] lineas = log.text.Split(new string[] { "\n" }, StringSplitOptions.None);

        log.text = "";
        
        for (int i = 1; i < numLineasMax; i++)
        {
            log.text += lineas[i]+"\n";
        }
    }

    public void setMarcador(string equipoLocal, int golesLocal, string equipoVis, int golesVisitante)
    {
        this.golesL=golesLocal;
        this.golesV=golesVisitante;
        this.loc=equipoLocal;
        this.vis=equipoVis;

        setMarcador();
    }

    public void golLocal()
    {
        this.golesL += 1;
        setMarcador();
        crearMensaje("GOL "+this.loc.ToUpper());
    }

    public void golVisitante()
    {
        this.golesV += 1;
        setMarcador();
        crearMensaje("GOL " + this.vis.ToUpper());
    }


    private void setMarcador()
    {

        this.marcador.text = loc.ToUpper() + " " + golesL + " - " + golesV + " " + vis.ToUpper();

    }

    public void gastarTurno(bool local, int turnos, bool cambioTurno)
    {
        if (local)
            turno.text = "TURNO\n\n"+loc;
        else
            turno.text = "TURNO\n\n"+vis;

        turno.text += "\n\n" + turnos;
        if(cambioTurno)
         crearMensaje("Cambio de turno");
    }


}
