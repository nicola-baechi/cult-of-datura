using UnityEngine;

public class Bush : MonoBehaviour
{
    [SerializeField]
    private GameObject hiddenObject;

    private GameObject _instance;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.gameObject.CompareTag("Player"))
        {
            ReduceBushOpacity();
            if(hiddenObject == null) return;
            
            if(_instance != null)
            {
                _instance.SetActive(true);
                return;
            }
            _instance = Instantiate(hiddenObject, transform.position, Quaternion.identity);
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            IncreaseBushOpacity();
            if(_instance == null) return;
            
            _instance.SetActive(false);
        }
    }
    
    private void ReduceBushOpacity()
    {
        var spriteRenderer = GetComponent<SpriteRenderer>();
        var color = spriteRenderer.color;
        color.a = 0.25f;
        spriteRenderer.color = color;
    }
    
    private void IncreaseBushOpacity()
    {
        var spriteRenderer = GetComponent<SpriteRenderer>();
        var color = spriteRenderer.color;
        color.a = 1f;
        spriteRenderer.color = color;
    }
}
