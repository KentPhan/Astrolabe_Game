using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstellationDisplayItem : MonoBehaviour {
    public Texture[] textures;
    public bool isAnimated = false;
    public bool isCongratulation = false;
    public bool isSetCongratulationMaterial = false;
    public bool isFinished = false;
    public bool isSetFinishedMaterial = false;
    public bool hasAnimation = false;
    
    private int frameCounter = 0;
    private int animatedMaterialID = 1;
    private float collapsedTime = 0;
    private float frameTime = 0.03f;
    private float alphaSpeed = 0.01f;

    public Material FinishedMaterial;
    public Material CongratulationMaterial;
    public GameObject congratulationAnimation;

    // Use this for initialization
    void Start () {
    }

    // Update is called once per frame
    void Update()
    {
        
        if (hasAnimation) {
            
            if (isAnimated)
            {
                collapsedTime += Time.deltaTime;
                if (collapsedTime > frameTime)
                {
                    collapsedTime = 0;
                    StartCoroutine("Play", 0.04f);
                }
            } else if (frameCounter >= 0 && !isCongratulation)
            {
                collapsedTime += Time.deltaTime;
                if (collapsedTime > frameTime)
                {
                    collapsedTime = 0;
                    StartCoroutine("PlayBack", 0.04f);
                }
            }
        } 
        if (isCongratulation && !isFinished)
        {
            if (!isSetCongratulationMaterial)
            {
                var newMaterials = this.GetComponentInChildren<Renderer>().materials;
                if (CongratulationMaterial)
                    newMaterials[2] = CongratulationMaterial;
                if (FinishedMaterial)
                    newMaterials[3] = FinishedMaterial;
                this.GetComponentInChildren<Renderer>().materials = newMaterials;
                isSetCongratulationMaterial = true;
            } else
            {
                var newMaterials = this.GetComponentInChildren<Renderer>().materials;
                var newMaterialColor = newMaterials[2].color;
                Debug.Log(newMaterialColor);
                newMaterialColor.a += alphaSpeed;
                if (newMaterialColor.a >= 1)
                {
                    newMaterialColor.a = 1;
                    alphaSpeed = -alphaSpeed / 2;
                }

                if (newMaterialColor.a <= 0 && alphaSpeed < 0)
                {
                    newMaterialColor.a = 0;
                    isFinished = true;
                }
                newMaterials[2].color = newMaterialColor;
                this.GetComponentInChildren<Renderer>().materials = newMaterials;
            }
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
        StopCoroutine("PlayLoop");
    }

    //A method to play the animation just once  
    IEnumerator Play(float delay)
    {
        //Wait for the time defined at the delay parameter  
        yield return new WaitForSeconds(delay);

        //If the frame counter isn't at the last frame  
        if (frameCounter < textures.Length - 1)
        {
            //Advance one frame  
            ++frameCounter;
            
        } else if (frameCounter >= textures.Length - 1)
        {
            isAnimated = false;
            isCongratulation = true;
        }
        var newMaterials = this.GetComponentInChildren<Renderer>().materials;
        newMaterials[animatedMaterialID].mainTexture = textures[frameCounter];
        this.GetComponentInChildren<Renderer>().materials = newMaterials;
        //Stop this coroutine  
        StopCoroutine("Play");
    }

    IEnumerator PlayBack(float delay)
    {
        //Wait for the time defined at the delay parameter  
        yield return new WaitForSeconds(delay);

        //If the frame counter isn't at the last frame  
        if (frameCounter > 0)
        {
            --frameCounter;
            
        }
        var newMaterials = this.GetComponentInChildren<Renderer>().materials;
        newMaterials[animatedMaterialID].mainTexture = textures[frameCounter];
        this.GetComponentInChildren<Renderer>().materials = newMaterials;
        StopCoroutine("PlayBack");
    }
}
