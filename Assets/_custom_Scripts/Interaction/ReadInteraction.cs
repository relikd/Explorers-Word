using UnityEngine;
using System.Collections;
using Interaction;
using UnityStandardAssets.Characters.FirstPerson;

/**
 * Ein Skript, dass es erlaubt, den Text auf dem versteckten Papier zu lesen. Es erfordert, dass dieses Papier ein Child Canvas besitzt. Während der Spieler liest, kann er sich nicht bewegen und die Kameera nicht rotieren.
 */
public class ReadInteraction : Interactable
{
    [SerializeField] 
    public string actionMessage = "read";
    [SerializeField]
    public string retrurnMessage = "stop reading";
    [SerializeField]
    public char returnKey = 'R';
    private bool reading = false;
    private CustomFirstPersonController CFPS;
    private Reachable ray;
    private float lastWalkingSpeed;
    private float lastRunningSpeed;
    private char storedKey;
    private string storedMessage;
    private AudioSource PlayerSound;

    /**
     * Initialisiert einige Variablen.
     */
    void Awake() {
        CFPS = GameObject.Find("FPSController").GetComponent<CustomFirstPersonController>();
        PlayerSound = GameObject.Find("FPSController").GetComponent<AudioSource>();
        ray = GameObject.Find("FPSController").GetComponentInChildren<Reachable>();
        storedKey = interactionKey;
        storedMessage = actionMessage;
    }

    /**
     * Da die Sprungfunktion nicht ausgeschlaten werden kann, muss Springen das lesen des Spielers abbrechen. 
     */
    void LateUpdate() {
        if(reading && Input.GetKeyDown(KeyCode.Space)) {
            OnInteractionKeyPressed();
        }
    }
    
    override public string interactMessage()
    {
        return actionMessage;
    }

    /**
     * Schaltet den Lesemodus an bzw. aus, abhängig vom aktuell aktiven Modus. 
     */
    override public void OnInteractionKeyPressed()
    {
        if (!reading)
        {
            reading = !reading;
            
            lastRunningSpeed = CFPS.m_RunSpeed;
            lastWalkingSpeed = CFPS.m_WalkSpeed;

            toggleCanvas();
            toggleMouseCrosshair();
            toggleGUIMessage();
            DisablePlayerSound();
            changeWalkingAndRunningSpeed(0, 0);
            LogWriter.WriteLog("lesen", gameObject);
        }
        else
        {
            reading = !reading;

            toggleCanvas();
            toggleMouseCrosshair();
            toggleGUIMessage();
            DisablePlayerSound();
            changeWalkingAndRunningSpeed(lastWalkingSpeed, lastRunningSpeed);
            LogWriter.WriteLog("lesen beendet", gameObject);
        }
    }

    /**
     * Setzt die Lauf- und die Renngeschwindigkeit des Spielers auf die gegebenen Werte. 
     */
    private void changeWalkingAndRunningSpeed(float walkingSpeed, float runningSpeed)
    {
        CFPS.m_RunSpeed = runningSpeed;
        CFPS.m_WalkSpeed = walkingSpeed;
    }
    
    /**
     * Deaktiviert die Spieler-Soundquelle. 
     */
    private void DisablePlayerSound()
    {
        if (PlayerSound)
        {
            PlayerSound.mute = reading;
        }
    }

    /**
     * Schaltet die Sichtbarkeit des Canvas um.
     */
    private void toggleCanvas() 
    {
        gameObject.GetComponentInChildren<Canvas>().enabled = reading;

    }
    /**
     * Scahltet die Sichtbarkeit des Fadenkreuzes um. 
     */
    private void toggleMouseCrosshair() 
    {
        GameObject.Find("FirstPersonCharacter").GetComponent<MouseCrosshair>().enabled = !reading;

    }

    /**
     * Schaltet die GUI-Message um, damit im Lesemodus die Option zum verlassen angezeigt wird. 
     */
    private void toggleGUIMessage() {
        if (reading)
        {
            toggleMouseRotate();
            toggleRay();
            EnableGUI(false);
            interactionKey = returnKey;
            actionMessage = retrurnMessage;
            toggleRay();

        }
        else
        {
            toggleMouseRotate();
            toggleRay();
            EnableGUI(false);
            interactionKey = storedKey;
            actionMessage = storedMessage;
            toggleRay();
        }
    }

    /**
     * Schaltet das Reachable Skript, damit der Raycast keine Probleme bereitet, während die neue GUI-Message eingetragen wird.
     */
    private void toggleRay() {
        ray.enabled = !ray.enabled;
    }

    /**
     * Schaltet die Rotation durch bewegen der Maus an bzw. aus.
     */
    private void toggleMouseRotate()
    {
        CFPS.toggleRotate();
    }
}
