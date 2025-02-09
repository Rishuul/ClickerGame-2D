using UnityEngine;

public class RotateCollectMoneyIcon : MonoBehaviour
{
    
    public float rotationSpeed = 4f;
   
    void FixedUpdate()
    {
        gameObject.transform.Rotate(0,rotationSpeed,0);
        
    }
}
