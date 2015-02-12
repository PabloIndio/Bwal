using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class Controller3 : MonoBehaviour {

    public PlayerData[] players;

    public InputField nombreEquipo;
    public InputField nombreJugador;
    public Text jugadores;

    private int numJugadoresCreados = 0;

	// Use this for initialization
	void Start () {

        jugadores.text = "";
        players = new PlayerData[EquipoScript.numJugadores];
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void crearJugador()
    {
        players[numJugadoresCreados] = new PlayerData();
        players[numJugadoresCreados].setParametrosRandom(nombreJugador.text);
        jugadores.text += players[numJugadoresCreados].ToString()+"\n";
        numJugadoresCreados += 1;
        nombreJugador.text = "";
    }

    public void crearEquipo()
    {
        if (players.Length == EquipoScript.numJugadores && !nombreEquipo.text.Equals(""))
        {
            EquipoScript e = new EquipoScript(nombreEquipo.text, players);
            save(e);
            Application.LoadLevel("SeleccionEquipoScene");
        }
        else
        {
            Debug.Log("Datos incompletos");
        }
    }

    //Guardar los datos de un equipo
    public void save(EquipoScript e)
    {

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/" + e.nombreEquipo + ".dat");

        TeamData data = new TeamData();
        data.setNombre(e.nombreEquipo);
        data.setPosiciones(e.posiciones);
        data.setJugadores(e.jugadores);
        bf.Serialize(file, data);
        file.Close();
    }

    public void cancelar()
    {
        Application.LoadLevel("SeleccionEquipoScene");
    }

}
