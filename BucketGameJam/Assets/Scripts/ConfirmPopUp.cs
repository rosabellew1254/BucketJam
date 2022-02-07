using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmPopUp : MonoBehaviour
{
    

    public void YesConfirm()
    {
        GameManager.gm.LoadScene(0);
        Destroy(gameObject);
    }

    public void NoConfirm()
    {
        Destroy(gameObject);
    }

}
