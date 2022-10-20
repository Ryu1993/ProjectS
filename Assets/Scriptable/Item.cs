using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Item : ScriptableObject
{
    [SerializeField]
    private string _itemName;
    public string itemName { get { return _itemName; } private set { } }
    [SerializeField]
    private Sprite _itemSprite;
    public Sprite itemSprite { get { return _itemSprite; } private set { } }
    [SerializeField]
    private Mesh _itemMesh;
    public Mesh itemMesh { get { return _itemMesh; } private set { } }
    [SerializeField]
    private Material _itemMaterial;
    public Material itemMaterilal { get { return _itemMaterial; } private set { } }

}
