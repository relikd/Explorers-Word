using UnityEngine;
using System.Collections;

namespace Interaction
{
	/**
	 * Enables the player to carry an object (Rigidbody and BoxCollider required)
	 */
    [RequireComponent(typeof(BoxCollider))]
    [RequireComponent(typeof(Rigidbody))]
    public class HoldInteraction : Interactable
    {
        [SerializeField]
        float distance = 1.6f;
        [SerializeField]
        float minDistance = 0.8f;
        [SerializeField]
        float smoothnessOfInterpolation = 80;

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

        CharacterController controller;


        /**
         * Initialise all variables
         */
        void Awake()
        {
			tarRotation = new Quaternion ();
            Player = GameObject.Find("FPSController");
            mainCamera = Player.GetComponentInChildren<Camera>();
            myCollider = gameObject.GetComponent<BoxCollider>();
            myRigid = gameObject.GetComponent<Rigidbody>();
            halfbox = Vector3.Scale(myCollider.size * 0.5f, gameObject.transform.lossyScale);
            controller = Player.GetComponent<CharacterController>();
            safe = gameObject.transform.position;
            saferotate = gameObject.transform.rotation;
        }
        /**
         * Constantly update the object position and disable hold interaction even if the user is not in reach
         * Drop the object if a prohibited movement is detected
         */
        void Update()
        {   
            if (carrying)
            {
                safe = mostDistantSafePosition();
                if ((Input.GetButtonUp("Interact")))
                {
                    drop();
                    return;
                }
                if (!hasLineOfSight(safe))
                {
                    drop();
                    return;
                }
                Vector3 NewLoc = Vector3.Lerp(gameObject.transform.position, safe, Time.deltaTime * smoothnessOfInterpolation); 
                gameObject.transform.position = NewLoc;
                gameObject.transform.rotation = saferotate;
                dropIfBelow();
				XplrDebug.LogWriter.Write ("Carrying Object: " + carrying, gameObject);
            }
        }
		/**
		 * Just display the interaction message
		 */
        public override string interactMessage()
        {
            return "Taste halten zum Tragen";
        }
		/**
		 * Drop object upon mouse release
		 */
        public override void OnInteractionKeyPressed()
        {
            if (carrying)
			{
                drop();
            }
        }
		/**
		 * Carry while mouse button hold
		 */
        public override void OnInteractionKeyDown()
		{
			if (!carrying)
            {
                safe = gameObject.transform.position;
                pickup();
            }
        }
        /**
        * Pickup and set necessary parameters and variables
        */
        private void pickup()
        {
            if (!carrying)
            {
				playInteractionSound (0);
                carrying = true;
				XplrDebug.LogWriter.Write("aufgehoben", gameObject);
                oldLayer = gameObject.layer;
                gameObject.layer = newLayer;
                myRigid.isKinematic = true;
                myRigid.useGravity = false;
            }
        }
        /**
         * Drop and reset all necessary parameters and variables
         */
        private void drop()
        {
            if (carrying)
            {
				playInteractionSound (1);
                carrying = false;
                gameObject.layer = oldLayer;
				XplrDebug.LogWriter.Write("fallen gelassen", gameObject);
                myRigid.velocity = Vector3.zero;
                myRigid.angularVelocity = Vector3.zero;
                myRigid.isKinematic = false;
                myRigid.useGravity = true;
                myRigid.AddForce(transform.forward * 0.8f, ForceMode.VelocityChange);
                myRigid.AddForce(Vector3.up * 0.8f, ForceMode.VelocityChange);
            }

        }
        /**
         * Check if the destination position will have a collision
         */
        private bool colliding()
        {
            return Physics.CheckBox(target, halfbox, tarRotation, Physics.DefaultRaycastLayers);
        }

        private bool inbetween(float Checkdistance)
        {
            Ray ray = mainCamera.ScreenPointToRay(new Vector3(x, y));
            RaycastHit hit;
            return Physics.Raycast(ray, out hit, Checkdistance );
        }
        /**
         * Find the furthest point without a collision 
         */
        private Vector3 mostDistantSafePosition()
        {
            float checkDistance = distance;
            while (checkDistance > minDistance)
            {
                if (!inbetween(checkDistance))
                {
                    target = mainCamera.transform.position + mainCamera.transform.forward * checkDistance;
                    tarRotation.eulerAngles =  new Vector3(saferotate.eulerAngles.x, Player.transform.rotation.eulerAngles.y, saferotate.eulerAngles.z);
                    if (!colliding())
                    {
                        saferotate = tarRotation;
                        return target;
                    }
                }
                checkDistance = checkDistance - 0.001f;
            }
            return safe;
        }
        /**
         * Check for any obstacles between player and the predestinated position
         */
        private bool hasLineOfSight(Vector3 PositiontoTest)
        {
            Vector3 position = gameObject.transform.position;
            Ray ray = new Ray(position, PositiontoTest - position);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Vector3.Distance(PositiontoTest, position), Physics.AllLayers))
            {
                if (hit.collider.gameObject.GetHashCode() == gameObject.GetHashCode() || hit.collider.isTrigger)
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
        /*
         * Drop the object if it is in a sphere beneath the player
         */
        private void dropIfBelow()
        {
            Collider[] hitColliders = Physics.OverlapSphere(Player.transform.position + 0.45f * controller.height * Vector3.down, controller.radius * 0.95f, Physics.IgnoreRaycastLayer);
            int i = 0;
            while (i < hitColliders.Length)
            {
                if (hitColliders[i].gameObject == gameObject)
                {
                    drop();
                    return;
                }
                i++;
            }
        }
    }
}