using UnityEngine;
using System.Collections;


namespace Interaction
{

    [RequireComponent(typeof(BoxCollider))]
    [RequireComponent(typeof(Rigidbody))]
    /**
     * Ein Skript, dass es erlaubt, Objekte zu tragen, die einen Rigidbody und einen BoxCollider besitzen.
     */
    public class HoldInteraction : Interactable
    {
        [SerializeField]
        float distance = 2.5f;
        [SerializeField]
        float minDistance = 0.5f;
        [SerializeField]
        float smoothnessOfInterpolation = 8;

        Camera mainCamera;
        GameObject Player;
        private BoxCollider myCollider;
        private Rigidbody myRigid;
        Vector3 halfbox;

        private bool carrying = false;

        int x = Screen.width / 2;
        int y = Screen.height / 2;

        private Vector3 safe;
        private Quaternion saferotate;
        private Vector3 target;
        private Quaternion tarRotation;
        private int oldLayer;
        private int newLayer = 2;

        bool dropping = false;
        /**
         *Initialisieren von Vairablen. 
         */
        void Awake()
        {
            Player = GameObject.Find("FPSController");
            mainCamera = Player.GetComponentInChildren<Camera>();
            myCollider = gameObject.GetComponent<BoxCollider>();
            myRigid = gameObject.GetComponent<Rigidbody>();
            halfbox = myCollider.size * 0.5f;
        }
        /**
         * Erneuert die Position des Objekts beim Tragen und erlaubt es das Tragen auch dann abzubrechen, wenn das Objekt eigentlich nicht in Reichweite ist. Bricht  Tragen ab, wenn unerlaubte Bewegungen versucht werden.
         */
        void Update()
        {
            if (carrying)
            {
                safe = mostDistantSafePosition();
                if ((Input.GetButtonUp("Interact"))) drop();
                Vector3 NewLoc = Vector3.Lerp(gameObject.transform.position, safe, Time.deltaTime * smoothnessOfInterpolation);
                hasLineOfSight(NewLoc);
                gameObject.transform.position = NewLoc;
                gameObject.transform.rotation = saferotate;
                float curDist = Vector3.Distance(mainCamera.transform.position, gameObject.transform.position);
                if (curDist < minDistance*0.75 || curDist > distance*1.5) drop();

            }
        }
        public override string interactMessage()
        {
            return "carry";
        }
        public override void OnInteractionKeyPressed()
        {

            if (carrying)
            {
                drop();
            }
        }

        public override void OnInteractionKeyDown()
        {
            if (!carrying)
            {
                safe = gameObject.transform.position;
                pickup();
            }
        }


        /**
        * Hebt das Objekt auf und setzt entsprechende Parameter.
        */
        private void pickup()
        {
            carrying = true;
            LogWriter.WriteLog("aufgehoben", gameObject);
            oldLayer = gameObject.layer;
            gameObject.layer = newLayer;
            myRigid.isKinematic = true;
          myRigid.useGravity = false;

        }
        /**
         * Laesst das Objekt fallen und setzt entsprechende Parameter.
         */
        private void drop()
        {
            carrying = false;
            gameObject.layer = oldLayer;
            LogWriter.WriteLog("fallen gelassen", gameObject);
            myRigid.velocity = Vector3.zero;
            myRigid.angularVelocity = Vector3.zero;

            myRigid.isKinematic = false;
            myRigid.useGravity = true;

        }
        /**
         * Prueft ob an der Zielposition eine Kollision stattfinden wuerde.
         */
        private bool colliding()
        {
            return Physics.CheckBox(target, halfbox, tarRotation, Physics.DefaultRaycastLayers);
        }

        private bool inbetween(float Checkdistance)
        {
            Ray ray = mainCamera.ScreenPointToRay(new Vector3(x, y));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Checkdistance + halfbox.z))
            {
                return true;
            }
            return false;
        }
        /**
         * Sucht den am weitesten entfernten Punkt, auf dem das Objekt nicht kollidiert
         */
        private Vector3 mostDistantSafePosition()
        {
            float checkDistance = distance;

            while (checkDistance > minDistance)
            {
                if (!inbetween(checkDistance))
                {
                    target = mainCamera.transform.position + mainCamera.transform.forward * checkDistance;
                    tarRotation = Player.transform.rotation;
                    if (!colliding())
                    {
                        saferotate = tarRotation;
                        return target;
                    }
                }
                checkDistance = checkDistance - 0.1f;

            }
            return safe;
        }
        /**
         * Prueft, ob es eine direkte Linie zwischen der aktuellen und der geplanten Position gibt. 
         */
        private bool hasLineOfSight(Vector3 PositiontoTest)
        {
            Vector3 position = gameObject.transform.position;
            Ray ray = new Ray(position, PositiontoTest - position);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Vector3.Distance(PositiontoTest, position)))
            {
                if (hit.collider.gameObject.GetHashCode() == gameObject.GetHashCode())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }
    }
}