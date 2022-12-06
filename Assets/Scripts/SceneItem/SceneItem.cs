using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneItem : MonoBehaviour,IInteraction,IPoolingable,IItemable
{
    protected MeshRenderer m_Renderer;
    protected MeshFilter m_Filter;
    protected Rigidbody m_Rigidbody;
    protected Item curItem;
    public ObjectPool home { get; set; }
    public Rigidbody rigi { get; set; }

    protected void Awake()
    {
        m_Renderer = GetComponent<MeshRenderer>();
        m_Filter = GetComponent<MeshFilter>();
        rigi = GetComponent<Rigidbody>();
    }
    public void ItemSetting(Item item)
    {
        if (item as Slime != null) return;
        Debug.Log(item);
        curItem = item;
        m_Filter.mesh = item.itemMesh;
        m_Renderer.material = item.itemMaterilal;
    }
    protected virtual void ItemReset()
    {
        rigi.velocity = Vector3.zero;
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.Euler(Vector3.zero);
        curItem = null;
        m_Filter.mesh = null;
        m_Renderer.material = null;
    }

    public Item ItemRequest()
    {
        return curItem;
    }
    public void ItemReturn()
    {
        ItemReset();
        home.Return(this.gameObject);
    }

    public void MoveStop()
    {
        
    }
}
