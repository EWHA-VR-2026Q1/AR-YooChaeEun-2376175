using UnityEngine;

public class ObjectController : MonoBehaviour
{
    public GameObject cube;
    public GameObject sphere;

    public void MoveSphere()
    {
        sphere.transform.position += new Vector3(1f, 0f, 0f);
    }

    public void RotateCube()
    {
        cube.transform.Rotate(0f, 45f, 0f);
    }
}