using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CARTEL_FINAL : MonoBehaviour
{
    private PAJARO scriptPajaro;
    private InteraccionCarpincho scriptCarpincho;
    private ObjectInteraction scriptCiervo;
    private INTERACCION_TORTUGA scriptTortuga;


    public GameObject cartel;
    public GameObject cartelTapado;
    public GameObject islaLinda;

    public AudioClip musicaFinal;
    public AudioSource audioSource;
    private bool musicaReproducida = false;

    private bool desactivarNiebla = false;

    public Material skyboxMaterial;
    public Camera miCamara;

    //public Light myLight;          // Referencia a la luz que deseas modificar.
    //public float aumentoIntensidad = 2.0f; // Aumento temporal de la intensidad.
    // public float duracionAumento = 2.0f;  // Duración del aumento en segundos.
    // private float intensidadOriginal; // Almacena la intensidad original de la luz.
    public Light luzGeneral;

    public Animator animator;



    void Start()
    {
        scriptPajaro = FindObjectOfType<PAJARO>();
        scriptTortuga = FindObjectOfType<INTERACCION_TORTUGA>();
        scriptCarpincho = FindObjectOfType<InteraccionCarpincho>();
        scriptCiervo = FindObjectOfType<ObjectInteraction>();
        cartel.SetActive(false);
        islaLinda.SetActive(false);

        RenderSettings.fog = true;
        RenderSettings.fogDensity = 0.03f;

        luzGeneral.intensity = 0.2f;

        // myLight.enabled = false;
        // intensidadOriginal = myLight.intensity;

        //  animator = GetComponent<Animator>();      

    }

    // Update is called once per frame
    void Update()
    {

        if (scriptPajaro.pajaroCompletado && scriptTortuga.tortugaCompletado && scriptCarpincho.carpinchoCompletado && scriptCiervo.ciervoCompletado)
        {
            if (!musicaReproducida)
            {

                cartel.SetActive(true);
                cartelTapado.SetActive(false);
                islaLinda.SetActive(true);
                audioSource.PlayOneShot(musicaFinal);
                musicaReproducida = true;
                desactivarNiebla = true;
                animator.SetBool("final", true);
                luzGeneral.intensity = 2.0f;
                //StartCoroutine(AumentarIntensidadPorTiempo());

            }
            else
            {
                animator.SetBool("final", false);
            }


        }


        if (desactivarNiebla)
        {

            StartCoroutine(updateTheFog());

            if (miCamara != null && skyboxMaterial != null)
            {
                // Cambia el tipo de fondo a Skybox.
                miCamara.clearFlags = CameraClearFlags.Skybox;

                // Asigna el material de Skybox.
                RenderSettings.skybox = skyboxMaterial;
            }
            else
            {
                Debug.LogError("La cámara principal o el material de Skybox no están configurados.");
            }
        }


    }



    /* private IEnumerator AumentarIntensidadPorTiempo()
 {
     myLight.enabled = true;

     float tiempoTranscurrido = 0.0f;
     float intensidadInicial = 2.0f; // Cambia la intensidad inicial a 2f.

     while (tiempoTranscurrido < duracionAumento)
     {
         // Calcula la intensidad gradualmente en función del tiempo utilizando una interpolación suavizada.
         float t = tiempoTranscurrido / duracionAumento;
         float smoothT = Mathf.SmoothStep(0.0f, 1.0f, t);
         myLight.intensity = Mathf.Lerp(intensidadInicial, intensidadInicial + aumentoIntensidad, smoothT);

         tiempoTranscurrido += Time.deltaTime;
         yield return null;
     }

     // Asegúrate de que la intensidad sea exactamente igual a intensidadInicial.
     myLight.intensity = intensidadInicial;
     luzGeneral.intensity = 2.0f;

     yield break;

 }*/

    IEnumerator updateTheFog()
    {
        float targetFogDensity = 0.0f; // El valor al que deseas que la niebla desaparezca.
        float fogChangeRate = 0.001f;   // La velocidad a la que la niebla disminuirá.

        while (RenderSettings.fogDensity > targetFogDensity)
        {
            // Disminuye gradualmente la densidad de la niebla.
            RenderSettings.fogDensity -= fogChangeRate;

            // Espera 3 segundos antes de continuar con la siguiente iteración.
            yield return new WaitForSeconds(3);
        }
        // Puedes usar "yield break;" para detener el bucle si es necesario.
    }

    void CambiarEscena()
     {
        SceneManager.LoadScene(2);
     }
}
