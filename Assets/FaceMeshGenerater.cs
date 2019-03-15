
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using UnityEngine.UI;
using System.IO;
public class FaceMeshGenerater : MonoBehaviour
{
    // Start is called before the first frame update
    public OscIn _oscIn;
    const string address1 = "/vertex";
    const string address2 = "/indices";
    List<float> _receivedFloats;
    
    private Mesh mesh;
    public Material mat;
    private MeshFilter meshfilter;
    private MeshRenderer renderer;
    private List<Vector3> vertices;
    private List<int> indices;
    public TextAsset indicesFile;

    public TextAsset vertexFile;

    private List<Vector3> baseVertices;

    public Mesh BaseMesh;
//    public Mesh baseFace;
    void Awake()
    {
        
        indices = new List<int>();
        var indexText = indicesFile.text.Split('\n');
        var maxIndex = 0;

        for (int i = 0; i < indexText.Length; i++)
        {
            
            var index = indexText[i].Split(' ');
            if(index.Length != 3 ) Debug.Log(index.Length);
            for (int j = 0; j < index.Length; j++)
            {
                indices.Add(int.Parse(index[j]));   
            }
        }


        indices = BaseMesh.GetIndices(0).ToList();
//        Debug.Log(text.Length);


        
        
        meshfilter = gameObject.AddComponent<MeshFilter>();
//        mesh = baseFace;
        mesh = new Mesh();
        
        meshfilter.sharedMesh = mesh;
//        Debug.Log(mesh.vertices.Length);
//        mesh.SetIndices(meshfilter.);
        renderer = gameObject.AddComponent<MeshRenderer>();
        renderer.material = mat;
        
        vertices = new List<Vector3>();
       
//        if( !_oscIn ) _oscIn = gameObject.AddComponent<OscIn>();
//        _oscIn.Open( 7000 );
        _receivedFloats = new List<float>();
        
        

        baseVertices = new List<Vector3>();
        var vertexArray = vertexFile.text.Split('\n');
        for (int i = 0; i < vertexArray.Length; i++)
        {
            
            var v = vertexArray[i].Split(' ');
            if(v.Length != 3 ) Debug.Log(i + " " + v.Length);
            baseVertices.Add(new Vector3(
                float.Parse(v[0]),
                float.Parse(v[1]),
                float.Parse(v[2])
                ));
           
        }

        baseVertices = BaseMesh.vertices.ToList();
        
        mesh.vertices = baseVertices.ToArray();
        mesh.SetIndices(indices.ToArray(),MeshTopology.Triangles,0);
        mesh.RecalculateNormals();
        meshfilter.sharedMesh = mesh;
//        
//        Debug.Log(indices.Count);
//        
    }
    void OnEnable()
    {
        // You can "map" messages to methods in two ways:

        // 1) For messages with a single argument, route the value using the type specific map methods.
        _oscIn.Map(address1, OnReceiveVertex);
        // 2) For messages with multiple arguments, route the message using the Map method.
        _oscIn.Map( address2, OnReceiveIndices );
    }


    void OnDisable()
    {
        // If you want to stop receiving messages you have to "unmap".
        _oscIn.Unmap( OnReceiveVertex );
        _oscIn.Unmap( OnReceiveIndices );
    }

		
    void OnReceiveVertex( OscMessage message )
    {
//        meshfilter.mesh.Clear();
//        mesh.vertices.
        
        var vertices = new List<Vector3>();
//        indices = new List<int>();
        if( message.TryGetBlob( 0, ref _receivedFloats ) ){
//            Debug.Log( "vertex: " + ListToString( _receivedFloats ) + "\n" );

            var count = 0;
           
            for (int i = 0; i < _receivedFloats.Count; i+=3)
            {
                
                var x = _receivedFloats[i];
                var y = _receivedFloats[i+1];
                var z = _receivedFloats[i+2];
                vertices.Add(new Vector3(x,y,z));
                baseVertices[count] = new Vector3(x, y, z);
                
//                indices.Add(count);
                count++;

            }
        }
        
        Debug.Log(vertices.Count);
        Debug.Log(baseVertices.Count);
        
//        if(mesh.vertices.Length != vertices.Count) return;
//        mesh.Clear();

//        Debug.Log(indices.Count);
//
//        if (indices.Count < 3)
//        {
//            int count = 0;
//            while (indices.Count < 3)
//            {
//                indices.Add(count);
//                Debug.Log(count);
//                count++;
//            }
//        }
        
        mesh.SetVertices(baseVertices);
        
//        mesh.vertices = baseVertices.ToArray();
        mesh.SetIndices(indices.ToArray(),MeshTopology.Triangles,0);
        mesh.RecalculateNormals();
        meshfilter.sharedMesh = mesh;
        OscPool.Recycle( message );
    }
    
    static string ListToString<T>( List<T> floats )
    {
        StringBuilder sb = new StringBuilder();
        for( int i = 0; i < floats.Count; i++ ) {
            if( i > 0 ) sb.Append( ", " );
            sb.Append( floats[i] );
        }
        return sb.ToString();
    }

    void Update()
    {
        if (Input.GetKey("d"))
        {
            UnityEditor.AssetDatabase.CreateAsset(meshfilter.mesh, "Assets/BaseFace.asset");
            UnityEditor.AssetDatabase.SaveAssets();
        }
    }
    void OnReceiveIndices( OscMessage message )
    {
//        Debug.Log();
//        Debug.Log( "index: " + ListToString( indices ) + "\n" );
        
//        indices.Clear();
//        if( message.TryGetBlob( 0, ref indices ) ){
//            Debug.Log( "index: " + ListToString( indices ) + "\n" );
            
//            StreamWriter sw = new StreamWriter("./LogData.txt",false); //true=追記 false=上書き
//            for (int i = 0; i < indices.Count; i++ )
//            {
////                Debug.Log(indices[i]);
//                sw.WriteLine(indices[i]);
//            }
//        
//            sw.Flush();
//            sw.Close();

//            mesh.SetIndices(indices.ToArray(),MeshTopology.Triangles,0);
//        }
    }
}
