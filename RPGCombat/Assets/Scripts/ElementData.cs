using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Element", menuName = "element")]
public class ElementData : ScriptableObject
{
    public string elementName;
    public string strongAgainstElement;
    public string weakAgainstElement;

    public float weakToElementModifier;
    public float strongAgainstElementModifier;

    public Sprite elementSprite;
}
