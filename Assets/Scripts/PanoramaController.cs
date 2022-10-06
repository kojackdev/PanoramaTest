// Panorama demo
//
// Released as public domain by Kojack


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanoramaController : MonoBehaviour
{
    public List<Texture2D> textures = new List<Texture2D>();    // List of panorama textures to cycle through
    public Material panoMat;                                    // Panorama material
    public GameObject groundPlane;                              // Ground plane object. This is toggled on/off for the ground effect.
    public GameObject creditsQuad;                              // Floating quad used to show required ESO credits.
    public Material creditsAlma;                                // Material with the credits for the Alma image
    public Material creditsCabral;                              // Material with the credits for the Cabral image
    public Material creditsPlatform;                            // Material with the credits for the platform image

    float timer = 0;                                            // Timer for showing the credits quad
    int currentTexture = 0;                                     // Current panorama texture being used

    void Start()
    {
        creditsQuad.SetActive(false);
        SetPano(0);
    }

    // Change the panorama and start the credits display if required
    // index - index into the texture array of panorama to display
    void SetPano(int index)
    {
        currentTexture = index;
        panoMat.mainTexture = textures[currentTexture];
        if(panoMat.mainTexture.name == "alma-01-12k-cc-scaled")
        {
            creditsQuad.SetActive(true);
            creditsQuad.GetComponent<MeshRenderer>().material = creditsAlma;
            timer = 4;
        }
        if (panoMat.mainTexture.name == "ESO_Paranal_360_Marcio_Cabral_Chile_09-CC-scaled")
        {
            creditsQuad.SetActive(true);
            creditsQuad.GetComponent<MeshRenderer>().material = creditsCabral;
            timer = 4;
        }
        if (panoMat.mainTexture.name == "platform-day-pano-scaled")
        {
            creditsQuad.SetActive(true);
            creditsQuad.GetComponent<MeshRenderer>().material = creditsPlatform;
            timer = 4;
        }
    }

    void Update()
    {
        // Press A to cycle to the next panorama (wraps around)
        if(OVRInput.GetDown(OVRInput.Button.One,OVRInput.Controller.RTouch))
        {
            SetPano((currentTexture + 1) % textures.Count);
        }

        // Press B to toggle the ground on/off
        if (OVRInput.GetDown(OVRInput.Button.Two, OVRInput.Controller.RTouch))
        {
            groundPlane.SetActive(!groundPlane.activeSelf);
        }

        // Perform the credits display countdown
        if(creditsQuad.activeSelf)
        {
            timer -= Time.deltaTime;
            if(timer<=0)
            {
                creditsQuad.SetActive(false);
            }
        }
    }
}
