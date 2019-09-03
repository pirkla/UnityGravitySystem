using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public interface ISlowable {
    event Action<float> OnSlowed;
    event Action OnSlowEnd;
}
