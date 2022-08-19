using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    private enum Direction { LEFT, RIGHT, UP, DOWN};
    public enum State { Pace, Trace, Attack};

    private NavMeshAgent myNv;

    public float tile_interval;

    private int limit;

    public GameObject startTile;

    public GameObject tracingTarget;

    State currentState;

    private void Awake()
    {
        myNv = GetComponent<NavMeshAgent>();   
    }
    // Start is called before the first frame update
    void Start()
    {
        // �̵� ���� limit (4 �̻� �� �� ����)
        limit = 0;

        // ���� ������ ����
        currentState = 0;

        // �ʱ� ���� ��ġ
        transform.position = startTile.transform.position;

        // �߰� ���
        tracingTarget = null;
    }

    

    // Update is called once per frame
    void Update()
    {

    }

    public void TurnPlaying()
    {
        switch(currentState)
        {
            case State.Pace:
            {
                Pacing();
                break;
            }
            case State.Trace:
            {
                Tracing();
                break;
            }
            case State.Attack:
            {
                Attacking();
                break;
            }
        }
    }    

    public void SetState(State st, GameObject target)
    {
        currentState = st;

        tracingTarget = target;
    }

    // �����Ÿ� ����
    public void Pacing()
    {
        int direction = Random.Range(0, 4);

        if(limit == 4)
        {
            limit = 0;
            Debug.Log("limit!");
            return;
        }

        switch(direction)
        {
            case 0:
                {
                    if (Moveable(Direction.LEFT))
                    {
                        //Debug.Log("Left");
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
                        //Debug.Log("Right");
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
                        //Debug.Log("Up");
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
                        //Debug.Log("Down");
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
        switch(dir)
        {
            case Direction.LEFT:
                {
                    RaycastHit hit;

                    Physics.Raycast(transform.transform.position + (Vector3.right * -tile_interval), Vector3.down, out hit, 5.0f);
                    
                    if(hit.collider.gameObject.layer == 6)
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
        switch(dir)
        {
            case Direction.LEFT:
                {
                    myNv.destination = new Vector3(transform.position.x-tile_interval, transform.position.y, transform.position.z);
                    break;
                }
            case Direction.RIGHT:
                {
                    myNv.destination = new Vector3(transform.position.x+tile_interval, transform.position.y, transform.position.z);
                    break;

                }
            case Direction.UP:
                {
                    myNv.destination = new Vector3(transform.position.x , transform.position.y , transform.position.z + tile_interval);
                    break;

                }
            case Direction.DOWN:
                {
                    myNv.destination = new Vector3(transform.position.x , transform.position.y , transform.position.z - tile_interval);
                    break;
                }
        }
    }

    // �߰� ����
    public void Tracing()
    {
        if(tracingTarget)
        {
            Direction xDir;
            Direction yDir;
            // x ���� ���� ��ġ
            if(tracingTarget.transform.position.x == transform.position.x)
            {
                // ���� ��ġ
                if(tracingTarget.transform.position.y > transform.position.y)
                {
                    BasicMove(Direction.UP);
                }
                // �Ʒ� ��ġ
                else
                {
                    BasicMove(Direction.DOWN);
                }
            }
            // y ���� ���� ��ġ
            // Ÿ���� ���� �ؿ� ��ġ�� ���
            // Ÿ���� ���� ���� ��ġ�� ���
        }
    }

    // ���� ����

    public void Attacking()
    {

    }

}
//left: -
//right : +
//up : +
//down : -