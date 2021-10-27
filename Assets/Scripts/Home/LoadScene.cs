using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Emmy; 
// vấn đề là ở đây, thêm using Emmy vô rồi, dưới cũng đang lỗi

namespace Home {
    public class LoadScene : MonoBehaviour
    {
        public GameObject contentItemCategoryWithLesson;
        public GameObject contentItemCategory;

        // Start is called before the first frame update
        void Start()
        {
            LoadCategories();
            LessonByCategory();
        }

        // Update is called once per frame
        void Update()
        {
            
        }
        void LoadCategories() 
        {
            // foreach (Category category in LoadData.Instance.GetCategories().data)
            foreach (Category category in LoadData.Instance.GetCategories().data)
            {
                GameObject categoryObject = Instantiate(Resources.Load(DemoConfig.demoItemCategoryPath) as GameObject);
                categoryObject.name = category.id.ToString();
                categoryObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = category.name.ToUpper();
                categoryObject.transform.parent = contentItemCategory.transform;
            }
        }
        void LessonByCategory() {

            foreach(Category category in LoadData.Instance.GetCategories().data)
            {
                string category_id = category.id;
                GameObject itemCategoryObject = Instantiate(Resources.Load(DemoConfig.demoItemCategoryWithLessonPath) as GameObject);
                itemCategoryObject.name = category.id.ToString();
                itemCategoryObject.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Text>().text = category.name.ToUpper();
                itemCategoryObject.transform.parent = contentItemCategoryWithLesson.transform;
                GameObject subContent = itemCategoryObject.transform.GetChild(1).GetChild(0).GetChild(0).gameObject;
                var response = LoadData.Instance.GetLessonByCategory(category_id);
                if(response != null){
                    foreach (Lesson lesson in LoadData.Instance.GetLessonByCategory(category_id).data){
                        LoadLessons(subContent, lesson);
                    }
                }
            }


            // foreach (CategorywithLesson category in LoadData.Instance.GetCategoryWithLesson().categorywithLessons)
            // {
            // GameObject itemCategoryObject = Instantiate(Resources.Load(DemoConfig.demoItemCategoryWithLessonPath) as GameObject);
            // itemCategoryObject.name = category.id.ToString();
            // itemCategoryObject.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Text>().text = category.name.ToUpper();
            // itemCategoryObject.transform.parent = contentItemCategoryWithLesson.transform;
            // GameObject subContent = itemCategoryObject.transform.GetChild(1).GetChild(0).GetChild(0).gameObject;
            //     foreach (Lesson lesson in category.lessons)
            //     {
            //         LoadLessons(subContent, lesson);
            //     }
            // }
        }

        void LoadLessons(GameObject parentObject, Lesson lesson) 
        {
            GameObject lessonObject = Instantiate(Resources.Load(DemoConfig.demoLessonObjectPath) as GameObject);
            lessonObject.name = lesson.id.ToString();
            lessonObject.transform.GetChild(1).GetChild(0).gameObject.GetComponent<Text>().text = lesson.name.ToUpper(); 
            lessonObject.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(lesson.thumbnailFileId);
            lessonObject.transform.parent = parentObject.transform;
            
            Button lessonBtn = lessonObject.GetComponent<Button>();
            lessonBtn.onClick.AddListener(() => InteractionUI.Instance.onClickItemLesson(lesson));
        }
    }

}