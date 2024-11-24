using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class TypewriterEffect : MonoBehaviour
{
    private TMP_Text _textBox;

    private int _currentVisibleCharacterIndex;
    private Coroutine _typewriterCoroutine;

    private WaitForSeconds _simpleDelay;
    private WaitForSeconds _interpunctuationDelay;

    [Header("Typewriter Settings")] 
    [SerializeField] private float charactersPerSecond = 20f;
    [SerializeField] private float interpunctuationDelay = 0.5f;
    [SerializeField] private float initialDelay;

    private void Awake()
    {
        _textBox = GetComponent<TMP_Text>();
        
        _simpleDelay = new WaitForSeconds(1f / charactersPerSecond);
        _interpunctuationDelay = new WaitForSeconds(interpunctuationDelay);
    }

    private void Start()
    {
        SetText();
    }

    private void SetText()
    {
        if(_typewriterCoroutine != null)
            StopCoroutine(_typewriterCoroutine);
        
        _textBox.maxVisibleCharacters = 0;
        _currentVisibleCharacterIndex = 0;
        
        _typewriterCoroutine = StartCoroutine(Typewriter());
    }

    private IEnumerator Typewriter()
    {
        yield return StartCoroutine(InitialDelay());
        TMP_TextInfo textInfo = _textBox.textInfo;
        
        while(_currentVisibleCharacterIndex < textInfo.characterCount + 1)
        {
            if (_currentVisibleCharacterIndex >= textInfo.characterInfo.Length)
                yield break;
            char character = textInfo.characterInfo[_currentVisibleCharacterIndex].character;
            _textBox.maxVisibleCharacters++;
            
            if (character == '.' || character == '!')
                yield return _interpunctuationDelay;
            else
                yield return _simpleDelay;
            
            _currentVisibleCharacterIndex++;
        }
    }
    
    private IEnumerator InitialDelay()
    {
        yield return new WaitForSeconds(initialDelay);
    }
}