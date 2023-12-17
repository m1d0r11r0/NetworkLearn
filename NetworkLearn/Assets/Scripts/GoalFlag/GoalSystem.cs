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
            //ÉQÅ[ÉÄÇíÜíf
            Time.timeScale = 0f;

            //îCà”ÇÃÉLÅ[Ç≈ÉQÅ[ÉÄèIóπ
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
