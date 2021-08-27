using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestroyObject : MonoBehaviour
{
    public GameObject text;
    public GameObject lives;
    public int scoreInt;
    public AudioSource owAudio;
    public AudioSource munchAudio;
    // Start is called before the first frame update
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("gametilemap_8"))
        {
            Destroy(collision.gameObject);
            scoreInt += 10;
            text.GetComponent<Text>().text = scoreInt.ToString();
            munchAudio.Play();
        }
        if (collision.gameObject.name.Contains("gametilemap_0"))
        {
            Destroy(collision.gameObject);
            scoreInt += 50;
            text.GetComponent<Text>().text = scoreInt.ToString();
        }
        if (collision.gameObject.name.Contains("enemy"))
        {
            owAudio.Play();
            if (lives.transform.childCount > 0)
            {
                Destroy(lives.transform.GetChild(0).gameObject);
            }
        }
    }
}
