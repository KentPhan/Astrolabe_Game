using UnityEngine;
using System.Collections;

public class Sample : MonoBehaviour
{
    //An array of Objects that stores the results of the Resources.LoadAll() method  
    private Object[] objects;
    //Each returned object is converted to a Texture and stored in this array  
    public Texture[] textures;
    //With this Material object, a reference to the game object Material can be stored  
    private Material goMaterial;
    //An integer to advance frames  
    private int frameCounter = 0;

    void Awake()
    {
        //Get a reference to the Material of the game object this script is attached to  
        Debug.Log(GetComponent<Renderer>().name);
        this.goMaterial = GetComponent<Renderer>().material;
    }

    void Start()
    {
        
    }

    void Update()
    {
        //Call the 'PlayLoop' method as a coroutine with a 0.04 delay  
        StartCoroutine("Play", 0.04f);
        //Set the material's texture to the current value of the frameCounter variable  
        if (frameCounter < textures.Length)
            goMaterial.mainTexture = textures[frameCounter];

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
        }

        //Stop this coroutine  
        StopCoroutine("PlayLoop");
    }

}