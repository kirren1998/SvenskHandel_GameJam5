using UnityEngine;

namespace Script.Player
{
    public class Pickup : MonoBehaviour
    {
        private bool m_IsPickedUp;
        
        public void PickupCheck(Transform theDest, Transform dropDest)
        {
            Debug.Log("Pick up");
            if (m_IsPickedUp)
            {
                DropItem(dropDest);
            }
            else
            {
                PickupItem(theDest);
            }
        }
        
        void PickupItem(Transform theDest)
        {
            GetComponent<Rigidbody>().isKinematic = true;
            transform.position = theDest.position;
            transform.parent = GameObject.Find("Pickup Destination").transform;
            m_IsPickedUp = true;
        }

        void DropItem(Transform dropDest)
        {
            var transform1 = transform;
            transform1.parent = null;
            transform1.position = dropDest.position;
            m_IsPickedUp = false;
        }
    }
}
