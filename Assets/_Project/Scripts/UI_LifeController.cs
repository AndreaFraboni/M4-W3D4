using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_LifeController : MonoBehaviour
{
    [SerializeField] private Image _bar_lifeBarFillable;
    [SerializeField] private TextMeshProUGUI _lifeText;

    [SerializeField] private Image _gameOverPanel;

    public void OnChangeLife(int hp, int maxhp)
    {
        _lifeText.text = hp + "/" + maxhp;
        _bar_lifeBarFillable.fillAmount = (float)hp / maxhp;
    }

    public void ShowGameOverPanel()
    {
        _gameOverPanel.gameObject.SetActive(true);
    }

}
