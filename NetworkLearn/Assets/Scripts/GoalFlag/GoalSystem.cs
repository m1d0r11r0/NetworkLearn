using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class GoalSystem : MonoBehaviour
{
    public GameObject GoalText;

    private void Start()
    {
        GoalText.SetActive(false);
    }

    private void Update()
    {
        if(GoalText.activeSelf)
        {
            //�Q�[���𒆒f
            Time.timeScale = 0f;

            //�C�ӂ̃L�[�ŃQ�[���I��
            if(Input.anyKeyDown)
            {
                QuitGame();
            }
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag=="Player")
        {
            GoalText.SetActive(true);
        }
    }

    private void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}
