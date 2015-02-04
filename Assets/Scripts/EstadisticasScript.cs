using UnityEngine;
using System.Collections;

public class EstadisticasScript {

	private static int probTotal=21;

    public static bool recepcion(int intercepcion)
    {
        int ran=Random.Range(0, probTotal);
        if (ran<=intercepcion)
        {
            MensajeScript.Instance.crearMensaje("Random fue " + ran + " que es menor o igual que " + intercepcion);
            return true;
        }else{
            MensajeScript.Instance.crearMensaje("Random fue " + ran + " que es mayor que " + intercepcion);
            return false;
        }
    }

	public static bool placaje(int fuerzaAtaque, int fuerzaDefensa){


		int modif=fuerzaDefensa/4;
		int ran = Random.Range(0,probTotal);
		MensajeScript.Instance.crearMensaje("ran= "+ran+" fAtk= "+fuerzaAtaque+" fDef/4= "+fuerzaDefensa/4);
		if(ran+modif<=fuerzaAtaque){
			MensajeScript.Instance.crearMensaje("Exito ataque");
			return true;
		}else{
			MensajeScript.Instance.crearMensaje("Fallo ataque");
			return false;
		}



	}

}
