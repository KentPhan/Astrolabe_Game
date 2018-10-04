using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefaultAnimationForImage : MonoBehaviour {
    public Sprite[] textures;
    public GameObject animatedGameObject;
    public Material animationMaterial;

    private float frameTime = 0.03f;
    private float collapsedTime = 0;
    private int frameCounter = 0;
    

    void Start () {
        animationMaterial = animatedGameObject.GetComponent<Image>().material;
        Debug.Log(animatedGameObject.name);
    }
	
	// Update is called once per frame
	void Update () {
        collapsedTime += Time.deltaTime;
        if (collapsedTime > frameTime)
        {
            collapsedTime = 0;
            StartCoroutine("PlayLoop", 0.04f);
        }
    }

    //The following methods return a IEnumerator so they can be yielded:  
    //A method to play the animation in a loop  
    IEnumerator PlayLoop(float delay)
    {
        //Wait for the time defined at the delay parameter  
        yield return new WaitForSeconds(delay);

        //Advance one frame  
        frameCounter = (++frameCounter) % textures.Length;
        //Stop this coroutine  
        animatedGameObject.GetComponent<Image>().sprite = textures[frameCounter];
        StopCoroutine("PlayLoop");
    }

    //A method to play the animation just once  
    IEnumerator Play(float delay)
    {
        //Wait for the time defined at the delay parameter  
        yield return new WaitForSeconds(delay);

        //If the frame counter isn't at the last frame  
        if (frameCounter < textures.Length - 1)
            ++frameCounter;

        animatedGameObject.GetComponent<Image>().sprite = textures[frameCounter];
        StopCoroutine("Play");
    }

}
