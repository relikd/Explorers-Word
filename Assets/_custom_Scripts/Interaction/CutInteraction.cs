using UnityEngine;
using Interaction;

/**
 * Ein Skript, dass die Interaktion mit dem Bild in Raum 2 erlaubt, um es zu zerschneiden. Dabei wird das Material ersetzt. Die Interaktion kann nur einmal erfolgen.
 */
public class CutInteraction : Interactable
{
    
    private string actionMessage = "cut open the painting";
    private Material newMaterial = (Material)Resources.Load("chineseBattleFieldPaintingMaterialRipped", typeof(Material));

    
    /**
     * Gibt den Text zurueck, der dem Spieler angezeigt wird, wenn er mit der Maus über das interagierbare Objekt faehrt.
     */ 
    override public string interactMessage()
    {
        return actionMessage;
    }

    /**
     * Ersetzt das Material des Bilds und zerstört den BoxCollider des Gameobjects um eine erneute Interaktion zu verhinden.
     */
    override public void OnInteractionKeyPressed()
    {
        LogWriter.WriteLog("Bild aufgeschnitten", gameObject);
        gameObject.GetComponent<MeshRenderer>().material = newMaterial;
        Destroy(gameObject.GetComponent<BoxCollider>());
        this.enabled = false;
    }
}
