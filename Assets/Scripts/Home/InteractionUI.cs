using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

namespace Home {
    public class InteractionUI : MonoBehaviour
    {
        private static InteractionUI instance;
        public static InteractionUI Instance 
        {
            get {
                if (instance == null) {
                    instance = FindObjectOfType<InteractionUI>();
                }
                return instance;
            }
        }


        public void onClickItemLesson(Lesson lesson)
        {
            LessonManager.InitLesson(lesson);
            SceneManager.LoadScene(SceneConfig.lessonDescription);
        }
    }

    
}
