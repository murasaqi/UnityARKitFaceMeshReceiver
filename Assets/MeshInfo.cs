using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class MeshInfo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var filter = GetComponent<MeshFilter>();
        
        Debug.Log(filter.mesh.name);
        
        StreamWriter sw = new StreamWriter("./LogData.txt",true); //true=追記 false=上書き
        var array = filter.mesh.GetIndices(0);
        for (int i = 0; i < array.Length; i++ )
        {
            Debug.Log(array[i]);
           sw.WriteLine(array[i]);
        }
        
        sw.Flush();
        sw.Close();
        
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
