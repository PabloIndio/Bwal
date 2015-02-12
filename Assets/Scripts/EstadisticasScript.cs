using UnityEngine;
using System.Collections;

public class EstadisticasScript {

	private static int probTotal=21;

    public static bool recepcion(int intercepcion)
    {
        int ran=Random.Range(0, probTotal);
        if (ran<=intercepcion)
        {
            MensajeScript.Instance.crearMensaje("Salió " + ran + " <= intercepción(" + intercepcion+"). Atrapa la bola");
            return true;
        }else{
            MensajeScript.Instance.crearMensaje("Salió " + ran + " >= intercepción(" + intercepcion + "). No atrapa");
            return false;
        }
    }

	public static bool placaje(int fuerzaAtaque, int fuerzaDefensa){

        //Al hacer un placaje, el Random + la cuarta parte de la defensa del rival tiene que ser menor que la fuerza del atacante
		int modif=fuerzaDefensa/4;
		int ran = Random.Range(0,probTotal);
        int total = ran+modif;
        MensajeScript.Instance.crearMensaje("La fAtaque tiene que ser mayor que Ran+fDefensa/4..");
		MensajeScript.Instance.crearMensaje("Random+fDefensa/4= "+total+"("+ran+"+"+modif+") y fAtaque: "+fuerzaAtaque);
		if(total<=fuerzaAtaque){
			MensajeScript.Instance.crearMensaje("Exito ataque");
			return true;
		}else{
			MensajeScript.Instance.crearMensaje("Fallo ataque");
			return false;
		}



	}

}
