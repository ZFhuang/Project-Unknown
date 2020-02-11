using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

// A behaviour that is attached to a playable
public class Caption : PlayableBehaviour
{
    //Use TMPro to show captions
    public TMPro.TextMeshProUGUI dialogContainer;
    public string dialogStr="";

    // Called when the owning graph starts playing
    public override void OnGraphStart(Playable playable)
    {

    }

    // Called when the owning graph stops playing
    public override void OnGraphStop(Playable playable)
    {

    }

    // Called when the state of the playable is set to Play
    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        if (dialogContainer != null)
        {
            //Show new caption
            dialogContainer.gameObject.SetActive(true);
            dialogContainer.text = dialogStr;
        }
    }

    // Called when the state of the playable is set to Paused
    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        if (dialogContainer != null)
        {
            //Close the current caption
            dialogContainer.gameObject.SetActive(false);
            dialogContainer.text = "";
        }
    }

    // Called each frame while the state is set to Play
    public override void PrepareFrame(Playable playable, FrameData info)
    {

    }
}
