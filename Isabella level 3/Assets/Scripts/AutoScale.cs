using UnityEngine;

public class AutoScale : MonoBehaviour
{
    public Vector3 iScale = new Vector3(0.1f, 0.1f, 0.1f);
    private Vector3 fScale;

    public float speed = 2f; // At speed 2, scaling takes 0.5 seconds (1 / speed)
    private float progress = 0f;

    void Start()
    {
        // Save the target scale from the editor, then shrink the object to iScale
        fScale = transform.localScale;
        transform.localScale = iScale;
    }

    void Update()
    {
        // Only run if we haven't reached the target scale yet
        if (progress < 1f)
        {
            progress += Time.deltaTime * speed;
            transform.localScale = Vector3.Lerp(iScale, fScale, progress);
        }
    }
}
