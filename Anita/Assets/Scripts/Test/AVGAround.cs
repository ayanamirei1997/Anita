using UnityEngine;
using Anita;

public class AVGAround : MonoBehaviour, AnitaAround
{
    public AVGFrame next;

    public void After()
    {
        next.Begin();
    }

    public void Before()
    {
        Debug.Log("Before");
    }
}
