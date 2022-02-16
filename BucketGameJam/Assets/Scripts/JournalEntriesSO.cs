using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class JournalEntriesSO : ScriptableObject
{
    public GameManager.months month;
    [TextArea]
    public string[] entryTexts;
    public Sprite[] sketches;

}
