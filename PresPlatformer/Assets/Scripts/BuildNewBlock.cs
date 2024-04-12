using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildNewBlock : MonoBehaviour
{
    public GameObject blockPrefab;
    public GameObject block;
    public Transform blockTransform;

    public float xPos;
    public float yPos;
    public float disappearTime;
    public float reappearTime;
    public Quaternion rotation;


    public void Start()
    {
        xPos = blockTransform.position.x;
        yPos = blockTransform.position.y;
        rotation = blockTransform.rotation;
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Disappear());
        }
    }

    IEnumerator Disappear()//removes the block after 1 second
    {
        Color c = block.GetComponent<Renderer>().material.color;
        for (float green = 1f; green >= 0; green -= 0.1f)
        {
            c.g = green;
            block.GetComponent<Renderer>().material.color = c;
            yield return new WaitForSeconds(disappearTime/10);
        }
        Destroy(block);
        BuildIt();
    }


    public void BuildIt()
    {
        StartCoroutine(Reappear());
    }

    IEnumerator Reappear() //Spawns another block in the place of the old one
    {
        for (int i = 0; i < reappearTime; i++)
        {
            yield return new WaitForSeconds(1f);
        }
        block = Instantiate(blockPrefab, new Vector2(xPos, yPos), rotation);
    }
}
