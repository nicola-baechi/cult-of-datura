using TMPro;
using UnityEngine;

public class Caption : MonoBehaviour
{
    public string[] noHitCaptions;
    public string[] onceHitCaptions;
    public string[] twiceHitCaptions;

    private GameObject _gameManager;

    private TMP_Text _textField;

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.FindGameObjectWithTag("GameManager");
        _textField = GetComponent<TMP_Text>();
        _textField.text = evaluateCaption();
    }

    private string evaluateCaption()
    {
        int hitCount = _gameManager.GetComponent<GameManager>().GetPlayerHitAmount();
        Debug.Log("hitcount:" + hitCount);
        return hitCount switch
        {
            0 => noHitCaptions[Random.Range(0, noHitCaptions.Length)],
            1 => onceHitCaptions[Random.Range(0, onceHitCaptions.Length)],
            2 => twiceHitCaptions[Random.Range(0, twiceHitCaptions.Length)],
            _ => "Error"
        };
    }
}