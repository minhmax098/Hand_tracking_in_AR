
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManagement : MonoBehaviour
{
    Touch touch;
    // Start is called before the first frame update
    GameObject objectSelected = null;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 1)
        {
            touch = Input.GetTouch(0);
            if (touch.tapCount == 2 && touch.phase == TouchPhase.Ended)
            {
                SelectObject();
            }
        }
    }

    void SelectObject()
    {
        GameObject iconTheEnd = GameObject.Find("IconTheEnd");
        if (iconTheEnd != null) return;

        objectSelected = isSelectedObject(touch.position);
        if (objectSelected != null)
        {
            GameObject objectParent = objectSelected.transform.parent.gameObject;
            int childCountOfParent = objectParent.transform.childCount;
            int indexObjectSeleted = objectSelected.transform.GetSiblingIndex();
       
            if (childCountOfParent > 1) {
                for (int i=0; i < childCountOfParent; i++) {
                    if (i == indexObjectSeleted) continue;
                    objectParent.transform.GetChild(i).gameObject.SetActive(false);
                }
            } 

            StartCoroutine(InteractObject.MoveObject(CameraManager.Instance.currentCamera.gameObject, objectSelected.transform.position + CameraConfig.offset));
            bool haveSubModel = objectSelected.transform.childCount > 0;
            ChildNodeManager.Instance.CreateChildNode(objectSelected.name, haveSubModel);
        }
    }

    GameObject isSelectedObject(Vector3 position)
    {
        Ray ray = Camera.main.ScreenPointToRay(position);
        RaycastHit[] raycastHits;
        raycastHits = Physics.RaycastAll(ray);
        foreach (RaycastHit hit in raycastHits)
        {
            if (hit.collider.tag == TagConfig.subModel)
            {
                return hit.collider.gameObject;
            }
        }
        return null;
    }
}
