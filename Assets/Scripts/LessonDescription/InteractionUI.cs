using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

namespace LessonDescription {
    public class InteractionUI : MonoBehaviour
    {
        private GameObject backToHomeBtn;
        private GameObject startLessonBtn;
        // Start is called before the first frame update
        void Start()
        {
            InitUI();
            SetActions();
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        void InitUI()
        {
            backToHomeBtn = GameObject.Find("BackBtn");
            startLessonBtn = GameObject.Find("StartLessonBtn");
        }

        void SetActions()
        {
            backToHomeBtn.GetComponent<Button>().onClick.AddListener(backToHome);
            startLessonBtn.GetComponent<Button>().onClick.AddListener(startLesson);
        }

        void backToHome()
        {
            SceneManager.LoadScene(SceneConfig.home);
        }

        void startLesson() {
            SceneManager.LoadScene(SceneConfig.lesson);
        }
    }

}
