using UnityEngine;

public class Bush : MonoBehaviour
{
    [SerializeField]
    private GameObject hiddenObject;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.gameObject.CompareTag("Player"))
        {
            ReduceBushOpacity();
            if(hiddenObject == null) return;
            
            Instantiate(hiddenObject, transform.position, Quaternion.identity);
        }
    }
    
    private void ReduceBushOpacity()
    {
        var spriteRenderer = GetComponent<SpriteRenderer>();
        var color = spriteRenderer.color;
        color.a = 0.25f;
        spriteRenderer.color = color;
    }
}
