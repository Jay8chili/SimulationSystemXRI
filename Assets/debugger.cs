using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Utilities;

public class debugger : MonoBehaviour
{
   
    public XRBaseControllerInteractor XRBaseControllerInteractor;
    public XRDirectInteractor Interactor;
    // Start is called before the first frame update
    public void ButtonPressed(GameObject Go)
    {
        Debug.Log("Button" + Go.name + "Was pressed");
    }

    public void ObjectGrabbed(GameObject Go)
    {
        Debug.Log("Object:" + Go.name + "Was grabbed");
    }

    public void ObjectUngrabbed(GameObject Go)
    {
        Debug.Log("Object:" + Go.name + "Was Ungrabbed");
    }

    public void ObjectUngrabbed(string Go)
    {
        Debug.Log( Go );
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) { GetallXRInteratable(); }
    }
    public void GetallXRInteratable()
    {
      
    }
}
