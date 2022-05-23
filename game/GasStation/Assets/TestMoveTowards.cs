using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMoveTowards : MonoBehaviour
{

    [SerializeField]
    private GameObject Target;
    private float speed = 1.5f;
    private bool move = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void StartMoveToward()
    {
        move = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (move)
        {
            this.transform.position = Vector2.MoveTowards(this.transform.position, Target.transform.position, speed * Time.deltaTime);
        }
    }
}
