using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems; 
using UnityEngine.Events; 
using TMPro; 

namespace LessonDetail {
    // [RequireComponent (typeof (UI.Text))]
    public class InteractionUI : MonoBehaviour
    {
        private GameObject listSubOrgan; 

        private GameObject separateBtn;
        private GameObject quitBtn;
        private GameObject infoBtn;
        private GameObject xRayBtn;
        private GameObject ARViewBtn;

        private GameObject labelBtn; 
        private GameObject animationBtn; 
        private GameObject pickupBtn; 

        // infoBtn 
        private GameObject info2Btn; 

        private bool isPress = false; 
        private bool isPressBtnInfo = false; 
        private bool isOpenMenu = false; 
        private bool isListSubNameOrgan = false; 

        private Touch touch;
        private float speedModifier;


        //My solution List gameobject, disable it
        //List<string> labelObjects = new List<string>();
        //List<Vector3> centersOfMass = new List<Vector3>();
        private GameObject activeObject;
        List<GameObject> labelObjects = new List<GameObject>();

        private List<Material> materials = new List<Material>(); 
        // Start is called before the first frame update
        void Start()
        {
            activeObject = GameObject.Find("Skeletal");
            //TODO: Not sure it usage
            //activeObject.tag = TagConfig.modelTag;
            speedModifier = 0.01f;
            InitUI();
            SetActions();
            CreateLabel();
            ShowChildLabel(false);
            SeparateObject(false);
        }

        // Update is called once per frame
        void Update()
        {
            // if(OrganManager.IsRotating)
            // {
            //     GameObject.Find("Gyroscope").transform.eulerAngles = new Vector3(
            //         GameObject.Find("Gyroscope").transform.eulerAngles[0], 
            //         GameObject.Find("Gyroscope").transform.eulerAngles[1] + 0.2f, 
            //         GameObject.Find("Gyroscope").transform.eulerAngles[2]
            //     );
            // }
            // if(TagHandler.Instance.addedTags.Count > 0)
            // {
            //     TagHandler.Instance.OnMove(); 
            // }
        
            if (Input.GetMouseButtonDown(0)){
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                Debug.Log(Input.touchCount);
                // TODO: Fix to work in emulator touchCount always 0
                // Khắc phục để khởi tạo trong trình giả lập luôn luôn bằng 0 
                if (Input.touchCount > 0 && Physics.Raycast(ray, out hit)) { 
                    Debug.Log(hit.transform.name);
                    // Change the color 
                    //hit.collider.GetComponent<Renderer>().material.color = Color.yellow;
                    touch = Input.GetTouch(0);
                    if (touch.phase == TouchPhase.Moved){
                        hit.transform.gameObject.transform.position = new Vector3(
                            hit.transform.gameObject.transform.position.x + touch.deltaPosition.x * speedModifier,
                            hit.transform.gameObject.transform.position.y,
                            hit.transform.gameObject.transform.position.z + touch.deltaPosition.y * speedModifier
                        );
                    }
                    
                }
            }

            // Khong on 
            // UnityEvent OnLongClick;
            // bool clicking = false;
            // float totalDownTime = 0;
            // // Detect first click 
            // if (Input.GetMouseButtonDown(0)){
            //     totalDownTime = 0; 
            //     clicking = true; 
            // }
            // // First click detected, still click, measure clicking time 
            // if (clicking && Input.GetMouseButton(0)){
            //     totalDownTime += Time.deltaTime;
            //     if (totalDownTime >= 1f){
            //         Debug.Log("Long click");
            //         clicking = false;
            //         // OnLongClick.Invoke();
            //     }
            // }

            // if (clicking && Input.GetMouseButtonUp(0)){
            //     clicking=false;
            //     Debug.Log("Short click");
            // }
            if ( ObjectManager.IsRotating){
                ObjectManager.CurrentModelObject.transform.Rotate(0, 1, 0);
            }
            AdjustLabelToCamera();
        }

        void InitUI() {
            quitBtn = GameObject.Find("QuitBtn");
            separateBtn = GameObject.Find("SeparationBtn");
            infoBtn = GameObject.Find("InfoBtn");
            xRayBtn = GameObject.Find("XRayBtn");
            ARViewBtn = GameObject.Find("ARMode");

            labelBtn = GameObject.Find("LabelBtn"); 
            animationBtn = GameObject.Find("AnimationBtn"); 
            pickupBtn = GameObject.Find("PickupBtn"); 
            
            // infoBtn 
            info2Btn = GameObject.Find("Info2Btn"); 
        }

        void SetActions()
        {
            quitBtn.GetComponent<Button>().onClick.AddListener(quitLesson);
            // btn.onClick.AddListener(() => { Function(param); OtherFunction(param); });
            separateBtn.GetComponent<Button>().onClick.AddListener(() => {SeparateObject(ObjectManager.IsSeparate);});
            infoBtn.GetComponent<Button>().onClick.AddListener(loadObjectAgain);
            xRayBtn.GetComponent<Button>().onClick.AddListener(changeMaterial);
            ARViewBtn.GetComponent<Button>().onClick.AddListener(loadViewAR);

            labelBtn.GetComponent<Button>().onClick.AddListener(showLabel); 
            animationBtn.GetComponent<Button>().onClick.AddListener(showAnimation); 
            pickupBtn.GetComponent<Button>().onClick.AddListener(showPickup);

            info2Btn.GetComponent<Button>().onClick.AddListener(show2Info); 
        }

        void quitLesson()
        {
            SceneManager.LoadScene(SceneConfig.home);
        }


        void separateModel()
        {
            ObjectManager.IsSeparate = !ObjectManager.IsSeparate;
            int childCount = ObjectManager.CurrentModelObject.transform.childCount;
            // Debug.Log("Child Count: " + childCount);
            if (ObjectManager.IsSeparate) {
                separateBtn.GetComponent<Image>().sprite = Resources.Load<Sprite>(SpriteConfig.btnSeparateClick);
                Vector3 originObject = ObjectManager.CurrentModelObject.transform.localPosition;
                // Debug.Log("x of origin: " + originObject.x);
                // foreach (Transform eachChild in ObjectManager.CurrentModelObject.transform) {
                //     Debug.Log("Each child name:" + eachChild.name);
                // }
                // Console.Log("childCount");
                // childCount = 2
                if (childCount > 1) {
                    for (int i = 0; i < childCount; i++) 
                    {
                        if (i%2 == 0)
                        {
                            //.gameObject.transform
                            ObjectManager.CurrentModelObject.transform.GetChild(i).gameObject.transform.position = new Vector3(
                                20, 
                                10, 
                                10);
                                // ObjectManager.CurrentModelObject.transform.GetChild(i).parent = null;
                                // Debug.Log("Fist child x: " + ObjectManager.CurrentModelObject.transform.GetChild(i).localPosition.x);
                        }
                        else
                        {
                            // No bien ca 2 thang theo cai nay
                            ObjectManager.CurrentModelObject.transform.GetChild(i).gameObject.transform.position = new Vector3(
                                -30, 
                                -10, 
                                -10);
                                // Debug.Log("Second child x: " + ObjectManager.CurrentModelObject.transform.GetChild(i).localPosition.x);
                        }
                    }
                }
                // Debug.Log(GameObject.Find("Spinal").transform.position);
                // Debug.Log(GameObject.Find("Thoracic Cage").transform.position);
                
            }
            else 
            {
                separateBtn.GetComponent<Image>().sprite = Resources.Load<Sprite>(SpriteConfig.btnSeparateAvailable);
                if (childCount > 1) {
                    for (int i=1; i < childCount; i++) {
                        // ObjectManager.CurrentModelObject.transform.GetChild(i).gameObject.transform.position = new Vector3(0,0,0);
                        ObjectManager.CurrentModelObject.transform.GetChild(i).gameObject.transform.localPosition = new Vector3(0,0,0);
                        ObjectManager.CurrentModelObject.transform.GetChild(i).gameObject.transform.localRotation = Quaternion.identity;

                    }
                }
            }
        }

        void loadObjectAgain() {
            int childCountOfParent = ObjectManager.CurrentModelObject.transform.childCount;
            if (childCountOfParent > 1) {
                for (int i=0; i < childCountOfParent; i++) {
                    ObjectManager.CurrentModelObject.transform.GetChild(i).gameObject.SetActive(true);
                }
            }
            
            GameObject[] objs = GameObject.FindGameObjectsWithTag(TagConfig.childNode);
            foreach( GameObject obj in objs)
            {
                Destroy(obj);
            }
        }

        void changeMaterial()
        {
            ObjectManager.IsXRay = !ObjectManager.IsXRay;
            int childCount = ObjectManager.CurrentModelObject.transform.childCount;

            if (ObjectManager.IsXRay) {
                xRayBtn.GetComponent<Image>().sprite = Resources.Load<Sprite>(SpriteConfig.btnXrayClick);
                for (int i=0; i < childCount; i++) {
                    materials.Add(ObjectManager.CurrentModelObject.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().material);
                    ObjectManager.CurrentModelObject.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().material = Resources.Load(
                        MaterialConfig.materialTransparency, typeof(Material)) as Material;
                }
            } else {
                xRayBtn.GetComponent<Image>().sprite = Resources.Load<Sprite>(SpriteConfig.btnXrayAvailable);
                for (int i=0; i < childCount; i++) {
                    ObjectManager.CurrentModelObject.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().material = materials[i] as Material;
                }
            }
        }

        void loadViewAR()
        {
            MeetingManager.isHost = true;
            SceneManager.LoadScene(SceneConfig.ARView);
        }


        // WORKING WITH OBJECT LABELS
        void CreateLabel(){
            // Create Label and Create Center of each child
            // Active object, main object Skeletal Change later, current use it locally
            // activeObject is global
            // TODO: 
            int childCount = ObjectManager.CurrentModelObject.transform.childCount;
            for (int i = 0; i < childCount; i++)
            {
                GameObject childObject = ObjectManager.CurrentModelObject.transform.GetChild(i).gameObject;
                // Ăn thua là cái chỗ này rồi
                GameObject labelObject = Instantiate(Resources.Load(PathConfig.labelPath, typeof(GameObject)) as GameObject);
                labelObject.tag = TagConfig.labelModel; // labelObject.tag = "LabelModel"
                labelObject.transform.SetParent(childObject.transform, false);
                SetLabel(childObject, labelObject, i % 2 != 0);
                labelObjects.Add(labelObject);
            }

            // Test
            // foreach (GameObject lb in labelObjects){
            //     Debug.Log("Label " + lb.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.GetComponent<UnityEngine.UI.Text>().text);
            // }
            // Debug.Log("Doneeeeeeeeee");
        }

        void SetLabel(GameObject currentObject, GameObject label, bool isLeft){
            GameObject line = label.transform.GetChild(0).gameObject;
            GameObject labelName = label.transform.GetChild(1).gameObject;
            // pha này nè, ko còn Text đồ nữa, chưa làm label thì M comment nó lại luôn
            // labelName.transform.GetChild(0).gameObject.GetComponent<UnityEngine.UI.Text>().text = currentObject.name;
            labelName.GetComponent<TextMeshPro>().text = currentObject.name; 
            // labelName.GetComponent<UnityEngine.UI.Text>().text = currentObject.name;
            // Debug.Log("To Setlabellllll");
            // Debug.Log(currentObject.name);
            Bounds objectBounds = GetRenderBounds(currentObject);
            // Debug.Log("Object Bounds Size: " + objectBounds.size);
            if (isLeft){
                labelName.transform.localPosition = new Vector3(currentObject.transform.localPosition.x - 0.5f * objectBounds.size.x, currentObject.transform.localPosition.y + 0.5f * objectBounds.size.y, currentObject.transform.localPosition.z);
            }
            else{
                // TODO: Just a quick fix, need more effort ?!!!!!!
                labelName.transform.localPosition = new Vector3(currentObject.transform.localPosition.x + 0.3f * objectBounds.size.x, currentObject.transform.localPosition.y + 0.3f * objectBounds.size.y, currentObject.transform.localPosition.z);
            }
            line.GetComponent<LineRenderer>().SetVertexCount(2);
            line.GetComponent<LineRenderer>().SetPosition(0, currentObject.transform.localPosition);
            line.GetComponent<LineRenderer>().SetPosition(1, labelName.transform.localPosition);
        }

        public void ShowChildLabel(bool isShowingLabel){
            foreach (GameObject label in labelObjects){
                label.SetActive(isShowingLabel);
                //label.transform.GetChild(1).gameObject.SetActive(true);
                // label.transform.GetChild(0).gameObject.SetActive(true);
            }
        }

        void showLabel(){
            // The heck isPress
            isPress = !isPress; 
            if (isPress){
                // Put createlabel here just for debug purpose
                //CreateLabel();
                ShowChildLabel(true);
            }
            else{
                ShowChildLabel(false);
            }
        }
        
        public static Bounds GetRenderBounds(GameObject go){
            var totalBounds = new Bounds();
            totalBounds.SetMinMax(Vector3.one * Mathf.Infinity, -Vector3.one * Mathf.Infinity);
            foreach (var renderer in go.GetComponentsInChildren<Renderer>()){
                var bounds = renderer.bounds;
                var totalMin = totalBounds.min;
                totalMin.x = Mathf.Min(totalMin.x, bounds.min.x);
                totalMin.y = Mathf.Min(totalMin.y, bounds.min.y);
                totalMin.z = Mathf.Min(totalMin.z, bounds.min.z);

                var totalMax = totalBounds.max;
                totalMax.x = Mathf.Max(totalMax.x, bounds.max.x);
                totalMax.y = Mathf.Max(totalMax.y, bounds.max.y);
                totalMax.z = Mathf.Max(totalMax.z, bounds.max.z);

                totalBounds.SetMinMax(totalMin, totalMax);
            }
            return totalBounds;
        }
        
        void AdjustLabelToCamera(){
            foreach (GameObject label in labelObjects){
                GameObject labelName = label.transform.GetChild(1).gameObject;
                labelName.transform.LookAt(labelName.transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);
            }
        }

        public void SeparateObject(bool isSeparating){
            ObjectManager.IsSeparate = !ObjectManager.IsSeparate;
            int childCount = ObjectManager.CurrentModelObject.transform.childCount;
            if (childCount <= 1){
                return;
            }
            if (isSeparating){
                Vector3 originPosition = ObjectManager.CurrentModelObject.transform.localPosition;
                    for (int i = 0; i < childCount; i++){
                        Vector3 newPosition = new Vector3(originPosition.x + 30 * i, originPosition.y, originPosition.z);
                        //StartCoroutine(Common.MoveObject(ObjectManager.CurrentModelObject.transform.GetChild(i).gameObject, newPosition));
                        ObjectManager.CurrentModelObject.transform.GetChild(i).gameObject.transform.position = newPosition;

                }
            }

            else{
                for (int i = 0; i < childCount; i++){
                    ObjectManager.CurrentModelObject.transform.GetChild(i).gameObject.transform.localPosition = new Vector3(0, 0, 0);
                    ObjectManager.CurrentModelObject.transform.GetChild(i).gameObject.transform.localRotation = Quaternion.identity;
                }
            }
        }

        void exiListSubOrgan()
        {
            listSubOrgan.SetActive(false);
        }

        void showAnimation()
        {
            // isPress = !isPress; 
            // if (isPress)
            // {
            //     animationBtn.GetComponent<Image>().color = new Color32(111, 229, 223, 255); 
            // }
            // else 
            // {
            //     animationBtn.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            // }
            ObjectManager.IsRotating = !ObjectManager.IsRotating;
        //     if(ObjectManager.IsRotating)
        //     {
        //         animationBtn.GetComponent<Image>().color = new Color32(111, 229, 223, 255); 
        //     }
        //     else
        //     {
        //         animationBtn.GetComponent<Image>().color = new Color32(255, 255, 255, 255); 
        //     }
        }

        void showPickup()
        {

        }

        // loadItemSubNameOrgan 
        void loadItemSubNameOrgan()
        {
            isListSubNameOrgan = true; 
            GameObject templateNameOrgan = listSubOrgan.transform.GetChild(0).gameObject; 
            GameObject templateItem = listSubOrgan.transform.GetChild(1).gameObject; 
            GameObject itemInstance; 

            // con nua, nam o day . 
            
        }

        void show2Info()
        {
            // isPress2BtnInfo = !isPress2BtnInfo; 
            // ani = panelInfo.GetComponent<Animator>(); 
            // bool isShow = ani.GetBool("show"); 
            // GameObject contentInfo = panelInfo.GetChild(1).gameObject; 
            // contentInfo.transform.GetChild(0).GetComponent<TMPro.TextMeshProGUI>().text = ObjectManager.DataOrgan.description; 
            // ani.SetShow("show", !isShow); 
            // if(isPress2BtnInfo)
            // {
            //     btn2Info.GetComponent<Image>().color = new Color32(111, 229, 223, 255); 
            // }
            // else
            // {
            //     btn2Info.GetComponent<Image>().color = new Color32(255, 255, 255, 255); 
            // }
        }


    }
}

