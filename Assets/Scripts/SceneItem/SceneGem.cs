using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneGem : SceneItem,ISaleable
{
    public void Sale(out Gem gem)
    {
        gem = curItem as Gem;
        ItemReset();
        home.Return(this.gameObject);
    }
}
