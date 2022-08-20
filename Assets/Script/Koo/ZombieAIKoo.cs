using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAIKoo : MonoBehaviour
{

    private enum Direction { LEFT, RIGHT, UP, DOWN };
    private enum State { Pace, Trace, Attack };

    private NavMeshAgent myNv;

    public int tile_interval;

    private int limit;

    public GameObject startTile;



    private void Awake()
    {
        myNv = GetComponent<NavMeshAgent>();
    }
    // Start is called before the first frame update
    void Start()
    {
        limit = 0;
        tile_interval = 1;

        transform.position = startTile.transform.position;

        myNv.height = 0.5f;
        myNv.baseOffset = 0.05f;
    }




    // Update is called once per frame
    void Update()
    {

    }

    public void Pacing()
    {
        int direction = Random.Range(0, 4);

        if (limit == 4)
        {
            limit = 0;
            Debug.Log("limit!");
            return;
        }

        switch (direction)
        {
            case 0:
                {
                    if (Moveable(Direction.LEFT))
                    {
                        Debug.Log("Left");
                        BasicMove(Direction.LEFT);
                        limit = 0;
                    }
                    else
                    {
                        limit++;
                        Pacing();
                    }
                    break;
                }
            case 1:
                {
                    if (Moveable(Direction.RIGHT))
                    {
                        Debug.Log("Right");
                        BasicMove(Direction.RIGHT);
                        limit = 0;
                    }
                    else
                    {
                        limit++;
                        Pacing();
                    }
                    break;
                }
            case 2:
                {
                    if (Moveable(Direction.UP))
                    {
                        Debug.Log("Up");
                        BasicMove(Direction.UP);
                        limit = 0;
                    }
                    else
                    {
                        limit++;
                        Pacing();
                    }
                    break;
                }
            case 3:
                {
                    if (Moveable(Direction.DOWN))
                    {
                        Debug.Log("Down");
                        BasicMove(Direction.DOWN);
                        limit = 0;
                    }
                    else
                    {

                        limit++;
                        Pacing();
                    }
                    break;
                }
        }
    }

    private bool Moveable(Direction dir)
    {
        switch (dir)
        {
            case Direction.LEFT:
                {
                    RaycastHit hit;

                    Physics.Raycast(transform.transform.position + (Vector3.right * -tile_interval), Vector3.down, out hit, 5.0f);

                    if (hit.collider.gameObject.layer == 6)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            case Direction.RIGHT:
                {
                    RaycastHit hit;

                    Physics.Raycast(transform.position + (Vector3.right * tile_interval), Vector3.down, out hit, 5.0f);

                    if (hit.collider.gameObject.layer == 6)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            case Direction.UP:
                {
                    RaycastHit hit;

                    Physics.Raycast(transform.position + (Vector3.forward * tile_interval), Vector3.down, out hit, 5.0f);

                    if (hit.collider.gameObject.layer == 6)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            case Direction.DOWN:
                {
                    RaycastHit hit;

                    Physics.Raycast(transform.position + (Vector3.forward * -tile_interval), Vector3.down, out hit, 5.0f);

                    if (hit.collider.gameObject.layer == 6)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            default:
                return false;
        }
    }

    private void BasicMove(Direction dir)
    {
        switch (dir)
        {
            case Direction.LEFT:
                {
                    myNv.destination = new Vector3(transform.position.x - tile_interval, transform.position.y, transform.position.z);
                    break;
                }
            case Direction.RIGHT:
                {
                    myNv.destination = new Vector3(transform.position.x + tile_interval, transform.position.y, transform.position.z);
                    break;

                }
            case Direction.UP:
                {
                    myNv.destination = new Vector3(transform.position.x, transform.position.y, transform.position.z + tile_interval);
                    break;

                }
            case Direction.DOWN:
                {
                    myNv.destination = new Vector3(transform.position.x, transform.position.y, transform.position.z - tile_interval);
                    break;
                }
        }
    }
}
//left: -
//right : +
//up : +
//down : -