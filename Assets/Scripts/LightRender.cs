using UnityEngine;
using System.Collections;

public class LightRender : MonoBehaviour
{
    private Renderer rend;
    [SerializeField]
    [Range(0.0f, 20.0f)]
    private float timeToBlink = 2.0f;
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        StartCoroutine(Blink());
    }

    // Toggle the Object's visibility each second.
    private IEnumerator Blink() {
        while (true)
        {
            rend.enabled = !rend.enabled;
            yield return new WaitForSeconds(timeToBlink);
        }

    }
}

