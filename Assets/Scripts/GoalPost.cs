using System;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GoalPost : MonoBehaviour
{
    public ParticleSystem portal_effect;
    public float attractant_force;
    private Rigidbody player;
    private AsyncOperation async;
    public String next_level;
    private AudioSource standard_source;
    public AudioClip portal_sound;
	// Use this for initialization
	void Start () {
        standard_source = GameObject.Find("Player").GetComponentsInChildren<AudioSource>()[3];
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        ParticleSystem portal = (ParticleSystem)Instantiate(portal_effect, transform.position, portal_effect.transform.rotation);
        player = other.attachedRigidbody;
        InvokeRepeating("Attract", 0, 0.04f);
        standard_source.clip = portal_sound;
        standard_source.loop = true;
        standard_source.volume = 1;
        standard_source.Play();
        StartLoading();
    }

    void Attract()
    {
        player.AddForce((transform.position - player.gameObject.transform.position) * attractant_force);
    }

    public void StartLoading()
    {
        StartCoroutine("load");
    }

    IEnumerator load()
    {
        Debug.LogWarning("ASYNC LOAD STARTED - " +
           "DO NOT EXIT PLAY MODE UNTIL SCENE LOADS... UNITY WILL CRASH");
        async = SceneManager.LoadSceneAsync(next_level);
        async.allowSceneActivation = true;
        yield return async;
    }


}
