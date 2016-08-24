using UnityEngine;
using System.Collections;

/**
 *  Skript, dass sich um das Dummy-Objekt kümmert, welches zur Kollisionsprüfung beim tragen verwendet wird.
 */
class DummyColliderObject : MonoBehaviour
{

    Camera mainCamera;
    GameObject Player;

    private bool colliding = false;
    public bool carrying = false;

    public float distance;
    public float smooth;
    public Vector3 safe;
    public Quaternion saferotate;

    Vector3 tarPos;

    int x = Screen.width / 2;
    int y = Screen.height / 2;
    /**
     * Aktualisiert die Position die geprüft wird. Wartet vor Auswertung auf nächstes Update der Physik.
     */
    void LateUpdate()
    {
        if (carrying) {
            tarPos = mainCamera.transform.position + mainCamera.transform.forward * distance;
            gameObject.transform.position = tarPos;
            gameObject.transform.rotation = Player.transform.rotation;
            StartCoroutine(PhysicCalc());
        }
    }
    /**
     * Wartet auf das nächste Update der Physik und testet dann die aktuelle Prüfposition.
     */
    IEnumerator PhysicCalc()
    {
        yield return new WaitForFixedUpdate();
        AmISafe();
    }
    /**
     * Testet die Prüfposition auf Kollisionen und auf Objekte die vor dem Spieler stehen.
     */
    void AmISafe() {
        bool inbetween = somethingInbetween();
        if (!colliding && !inbetween) {
            safe = gameObject.transform.position;
            saferotate = gameObject.transform.rotation;
        }
    }
    /**
     * Prüft ob ein Ojekt zwischen dem Spieler und dem getragenen Objekt steht.
     */
    private bool somethingInbetween()
    {        
        Ray ray = mainCamera.ScreenPointToRay(new Vector3(x, y));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit,100))
        {         
            if (hit.collider.GetHashCode() ==  gameObject.GetComponent<Collider>().GetHashCode())
            {
                return false;   
            }
            return true;
        }
        return true;
    }
    /**
     * Initialisiert die benötigten Variablen.
     */
    void Awake()
    {
        Player = GameObject.Find("FPSController");
        mainCamera = Player.GetComponentInChildren<Camera>();
    }
    /**
     * Merkt sich eintreffende Kollissionen.
     */
    void OnCollisionEnter(Collision collision) 
    {
        colliding = true;
    }
    /**
     * Merkt das Ende einer Kollission.
     */
    void OnCollisionExit(Collision collision)
    {
       colliding = false;        
    }
}
 