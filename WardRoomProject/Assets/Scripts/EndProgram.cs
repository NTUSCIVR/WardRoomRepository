using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndProgram : MonoBehaviour {

    public void StartEnd()
    {
        StartCoroutine(EndProgramFunc());
    }

    IEnumerator EndProgramFunc()
    {
        yield return new WaitForSecondsRealtime(10);
        Application.Quit();
    }
}
