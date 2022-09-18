using UnityEngine;
using UnityEngine.UI;

public class Task3Script : MonoBehaviour
{
    public GameObject cube;
    public int count;
    
    void Start()
    {
        count = 0;
    }

    public void MakeObjects(string inputField){
        count = int.Parse(inputField);
        
        for (int i = 0; i < count; ++i){
            Instantiate(cube, new Vector3(-10 + i, 5, 0), cube.transform.rotation);
        }
    }

}
