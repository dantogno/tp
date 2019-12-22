using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class AfterActionReview : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI rankText;

    private void Start()
    {
        rankText.text = Ranking.GetRankText();
    }

    public void LoadCreditsScene()
    {
        SceneManager.LoadScene("creditsScene");
    }
}
