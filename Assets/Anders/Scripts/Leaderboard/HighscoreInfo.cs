using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Score", menuName = "Score")]
public class HighscoreInfo : ScriptableObject
{
    public string Name;
    public int Score;
}
