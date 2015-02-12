using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class PlayerData
{
    public string nombre;
    public int vidaTotal = 10;
    public int vida;
    public int velocidad;
    public int fuerza;
    public int lpase;
    public int recepcion;
    public int dribbling;
    public int agilidadDefensa;
    public int intercepcion;
    public int golpe;
    public int probGolpe;



    public void setParametros(string nombre, int vidaTotal, int velocidad, int fuerza, int pase,
                          int recepcion, int dribbling, int agilidadDefensa,
                          int intercepcion, int golpe, int probGolpe)
    {


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

    public void setParametrosRandom(string nombre)
    {
        this.nombre = nombre;
        this.vidaTotal = UnityEngine.Random.Range(1, 21);
        this.vida = UnityEngine.Random.Range(1, 21);
        this.velocidad = UnityEngine.Random.Range(1, 21);
        this.fuerza = UnityEngine.Random.Range(1, 21);
        this.lpase = UnityEngine.Random.Range(1, 21);
        this.recepcion = UnityEngine.Random.Range(1, 21);
        this.dribbling = UnityEngine.Random.Range(1, 21);
        this.agilidadDefensa = UnityEngine.Random.Range(1, 21);
        this.intercepcion = UnityEngine.Random.Range(1, 21);
        this.golpe = UnityEngine.Random.Range(1, 21);
        this.probGolpe = UnityEngine.Random.Range(1, 21);
    }


    //Getters de estadisticas de jugador
    public string getNombre() { return this.nombre; }
    public int getVidaTotal() { return this.vidaTotal; }
    public int getVida() { return this.vida; }
    public int getVelocidad() { return this.velocidad; }
    public int getFuerza() { return this.fuerza; }
    public int getPase() { return this.lpase; }
    public int getRecepcion() { return this.recepcion; }
    public int getDribbling() { return this.dribbling; }
    public int getAgilidadDefensa() { return this.agilidadDefensa; }
    public int getIntercepcion() { return this.intercepcion; }
    public int getGolpe() { return this.golpe; }
    public int getProbGolpe() { return this.probGolpe; }


    public string ToString()
    {
        string s = "";
        s += "Nombre: " + nombre+" - "+"Vida: "+vidaTotal+" - "+"Pase: "+lpase+" - "+"Vel.: "+velocidad;
        return s;
    }
}
