using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTile : SafeTile {

    public override void OnDiscoPeteLanded(DiscoPeteBehaviour pete)
    { Debug.Log("YOU HAVE WON!"); }
}
