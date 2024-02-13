using TMPro;
using UnityEngine;

public class BasketHoop : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    private int _score = 0;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Scored!");
        _score++;
        scoreText.text = $"Score: {_score}";
    }
}