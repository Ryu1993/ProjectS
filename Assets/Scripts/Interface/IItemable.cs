using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItemable
{
    public Item ItemRequest();
    public void ItemReturn();

}
