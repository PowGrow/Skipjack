using System;
using System.Collections.Generic;
using UnityEngine;
public interface IStrufeFactory
{
    List<Strufe> Strufes { get; }
    float StrufeWidth { get; }
    void PlaceStrufe(GameObject strufe);
}
