using System;
using UnityEngine;
using System.Collections;
using JetBrains.Annotations;

public interface IPowerUp
{
    void UsePowerUp(GameObject gObject, IPowerUp powerup);
    String Name();

}
