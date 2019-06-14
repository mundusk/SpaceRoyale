using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreUIController : MonoBehaviour
{
    Transform highScoreContainer;
    Transform highScoreTemplate;

    void Awake()
    {
        highScoreContainer = GameObject.Find("HighScoreContainer").transform;
        highScoreTemplate = GameObject.Find("HighScoreTemplate").transform;

        highScoreTemplate.gameObject.SetActive(false);

        float templateHeight = 40f;

        //TODO: Debug why 1st place doesn't change
        for(int i = 0; i < 10; i++)
        {
            Transform highScoreTransform = Instantiate(highScoreTemplate, highScoreContainer);
            RectTransform highScoreRectTransform = highScoreTransform.GetComponent<RectTransform>();
            highScoreRectTransform.anchoredPosition = new Vector2(0, -templateHeight * i);

            highScoreTemplate.gameObject.SetActive(true);

            int rank = i + 1;
            string rankString;

            switch(rank)
            {
                case 1: rankString = "1ST";
                        break;
                case 2: rankString = "2ND";
                        break;
                case 3: rankString = "3RD";
                        break;
                default:
                        rankString = rank.ToString() + "TH";
                        break;
            }

            highScoreTransform.Find("PositionText").GetComponent<Text>().text = rankString;
            highScoreTransform.Find("ScoreText").GetComponent<Text>().text = "";
            highScoreTransform.Find("NameText").GetComponent<Text>().text = "";
        }
    }
}
