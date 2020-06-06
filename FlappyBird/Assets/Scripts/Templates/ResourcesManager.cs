﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ResourcesManager", menuName = "Template/ResourcesManager")]
public class ResourcesManager : ScriptableSingleton<ResourcesManager>
{
    public List<Sprite> scores;
    public List<Sprite> highScores;
}