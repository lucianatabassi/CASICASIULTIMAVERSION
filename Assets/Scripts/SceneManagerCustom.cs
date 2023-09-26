using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerCustom : MonoBehaviour
{
    public string NombreEscena;
    void Update()
    {
        // Verificar si se presiona la tecla "D"
        if (Input.GetKeyDown(KeyCode.D))
        {
            // Cargar la escena deseada
            SceneManager.LoadScene(NombreEscena);
        }
    }
}
