using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmPopUp : MonoBehaviour
{
    

    public void YesConfirm()
    {
        GameManager.gm.LoadScene(0);
    }

    public void NoConfirm()
    {
        Destroy(gameObject);
    }

}
