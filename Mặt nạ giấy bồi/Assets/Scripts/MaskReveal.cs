using UnityEngine;
using System.Collections;

public class MaskReveal : MonoBehaviour
{
    public GameObject paperParent;
    public GameObject maskModel;
    public Transform mold;
    public float removeSpeed = 0.5f;
    public Vector3 removeDirection = new Vector3(0, 1, 0);

    private bool isRemoving = false;

    void Start()
    {
        maskModel.SetActive(false);
    }

    // 
    public void OnRevealMaskButtonClicked()
    {
        if (!isRemoving)
        {
            StartCoroutine(RemoveMask());
        }
    }

    IEnumerator RemoveMask()
    {
        isRemoving = true;

        paperParent.SetActive(false);
        maskModel.SetActive(true);

        float time = 0;
        Vector3 startPosition = maskModel.transform.position;
        Vector3 targetPosition = startPosition + removeDirection * 0.9f;

        while (time < 1)
        {
            time += Time.deltaTime * removeSpeed;
            maskModel.transform.position = Vector3.Lerp(startPosition, targetPosition, time);
            yield return null;
        }

        isRemoving = false;
    }
}
