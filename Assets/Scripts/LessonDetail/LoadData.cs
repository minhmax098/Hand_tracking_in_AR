using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LessonDetail {
    public class LoadData : MonoBehaviour
    {
        public TextAsset jsonFileModel;
        // Start is called before the first frame update
        
        void Start()
        {
            GetModelById(LessonManager.lesson.modelId);
            GetModelObject();
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public void GetModelById (int modelId) {
            Models modelsJson = JsonUtility.FromJson<Models>(jsonFileModel.text);
            foreach (Model model in modelsJson.models) {
                if (model.id == modelId) {
                    ModelManager.InitModelData(model);
                }
            }
        }

        public void GetModelObject () {
            // cho ni no ko lay dc nè
            GameObject objectModel = Instantiate(Resources.Load(ModelManager.model.model_file_path) as GameObject);
            objectModel.tag = TagConfig.modelTag;
            // GameObject a = objectModel.transform.GetChild(0).gameObject;
            // foreach (Transform eachChild in a.transform) {
            //         Debug.Log("Each child name:" + eachChild.name);
            // }
            // Debug.Log("DONEEEEEEEEEE");
            ObjectManager.InitModel(objectModel);
        }
    }
}
