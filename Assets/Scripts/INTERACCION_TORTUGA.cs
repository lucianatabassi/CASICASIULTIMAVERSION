using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class INTERACCION_TORTUGA : MonoBehaviour
{
    private bool isCarrying = false;
    private GameObject carriedObject;
    public GameObject destinationObject; // Objeto destino con Animator
     private bool isMovingForward = false;
    public bool tortugaCompletado = true;

    public AudioClip sonido;
    public AudioSource audioSource;

    public bool circuloSi = false;
    public GameObject circulo;

    void Start()
    {
        circulo.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) //DESCOMENTAR PARA PC
        //if  (Input.GetKey("joystick button 1")) //DESCOMENTAR PARA APK
        {
            if (!isCarrying)
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.CompareTag("Tortuga"))
                    {
                        PickUpObject(hit.collider.gameObject);
                        if (circuloSi)
                        {
                            circulo.SetActive(true);
                        }
                    }
                }
            }
            else
            {
                DropObject();
                circulo.SetActive(false);
            }
        }
        if(isMovingForward){
            MoveForward();
        }


    }

    void PickUpObject(GameObject obj)
    {
        isCarrying = true;
        carriedObject = obj;
        obj.GetComponent<Rigidbody>().isKinematic = true;
        Rigidbody objRigidbody = obj.GetComponent<Rigidbody>();
    if (objRigidbody != null)
    {
        objRigidbody.useGravity = false;
    }
    
    obj.transform.SetParent(Camera.main.transform);
        circuloSi = true;
    }

    void DropObject()
    {
        isCarrying = false;
        carriedObject.GetComponent<Rigidbody>().isKinematic = false;
        carriedObject.transform.SetParent(null);

        // Verificar si el objeto se entrega en el destino
        float distanceToDestination = Vector3.Distance(carriedObject.transform.position, destinationObject.transform.position);
        if (distanceToDestination < 10.0f) // Ajusta el valor de distancia según tus necesidades
        {
            // Activa la animación en el objeto "carriedObject"
            Animator carriedAnimator = carriedObject.GetComponent<Animator>();
            
            if (carriedAnimator != null)
            {
                carriedAnimator.SetTrigger("ObjectDropped");
            }
            // Iniciar movimiento hacia adelante
            StartCoroutine(MoveForwardForDuration(2.0f));
            tortugaCompletado = true;
            audioSource.PlayOneShot(sonido);
        }
          Rigidbody carriedRigidbody = carriedObject.GetComponent<Rigidbody>();
    if (carriedRigidbody != null)
    {
        carriedRigidbody.useGravity = true;
    }
    }

    void MoveForward()
    {
        // Mover el objeto hacia adelante aquí, por ejemplo, modificando su posición
        float speed = 2.0f; // Velocidad de movimiento
        carriedObject.transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    IEnumerator MoveForwardForDuration(float duration)
    {
        isMovingForward = true;
        yield return new WaitForSeconds(duration);
        isMovingForward = false;
    }
}

