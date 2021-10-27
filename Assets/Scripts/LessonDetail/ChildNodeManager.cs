using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChildNodeManager : MonoBehaviour
{
    private static ChildNodeManager instance;
    public static ChildNodeManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<ChildNodeManager>();
            }
            return instance;
        }   
    }

    [SerializeField] GameObject childNodePrefab;
    [SerializeField] GameObject rootObject;

    public void CreateChildNode(string nameModel, bool haveSubModel)
    {
        GameObject childNode = Instantiate(childNodePrefab);
        childNode.transform.SetParent(rootObject.transform, false);
        childNode.transform.GetChild(1).GetChild(0).gameObject.GetComponent<Text>().text = nameModel;
        if (haveSubModel) {
            Destroy(childNode.transform.GetChild(2).gameObject);
        }
    }

    public void DestroyChildNode() {
        for (int i = 1; i < rootObject.transform.childCount; i++) {
            Destroy(rootObject.transform.GetChild(i).gameObject);
        }
    }
    GameObject FindRootObject()
    {
        return GameObject.FindWithTag(TagConfig.infoTree);
    }
}
