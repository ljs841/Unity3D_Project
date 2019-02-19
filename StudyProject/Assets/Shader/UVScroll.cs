using UnityEngine;

public class UVScroll : MonoBehaviour
{
    // Scroll main texture based on time

    public SpriteRenderer spriteRenderer;
    public float _speedOffset = 0.0f;
    public float _mapWidth = 0.0f;
    Character _char;
    void Start()
    {
        
        _char = BattleManager._Instance.CurrentPlayer;

        Rect rect = FineValue(spriteRenderer.sprite.uv);
        MaterialPropertyBlock block = new MaterialPropertyBlock();
        spriteRenderer.GetPropertyBlock(block);
        block.SetVector("_SpriteRect", new Vector4(rect.x, rect.y, rect.width, rect.height));
        block.SetFloat("_Speed", _char.gameObject.transform.position.x * _speedOffset / _mapWidth);
        spriteRenderer.SetPropertyBlock(block);
        

    }

    private void Update()
    {
        
        
        MaterialPropertyBlock block = new MaterialPropertyBlock();
        spriteRenderer.GetPropertyBlock(block);
        block.SetFloat("_Speed", _char.gameObject.transform.position.x * _speedOffset / _mapWidth);
        spriteRenderer.SetPropertyBlock(block);
        
    }


    Rect FineValue(Vector2[] uv)
    {
        float xMin = 1;
        float xMax = 0;
        float yMin = 1;
        float yMax = 0;

        foreach(var ob in uv)
        {
            if (ob.x <= xMin)
            {
                xMin = ob.x;
            }
            if (ob.x >= xMax)
            {
                xMax = ob.x;
            }
            if (ob.y <= yMin)
            {
                yMin = ob.y;
            }
            if (ob.y >= yMax)
            {
                yMax = ob.y;
            }
        }

        Rect rect = new Rect(xMin, yMax, xMax - xMin, yMax - yMin);
        return rect;
    }

   
}