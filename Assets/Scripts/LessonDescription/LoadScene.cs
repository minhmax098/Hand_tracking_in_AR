using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LessonDescription {
    public class LoadScene : MonoBehaviour
    {
        private GameObject lessonModel;
        private GameObject lessonDescription;
        // Start is called before the first frame update
        void Start()
        {
            InitUI();
            LoadLessonModel();
            LoadLessonDescription();
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        void InitUI() {
            lessonModel = GameObject.Find("LessonModel");
            lessonDescription = GameObject.Find("LessonDescription");
        }
    
        void LoadLessonModel() {
            lessonModel.transform.GetChild(0).gameObject.GetComponent<Text>().text = LessonManager.lesson.name;
            lessonModel.transform.GetChild(1).gameObject.GetComponent<Image>().sprite =  Resources.Load<Sprite>(LessonManager.lesson.thumbnailFileId);
        }

        void LoadLessonDescription() {
            lessonDescription.transform.GetChild(1).GetChild(1).gameObject.GetComponent<Text>().text = LessonManager.lesson.description;
        }
    }

}
