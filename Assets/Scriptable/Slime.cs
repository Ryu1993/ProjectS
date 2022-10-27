using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName =("Item/Slime"))]
public class Slime : Item
{
    public Crop likeFeed;
    public float hungry;
    public float speed;
    public float jumpPower;
    [SerializeField]
    private Gem _rewardGem;
    public Gem rewardGem { get { return _rewardGem; }}
    public bool isHat;
    public Mesh hatMesh;
    public Material hatMaterial;

}
