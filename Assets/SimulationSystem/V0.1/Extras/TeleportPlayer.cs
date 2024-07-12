using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPlayer : MonoBehaviour
{
    
    // Start is called before the first frame update
    public void UpdatePlayerPos(Transform transform)
    {
        this.transform.root.position = transform.position;  
    }
}
