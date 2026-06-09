using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum MenuOrigen
{
    Principal,
    Pausa
}
public class MainOpciones: MonoBehaviour
{

    public Slider musicSlider;

    void Start()
    {
        musicSlider.onValueChanged.RemoveAllListeners();
        musicSlider.value = MusicManager.Instance.GetMusicVolume();
        musicSlider.onValueChanged.AddListener(ChangeVolume);
    }

    void ChangeVolume(float value)
    {
        MusicManager.Instance.SetMusicVolume(value);
    }

    public GameObject menuPausa; // Canvas de pausa
    public GameObject menuOpciones; // Canvas de opciones

    private MenuOrigen origen;

    public void IrAOpcionesDesdePrincipal()
    {
        origen = MenuOrigen.Principal;
        SceneManager.LoadScene("Opciones"); // Abre la escena de opciones
    }

    public void AbrirDesdePausa()
    {
        origen = MenuOrigen.Pausa;
        menuPausa.SetActive(false);
        menuOpciones.SetActive(true);
    }

    public void SalirOpciones()
    {
        if (origen == MenuOrigen.Principal)
        {
            // Volver al menú principal cargando la escena
            SceneManager.LoadScene("MainMenu");
        }
        else if (origen == MenuOrigen.Pausa)
        {
            // Volver al Canvas de pausa
            menuOpciones.SetActive(false);
            menuPausa.SetActive(true);
        }
    }
}

