using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Json;
using System.Xml;
using System.Text;
using Emmy; 

namespace Home {
    public class LoadData : MonoBehaviour
    {
        public TextAsset jsonFileCategoryWithLesson;
        public TextAsset jsonFileCategory;
        private static LoadData instance;
        public static LoadData Instance
        {
            get {
                if (instance == null) {
                    instance = FindObjectOfType<LoadData>();
                }
                return instance;
            }
        }

        void Start()
        {
            
        }


        public Lessons GetLessonByCategory(string category_id){
            LoadJsonFromWeb api_loader = GameObject.Find("API").GetComponent<LoadJsonFromWeb>();
            string json_result = api_loader.GetListLessonByCategory(category_id);
            if (json_result != ""){
                return JsonUtility.FromJson<Lessons>(json_result);
            }
            else return null;
        }

        public Categories GetCategories() {
            // Dung ham lay json tu web ve (cs - > )Jos
            LoadJsonFromWeb api_loader = GameObject.Find("API").GetComponent<LoadJsonFromWeb>();
            return JsonUtility.FromJson<Categories>(api_loader.GetCategoryJSON());
            // return JsonUtility.FromJson<Categories>(jsonFileCategory.text);
        }

    }

}