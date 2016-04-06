using UnityEngine;
using System.Collections;

public class TitleBall : MonoBehaviour
{
    private GameObject _ball;

    void Start ()
    {
        _ball = gameObject;
    }
	
	// Update is called once per frame
	void Update () {
        transform.transform.rotation = Quaternion.AngleAxis(0.5f, Vector3.left) * transform.rotation;
    }

}
