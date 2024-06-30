using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface ICollectable
{
    public int getRockID();
    public void OnCollect(RawImage rIamge) { }
}
