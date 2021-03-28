using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBeingSick
{
    void gettingSick();
    void Cured();
    void Quarantine();
    void spawnVirus();
}
