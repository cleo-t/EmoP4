using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StickerBoard : MonoBehaviour
{
    [Serializable]
    public struct StickerBugPair
    {
        public BugManager.Bug bugType;
        public Renderer sticker;
    }

    [SerializeField]
    private List<StickerBugPair> stickers;

    void Start()
    {
        foreach(StickerBugPair sticker in stickers)
        {
            sticker.sticker.enabled = false;
        }
    }

    void Update()
    {
        List<BugManager.Bug> bugTypesCaught = BugManager.instance.GetBugTypesCaught();
        foreach(StickerBugPair sticker in stickers)
        {
            sticker.sticker.enabled = bugTypesCaught.Contains(sticker.bugType);
        }
    }
}
