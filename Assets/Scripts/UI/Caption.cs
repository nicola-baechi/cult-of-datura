using System.Collections;
using System.Collections.Generic;
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
        _textField.text = evaluateCaption();
    }

    private string evaluateCaption()
    {
        int hitCount = _gameManager.GetComponent<GameManager>().GetPlayerHitAmount();
        switch(hitCount)
        {
            case 0:
                return noHitCaptions[Random.Range(0, noHitCaptions.Length)];
            case 1:
                return onceHitCaptions[Random.Range(0, onceHitCaptions.Length)];
            case 2:
                return twiceHitCaptions[Random.Range(0, twiceHitCaptions.Length)];
            default:
                return "Error";
        }
    }
}