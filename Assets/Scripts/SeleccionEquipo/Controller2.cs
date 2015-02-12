using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class Controller2 : MonoBehaviour {

    public Text t;
    public Text infoEquipo;
    public Text infoEquipo2;
    public InputField inp;
    public InputField inp2;
    public InputField inpCreacion;
    public static EquipoScript e1;
    public static EquipoScript e2;
    public EquipoScript e3;

	// Use this for initialization
	void Start () {

        verEquipos();
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void verEquipos()
    {
        string[] filePaths = Directory.GetFiles(@Application.persistentDataPath, "*.dat", SearchOption.AllDirectories);
        t.text = "";
        for (int i = 0; i < filePaths.Length; i++)
        {
            t.text += Path.GetFileNameWithoutExtension(filePaths[i]) + "\n";

        }
    }

    public void crearEquipoLocal()
    {
        e1 = new EquipoScript(inp.text);
  
    }


    public void cargarLocal()
    {
        e1 = load(inp.text);
        mostrarEquipo(e1,infoEquipo);

    }

    public void cargarVisitante()
    {
        e2 = load(inp2.text);
        mostrarEquipo(e2,infoEquipo2);
    }

    public void mostrarEquipo(EquipoScript e,Text info)
    {
        info.text = e.nombreEquipo +"\n";
        for (int i = 0; i < e.jugadores.Length; i++)
        {
            info.text += e.jugadores[i].nombre+" "+
                e.jugadores[i].lpase+" "+
                e.jugadores[i].intercepcion+" "+
                e.jugadores[i].velocidad+" "+
                e.jugadores[i].vidaTotal+"\n";

        }
    }


    public void borrarTexto()
    {
        t.text = "";
    }

   

    public EquipoScript load(string nombreEquipo)
    {
        BinaryFormatter bf = new BinaryFormatter();
        
        FileStream file = File.Open(Application.persistentDataPath + "/" + nombreEquipo + ".dat", FileMode.Open);
        TeamData data = (TeamData)bf.Deserialize(file);
        file.Close();

        EquipoScript e = new EquipoScript(data.getNombre());

        e.nombreEquipo = data.getNombre();
        e.posiciones = data.getPosiciones();
        e.jugadores = data.getJugadores();

        return e;
    
    }

    public void borrarArchivos()
    {
        string[] filePaths = Directory.GetFiles(@Application.persistentDataPath, "*.dat", SearchOption.AllDirectories);
        foreach (string filePath in filePaths)
            File.Delete(filePath);
        verEquipos();
    }


    public void comenzarPartida()
    {
        if (e1 != null && e2 != null)
            Application.LoadLevel("PrincipalScene");
        else
            Debug.Log("Equipos sin confirmar");
    }

    public void crearEquipoNuevo()
    {
        Application.LoadLevel("CrearEquipoScene");
    }

}
