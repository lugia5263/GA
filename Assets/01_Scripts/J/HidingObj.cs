using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class HidingObj : MonoBehaviour
{
    HidingObj hidingObj;
    static Dictionary<Collider, HidingObj> hidingObjTarget = new Dictionary<Collider, HidingObj>();
    [SerializeField]
    public GameObject render;
    public Collider col = null;
    void Start()
    {
        render = this.gameObject;
        col = GetComponent<Collider>();
        InitObj();
    }

    public static void InitObj()
    {
        foreach(var obj in hidingObjTarget.Values)
        {
            if(obj != null && obj.col != null)
            {
                obj.SetVisible(true);
                obj.hidingObj = null;
            }
        }
        hidingObjTarget.Clear();

        foreach(var obj in FindObjectsOfType<HidingObj>())
        {
            if(obj.col != null)
            {
                hidingObjTarget[obj.col] = obj;
            }
        }    
    }

    public static HidingObj GetRootHidingByCollider(Collider collider)
    {
        HidingObj obj;
        if (hidingObjTarget.TryGetValue(collider, out obj))
            return GetRoot(obj);
        else
            return null;
    }
    static HidingObj GetRoot(HidingObj obj)
    {
        if (obj.hidingObj == null)
            return obj;
        else
            return GetRoot(obj.hidingObj);
    }

    public void SetVisible(bool visible)
    {
        Renderer rend = render.GetComponent<Renderer>();
        if(rend != null && rend.gameObject.activeInHierarchy && hidingObjTarget.ContainsKey(rend.GetComponent<Collider>()))
        {
            rend.shadowCastingMode = visible ? ShadowCastingMode.On : ShadowCastingMode.ShadowsOnly;
        }
    }
    void Update()
    {
        
    }
}
