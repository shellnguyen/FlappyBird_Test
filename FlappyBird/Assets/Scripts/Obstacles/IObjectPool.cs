﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObjectPool
{
    void OnUpdate();
    void OnReuse();
}
