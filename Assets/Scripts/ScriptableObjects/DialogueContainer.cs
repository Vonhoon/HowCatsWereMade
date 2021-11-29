using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialogues")]

public class DialogueContainer : ScriptableObject
{
    public List<string> dialogues;
    public List<string> dialoguesEN;
}
