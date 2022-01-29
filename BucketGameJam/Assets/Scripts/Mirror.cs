using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour
{
    
    public void ClickMirror()
    {
        //fade object then destroy it

        Object.Destroy(gameObject);
    }

}
