using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarsGenerator : MonoBehaviour
{
    public GameObject StarObject;

    public float frequencyBot;//min stars per square unit
    public float frequencyTop;//max stars per square unit

    public float sizeBot;//min stars size
    public float sizeTop;//max stars size

    public float starBlinkProbability;

    private List<GameObject> stars;

    private float frequency;//actual stars per square unit

    void Start()
    {
        stars = new List<GameObject>();
        if (frequencyBot>frequencyTop)
        {
            Debug.LogError("ERROR: frequencyBot > frequencyTop");
            frequency = frequencyTop;
        }else frequency = Random.Range(frequencyBot, frequencyTop);

        float yExtent = GetComponent<Camera>().orthographicSize * 1.01f;
        float xExtent = yExtent * Screen.width / Screen.height * 1.01f;
        Vector3 cameraPosition = GetComponent<Camera>().transform.position;

        int starsNumber = (int) ((xExtent * 2) * (yExtent * 2) * frequency);

        for(int i = 0; i < starsNumber; i++)
        {
            Vector3 newPos = new Vector3(cameraPosition.x + Random.Range(-xExtent, xExtent),
                                        cameraPosition.y + Random.Range(-yExtent, yExtent),
                                        0);
            float scale = Random.Range(sizeBot, sizeTop);
            GameObject generatedStar = Instantiate(StarObject, newPos, Quaternion.identity) as GameObject;
            generatedStar.transform.localScale *= scale;
            generatedStar.transform.parent = gameObject.transform;

            stars.Add(generatedStar);
        }
    }

    private void Update()
    {
        foreach(GameObject star in stars)
            if(Random.Range(0.0f,1.0f) < starBlinkProbability)
                star.GetComponent<Animator>().SetTrigger("Blink");
    }
}
