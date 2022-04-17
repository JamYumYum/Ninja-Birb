using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreText : MonoBehaviour
{
    [Tooltip("Needs the a Script implementing IMeasureScore.")]
    public GameObject containsMeasureScore;

    private TextMeshProUGUI scoreText;
    private IMeasureScore scoreMeasure;
    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
        scoreMeasure = containsMeasureScore.GetComponent<IMeasureScore>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = $"{scoreMeasure.score.ToString("n2")}m";
    }
}
