using UnityEngine;
using UnityEngine.SceneManagement;

public class Script_Menu : MonoBehaviour
{
    public void ProbarBoton()
    {
        Debug.Log("¡Botón pulsado!");
    }

    // Método original para empezar nivel
    public void EmpezarNivel(string NombreNivel)
    {
        Debug.Log("Intentando cargar escena: " + NombreNivel);
        if (Application.CanStreamedLevelBeLoaded(NombreNivel))
        {
            SceneManager.LoadScene(NombreNivel);
        }
        else
        {
            Debug.LogError("No se puede cargar la escena: " + NombreNivel);
        }
    }

    //Metodo que al principio iba a usar pero es mejor el que puse en el codigo de MainOpciones
    /*
    public void IrAOpciones()
    {
        string nombreEscenaOpciones = "Opciones";
        if (Application.CanStreamedLevelBeLoaded(nombreEscenaOpciones))
        {
            SceneManager.LoadScene(nombreEscenaOpciones);
        }
        else
        {
            Debug.LogError("No se puede cargar la escena de opciones.");
        }
    }
    */


    public void Salir()
    {
        Debug.Log("Salir del juego");
        Application.Quit();
    }
}
