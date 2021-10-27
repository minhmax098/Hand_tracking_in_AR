using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LeftPanelHandler : MonoBehaviour
{
    [SerializeField]
    Animator statusAnimator;
    private GameObject meetingManager;
    void Start()
    {
        InitUI();
        InitEvent();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitUI() 
    {
        meetingManager = GameObject.Find("MeetingManager");
    }
    
    void InitEvent() 
    {
        meetingManager.GetComponent<Button>().onClick.AddListener(LoadMeetingJoiningRoom);
    }

    void LoadMeetingJoiningRoom() 
    {
        SceneManager.LoadScene(SceneConfig.meetingJoin);
    }

    public void ShowLeftPanel() 
    {
        statusAnimator.SetBool(AnimatorConfig.showLeftPanel, true);
    }

    public void HideLeftPanel() 
    {
        statusAnimator.SetBool(AnimatorConfig.showLeftPanel, false);
    }
}
