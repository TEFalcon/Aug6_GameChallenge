using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMesh;

    public void ChageScoreUI(int num)
    {
        textMesh.text = num.ToString();
    }
}
