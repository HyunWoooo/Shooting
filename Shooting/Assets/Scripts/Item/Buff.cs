using UnityEngine;
using System.Collections;

namespace BuffItem
{
    public class Buff : MonoBehaviour
    {
        protected int buff;

        public void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
                Destroy(this.gameObject);
        }

        public int GetBuff()
        {
            return buff;
        }
    }
}