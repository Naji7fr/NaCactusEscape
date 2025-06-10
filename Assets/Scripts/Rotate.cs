using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float speed = 360f;
    // Update is called once per frame 
     
    void Update()
    {
        transform.rotation = Quaternion.Euler(0, Time.time * speed, 0);
    }
    
}
