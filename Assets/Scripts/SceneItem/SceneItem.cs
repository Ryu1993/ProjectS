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

    protected void Awake()
    {
        m_Renderer = GetComponent<MeshRenderer>();
        m_Filter = GetComponent<MeshFilter>();
        m_Rigidbody = GetComponent<Rigidbody>();
    }
    public void ItemSetting(Item item)
    {
        if (item as Slime != null) return;
        Debug.Log(item);
        curItem = item;
        m_Filter.mesh = item.itemMesh;
        m_Renderer.material = item.itemMaterilal;
    }
    protected void ItemReset()
    {
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

}
