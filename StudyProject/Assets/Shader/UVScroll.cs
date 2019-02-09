using UnityEngine;

public class UVScroll : MonoBehaviour
{
    // Scroll main texture based on time

    public SpriteRenderer spriteRenderer;

    void Start()
    {
        Rect dd = FineValue(spriteRenderer.sprite.uv);

        MaterialPropertyBlock block = new MaterialPropertyBlock();
        spriteRenderer.GetPropertyBlock(block);
        block.SetVector("_SpriteRect", new Vector4(dd.x, dd.y, dd.width, dd.height));
        block.SetFloat("_Speed", 5.0f);
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