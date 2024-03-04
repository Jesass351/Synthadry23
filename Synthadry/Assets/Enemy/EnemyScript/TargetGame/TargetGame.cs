using TMPro;
using UnityEngine;

public class TargetGame : MonoBehaviour
{
    [SerializeField] private int counter;
    [SerializeField] private TextMeshProUGUI ui;

    public void Increase(int points)
    {
        counter += points;
        ui.text = counter.ToString();
    }
}
