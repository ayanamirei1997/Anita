using UnityEngine;

namespace Anita
{
    public class AVGAroundDefault : MonoBehaviour, AnitaAround
    {
        public void After() { print("AVG End"); }

        public void Before() { print("AVG Start"); }
    }
}
