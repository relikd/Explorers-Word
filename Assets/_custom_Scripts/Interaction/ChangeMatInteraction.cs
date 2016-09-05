using UnityEngine;

namespace Interaction
{
	[RequireComponent (typeof(BoxCollider))]
	/**
	 * Erlaubt es das Material eines Objekts beim interagieren zu aendern. Die Interaktion kann nur einmal erfolgen.
	 */
	public class ChangeMatInteraction: Interactable
	{
		[SerializeField] 
		string actionMessage; 
		[SerializeField]
		Material afterInteract;
		[SerializeField]
		BoxCollider BoxcolliderObj;

		/**
		 * Gibt den Text zurueck, der dem Spieler angezeigt wird, wenn er mit der Maus über das interagierbare Objekt faehrt.
		 */
		override public string interactMessage() {
			return actionMessage;
		}

		/**
		 * Ersetzt das Material des Bilds und zerstört den BoxCollider des Gameobjects um eine erneute Interaktion zu verhinden.
		 */
		override public void OnInteractionKeyPressed()
		{
			XplrDebug.LogWriter.Write("Material gewechselt", gameObject);
			gameObject.GetComponent<MeshRenderer>().material = afterInteract;
			BoxcolliderObj.enabled = false;
			this.enabled = false;
		}
	}
}