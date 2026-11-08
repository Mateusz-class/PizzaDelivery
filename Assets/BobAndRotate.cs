using UnityEngine;

//Code from:
//https://youtu.be/Fv5ASm1iFl4?si=6MCCJVFgL-0ISVts
public class BobAndRotate : MonoBehaviour
{

    [Header("Bobbing Variables")]
    [SerializeField, Range(0, 2)] float yOffset = 0;
    [SerializeField, Range(0, 2)] float bobbingSpeed = 2f;
    [SerializeField, Range(0, 2)] float upDownDifference = 0.5f;

    [Header("Rotation Variables")]
    [SerializeField, Range(-100, 100)] float yRotationSpeed = 50;

    Vector3 originalPosition;

    void Start()
    {
        transform.position += new Vector3(0, yOffset, 0);
        originalPosition = transform.position;
    }

    void Update()
    {
        transform.position = originalPosition + Mathf.Sin(Time.time * bobbingSpeed) * (upDownDifference / 2) * Vector3.up;
        transform.Rotate(new Vector3(0, yRotationSpeed, 0) * Time.deltaTime);
    }
}