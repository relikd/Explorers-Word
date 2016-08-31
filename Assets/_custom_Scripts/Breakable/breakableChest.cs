using UnityEngine;
using System.Collections;

/*
 * Das Skript zum Zerbrechen der Truhe in Raum 2. Es benoetigt die Ueberreste des OBjekts (Remains), Das Objekt, weclhes die Truhe zerbricht (BreakingObject),
 * Ein GameObject, dessen Collider als Trigger vorgibt, in welchem Gebiert die Truhe zerbechen darf (AllowedArea) sowie ein Array von GameObjects, die beim Zerbrechen als Inhalt activiert werden (contains).
 */ 
public class breakableChest : Breakable {

	[SerializeField]
	GameObject BreakingObject;
	[SerializeField]
	GameObject AllowedArea;
	[SerializeField]
	GameObject[] contains;
	private bool rightSpot;

	/*
	 * Setzt den Inhalt der Truhe auf inaktiv.
	 */
	void Start () {
		foreach (GameObject current in contains) {
			current.SetActive(false);
		}
	}
	/*
	 * Prüft bei Kollision, ob die Truhe im richtigen Bereich ist und ob die Kollision mit dem BreakingOBject war.
	 */
	public void OnCollisionEnter(Collision col) {
		if(rightSpot && col.gameObject == BreakingObject) {
			shatterChest();
		}
	}
	/*
	 * Markiert, dass die Truhe die AllowedArea betreten hat.
	 */ 
	public void OnTriggerEnter(Collider col) {
		if (col.gameObject == AllowedArea) rightSpot = true; 
	}
	/*
	 * Markiert, dass die Truhe die AllowedArea verlassen hat.
	 */
	public void OnTriggerExit(Collider col)
	{
		if (col.gameObject == AllowedArea) rightSpot = false;
	}
	/*
	 * Schaltet den Inhalt auf Aktiv und ruft shatter() auf.
	 */
	public void shatterChest() {
		LogWriter.WriteLog("Truhe zerbrochen", gameObject);
		foreach(GameObject current in contains) {
			current.transform.position = transform.position;
			current.SetActive(true);
		}
		shatter();
	}
}
