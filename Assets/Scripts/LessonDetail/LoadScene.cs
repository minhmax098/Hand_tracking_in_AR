using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LessonDetail {
    public class LoadScene : MonoBehaviour
    {
        private static LoadScene instance;
        public static LoadScene Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<LoadScene>();
                }
                return instance;
            }   
        }

        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }

}