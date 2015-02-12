using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

public class EquipoScript {

    public static int numJugadores=5;
    public string nombreEquipo;
    public Vector2[] posiciones = new Vector2[numJugadores];
    public PlayerData[] jugadores = new PlayerData[numJugadores];


 

    public EquipoScript(string nombre)
    {
       
        this.nombreEquipo = nombre;
        generarPosicionesRandom();
        generarJugadoresRandom();
    }

    public EquipoScript(string nombre, PlayerData[] players)
    {
        this.nombreEquipo = nombre;
        generarPosicionesRandom();
        this.jugadores = players;
        
    }

    private void generarPosicionesRandom()
    {
        for (int i = 0; i < numJugadores; i++)
        {
            posiciones[i] = new Vector2(i +3 , (i+3) *2);
        }
    }

    private void generarJugadoresRandom()
    {
        for (int i = 0; i < numJugadores; i++)
        {
            jugadores[i]= new PlayerData();
            jugadores[i].setParametros("Tali", 10, 10, 10,10, 10, 10, 10, 10, 10, 10);
        }
        
    }

   

}

[Serializable]
class TeamData
{
    public string nombre;

    public Vector2Ser[] posiciones = new Vector2Ser[EquipoScript.numJugadores];

    public PlayerData[] jugadores = new PlayerData[EquipoScript.numJugadores];


    public void setNombre(string nom)
    {
        this.nombre = nom;
    }
    public string getNombre()
    {
        return nombre;
    }

    public void setPosiciones(Vector2[] pos)
    {
        for (int i = 0; i < pos.Length; i++)
            this.posiciones[i] = new Vector2Ser(pos[i].x,pos[i].y);
    }

    public Vector2[] getPosiciones()
    {
        Vector2[] v = new Vector2[posiciones.Length];
        for (int i = 0; i < posiciones.Length; i++)
            v[i] = new Vector2(posiciones[i].x, posiciones[i].y);

        return v;
    }


    public void setJugadores(GameObject[] players)
    {
        if (players.Length != EquipoScript.numJugadores)
            return;

        PlayerScript[] pls = new PlayerScript[EquipoScript.numJugadores];

        for (int i=0; i < EquipoScript.numJugadores; i++)
        {
            pls[i]=players[i].GetComponent<PlayerScript>();

        }

        setJugadores(pls);

    }

    public void setJugadores(PlayerScript[] jug)
    {

        for (int i = 0; i < EquipoScript.numJugadores; i++)
        {
            jugadores[i].setParametros(jug[i].getNombre(),
                jug[i].getVidaTotal(),
                jug[i].getVelocidad(),
                jug[i].getFuerza(),
                jug[i].getPase(),
                jug[i].getRecepcion(),
                jug[i].getDribbling(),
                jug[i].getAgilidadDefensa(),
                jug[i].getIntercepcion(),
                jug[i].getGolpe(),
                jug[i].getProbGolpe());
        }

    }

    public void setJugadores(PlayerData[] jug)
    {
        jugadores = jug;
    }


    /*public PlayerScript[] getJugadores()
    {
        PlayerScript[] players = new PlayerScript[EquipoScript.numJugadores];


        for (int i = 0; i < EquipoScript.numJugadores; i++)
        {
            PlayerData j = jugadores[0];
            players[i].setParametros(
                j.nombre, j.vidaTotal, j.velocidad, j.fuerza, 
                j.lpase, j.recepcion, j.dribbling, j.agilidadDefensa,
                j.intercepcion, j.golpe, j.probGolpe);

        }


        return players;
    }*/

    public PlayerData[] getJugadores()
    {
        return jugadores;
    }

 


}


[Serializable]
class Vector2Ser
{
    public float x;
    public float y;

    public Vector2Ser(float x, float y)
    {
        this.x = x;
        this.y = y;
    }

}