using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickUp : PickUpItems {


    public override void OnPickup(Collider collider)
    {

        base.OnPickup(collider);

        GameController.gc.container.Add(itemInfo);



        Destroy(gameObject);
    }
}

