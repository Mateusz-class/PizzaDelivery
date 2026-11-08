using UnityEngine;
using UnityEngine.Rendering.Universal;

//Code From:
//https://discussions.unity.com/t/how-to-set-cookie-offset-in-urp/251898/3
public class LightCookieManager : MonoBehaviour
{
    public Vector2 speed = new();

    private UniversalAdditionalLightData lightData;

    void Start()
    {
        lightData = GetComponent<UniversalAdditionalLightData>();

        if(lightData == null)
        {
            enabled = false;
        }
    }

    void Update()
    {
        lightData.lightCookieOffset += speed * Time.deltaTime;
    }
}