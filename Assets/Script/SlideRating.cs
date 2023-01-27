using System.Collections.Generic;
using UnityEngine.UI;
using System;

[Serializable]
public class SlideRating
{
    public int presentation;
    public int reception;

    public SlideRating(int[] data)
    {
        presentation = data[0];
        reception = data[1];
    }
}