using UnityEngine;
using System;
using Interaction;
/**
 * Ein Skript, das es erlaubt GameObjects einzusammeln. Da es kein Inventar gibt wird das GO nicht eingesammelt, sondern deaktiviert. Zusaetlich wir ein beliebiges Skript an ein anderes GO angehaengt um die Funktionalitaet zur Vefuegung zu stellen, die durch das Einsammeln möglich wird. */
public class CollectInteraction : Interactable
{
    [SerializeField] private string NameOfItem;
    [SerializeField] private String NameOfNewScript;
    [SerializeField] private GameObject TargetGameObject;

    /**
     * Gibt den Text zurueck, der dem Spieler angezeigt wird, wenn er mit der Maus über das interagierbare Objekt faehrt.
     */
    override public string interactMessage() {
		return "pick up " + NameOfItem ;
	}

    /**
     * Fuegt dem uebergebenen GameObject das uebergebene Skript an, falls vorhanden.
     */
    private void attachScript()
    {
        TargetGameObject.AddComponent(Type.GetType(NameOfNewScript));
    }

    /**
     * Ruft attachScript auf und deaktiviert das eingesammelte GameObject.
     */
    override public void OnInteractionKeyPressed () {
        if (TargetGameObject)  attachScript();
		this.gameObject.SetActive(false);
	}
}