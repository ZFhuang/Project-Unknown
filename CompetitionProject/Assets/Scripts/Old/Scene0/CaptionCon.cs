using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

//Caption controller, this is the only type of script that can be added to Timeline
[System.Serializable]
public class CaptionCon : PlayableAsset
{
    //ExposedReference is used to convert objects to the real running script
    public ExposedReference<TMPro.TextMeshProUGUI> dialogContainer;
    public string dialogStr;

    // Factory method that generates a playable based on this asset
    public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
    {
        //The real running script class
        Caption caption = new Caption();

        //Set some values
        caption.dialogStr = dialogStr;
        //Set some objects
        caption.dialogContainer = dialogContainer.Resolve(graph.GetResolver());

        return ScriptPlayable<Caption>.Create(graph, caption);
    }
}
