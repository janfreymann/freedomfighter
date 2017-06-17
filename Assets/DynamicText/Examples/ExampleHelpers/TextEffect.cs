using UnityEngine;
using System.Text;

[RequireComponent(typeof(DynamicText))]
public class TextEffect : MonoBehaviour
{
    public string charGradient = " ¸.:oOQ@&8*º°\"'´";
    public int width = 10;
    public int height = 10;

    DynamicText dynamicText;

    void Start()
    {
    }

    void Update()
    {
        width = Mathf.Clamp(width, 1, 100);
        height = Mathf.Clamp(height, 1, 100);

        int chGradientLen = charGradient.Length;
        if (chGradientLen < 1)
        {
            Debug.LogWarning("No char gradient!", this);
            return;
        }

        dynamicText = GetComponent<DynamicText>();
        if (dynamicText == null)
        {
            Debug.LogError("No Dynamic Text!", this);
            return;
        }

        int charCount = height * (width + 3);
        StringBuilder sb = dynamicText.textSB;
        sb.EnsureCapacity(charCount);
        sb.Length = charCount;
        int sbOffs = 0;
        for (int y = 0; y < height; ++y)
        {
            sb[sbOffs++] = '.';
            for (int x = 0; x < width; ++x)
            {
                float t = Time.fixedTime;
                float s1 = Mathf.Sin(t * 0.77f + x * 0.41f) * 3;
                float s2 = Mathf.Sin(t * 0.65f - y * 0.51f) * 3;
                float s3 = Mathf.Sin(t * 1.93f - x * 0.27f) * 4;
                float s4 = Mathf.Sin(t * 1.91f + y * 0.29f) * 3;
                int chValue = (int)(s1 * s2 + s3 + s4 + chGradientLen * 16 + chGradientLen / 2) % chGradientLen;

                //dynamicText.textSB.Append(charGradient[chValue]);
                sb[sbOffs++] = charGradient[chValue];
            }
            sb[sbOffs++] = '.';
            sb[sbOffs++] = '\n';
        }

        dynamicText.FinishedTextSB();
    }
}
