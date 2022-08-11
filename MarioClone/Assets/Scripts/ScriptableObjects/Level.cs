using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu (fileName = "New Level", menuName = "Scriptable Objects/Level")]
public class Level : ScriptableObject
{
    public int levelID;
    public string levelName;
    public Sprite levelIMG;
    public Object sceneToLoad;
    public int colectedCoins;
    public int maxCoins;
    public bool levelLock;
}
