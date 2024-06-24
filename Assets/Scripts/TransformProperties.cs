using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformProperties
{
    private static ETransform form = ETransform.HUMAN_FORM;

    public static ETransform Form
    {
        get { return form; }  // Use the backing field 'form'
        set { form = value; } // Use the backing field 'form'
    }
}

