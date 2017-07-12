using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutScene : MonoBehaviour {

    public AudioClip[] chapters;
    private Camera cam;
    public GameObject map;
    public GameObject paper;
    public GameObject paperPointHigher;
    public GameObject paperPointLower;
    public GameObject mapPointHigher;
    public GameObject mapPointLower;
    public GameObject fader;
    public Texture[] textures;
    public AudioSource audio;
   // bool chapterOne = true;
   // bool chapterTwo = false;
   // bool chapterThree = false;
   // bool chapterFour = false;
   // bool chapterFive = false;
    private Transform target = null;
    private bool moveCamera = true;
    private bool startgame = false;
    private float timer = 0;

    private float speed = 0.003f;
    

    void Start ()
    {
        audio = GetComponent<AudioSource>();
        audio.Play();
        cam = Camera.main;
        fader = GameObject.FindGameObjectWithTag("Fader");
        fader.GetComponent<MeshRenderer>().material.color = new Color(0, 0, 0, 0);
        paperPointHigher = GameObject.FindGameObjectWithTag("PaperPointHigher");
        paperPointLower = GameObject.FindGameObjectWithTag("PaperPointLower");
        mapPointHigher = GameObject.FindGameObjectWithTag("MapPointHigher");
        mapPointLower = GameObject.FindGameObjectWithTag("MapPointLower");
        map = GameObject.FindGameObjectWithTag("Map");
        paper = GameObject.FindGameObjectWithTag("NewsPaper");
        target = mapPointLower.transform;
        StartCoroutine(cutscene());
    }

    void Update ()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            audio.Stop();
            startgame = true;
        }
        if (moveCamera)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, target.position, speed);
        }
        if (startgame)
        {
            fader.GetComponent<MeshRenderer>().material.color = new Color(0, 0, 0, 1);
            timer += Time.deltaTime;
            if (timer >= 5) SceneManager.LoadScene("Main");
        }
    }

    IEnumerator cutscene()
    {
        for (int i = 1; i <= chapters.Length; i++)
        {
            yield return new WaitForSeconds((GetComponent<AudioSource>().clip.length) + 3);
            if (i <= 3)
            {
                gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, paperPointHigher.transform.position, 1000000);
                target = paperPointLower.transform;
                speed = 0.02f;
                GetComponent<AudioSource>().clip = chapters[i];
                audio.Play();
                paper.GetComponent<Renderer>().material.mainTexture = textures[i+7];
            }
            else
            {
                gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, mapPointHigher.transform.position, 1000000);
                target = mapPointLower.transform;
                speed = 0.003f;
                GetComponent<AudioSource>().clip = chapters[i];
                audio.Play();
                for (int a = 1; a <= 7; a++)
                {
                    if(a == 7) yield return new WaitForSeconds(11);
                    else yield return new WaitForSeconds(3);
                    map.GetComponent<Renderer>().material.mainTexture = textures[a];
                    if (a == 7)
                    {
                        yield return new WaitForSeconds(22);
                        startgame = true;
                    }
                }
            }
        }
    }
}
