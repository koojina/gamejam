using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    private enum Direction { LEFT, RIGHT, UP, DOWN};
    public enum State { Idle, Pace, Trace, Attack};

    private NavMeshAgent myNv;

    public float tile_interval;

    private int limit;

    public GameObject startTile;

    State currentState;

    State prevState;

    public GameObject tracingTarget;

    public bool IsTurn;

    public GameObject atkTrigger;

    public GameObject[] RangeBox;

    private void Awake()
    {
        myNv = GetComponent<NavMeshAgent>();   
    }
    // Start is called before the first frame update
    void Start()
    {
        transform.parent.transform.position = startTile.transform.position;

        transform.localPosition = Vector3.zero;

        // 기본 턴 세팅 false
        IsTurn = false;

        // 이동 결정 limit (4 이상 시 턴 종료)
        limit = 0;

        // 다음 진행할 상태
        currentState = State.Pace;

        // 추격 대상
        tracingTarget = null;
    }

    

    // Update is called once per frame
    void Update()
    {

    }

    public void TurnPlaying()
    {
        Debug.Log(currentState);

        switch(currentState)
        {
            case State.Idle:
                {
                    Idle();
                    IsTurn = false;
                    prevState = State.Idle;
                    break;
                }
            case State.Pace:
            {
                Pacing();
                IsTurn = false;
                prevState = State.Pace;
                break;
            }
            case State.Trace:
            {
                Tracing();
                IsTurn = false;
                    prevState = State.Trace;

                    break;
            }
            case State.Attack:
            {
                Attacking();
                IsTurn = false;
                    prevState= State.Attack;
                break;
            }
        }
    }    

    public void SetState(State st, GameObject targetPos)
    {
        currentState = st;

        tracingTarget = targetPos;

        switch (st)
        {
            case State.Idle:
                {
                    atkTrigger.GetComponent<AttackTrigger>().activeMode = false;
                    prevState = State.Idle;
                    break;
                }
            case State.Pace:
                {
                    atkTrigger.GetComponent<AttackTrigger>().activeMode = false;
                    prevState = State.Pace;
                    break;
                }
            case State.Trace:
                {
                    atkTrigger.GetComponent<AttackTrigger>().activeMode = false;
                    prevState = State.Trace;
                    break;
                }
            case State.Attack:
                {
                    prevState = State.Attack;
                    break;
                }
        }
    }
    
    public State GetState()
    {
        return currentState;
    }

    public void Idle()
    {

    }

    // 서성거림 로직
    public void Pacing()
    {
        atkTrigger.GetComponent<AttackTrigger>().activeMode = false;

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

                    Debug.DrawRay(transform.transform.position + (Vector3.right * -tile_interval), Vector3.down, Color.green,5.0f);

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

                    Debug.DrawRay(transform.position + (Vector3.right * tile_interval), Vector3.down, Color.green, 5.0f);

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

                    Debug.DrawRay(transform.position + (Vector3.forward * tile_interval), Vector3.down, Color.green, 5.0f);

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

                    Debug.DrawRay(transform.position + (Vector3.forward * -tile_interval), Vector3.down, Color.green, 5.0f);

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
                    //Debug.Log("목적지");
                    //Debug.Log(new Vector3(transform.position.x - tile_interval, transform.position.y, transform.position.z));
                    myNv.destination = new Vector3(transform.position.x-tile_interval, transform.position.y, transform.position.z);
                    //transform.Translate(new Vector3(transform.position.x - tile_interval, transform.position.y, transform.position.z));
                    break;
                }
            case Direction.RIGHT:
                {
                    //Debug.Log("목적지");
                    //Debug.Log(new Vector3(transform.position.x + tile_interval, transform.position.y, transform.position.z));
                    myNv.destination = new Vector3(transform.position.x+tile_interval, transform.position.y, transform.position.z);
                    //transform.Translate(new Vector3(transform.position.x + tile_interval, transform.position.y, transform.position.z));
                    break;

                }
            case Direction.UP:
                {
                    //Debug.Log("목적지");
                    //Debug.Log(new Vector3(transform.position.x, transform.position.y, transform.position.z + tile_interval));
                    myNv.destination = new Vector3(transform.position.x , transform.position.y , transform.position.z + tile_interval);
                    //transform.Translate(new Vector3(transform.position.x, transform.position.y, transform.position.z + tile_interval));
                    break;

                }
            case Direction.DOWN:
                {
                    //Debug.Log("목적지");
                    //Debug.Log(new Vector3(transform.position.x, transform.position.y, transform.position.z - tile_interval));
                    myNv.destination = new Vector3(transform.position.x , transform.position.y , transform.position.z - tile_interval);
                    //transform.Translate(new Vector3(transform.position.x, transform.position.y, transform.position.z - tile_interval));
                    break;
                }
        }
    }

    // 추격 로직
    public void Tracing()
    {
        atkTrigger.GetComponent<AttackTrigger>().activeMode = false;

        if (tracingTarget)
        {
                // x 동일 선상 위치
                if (tracingTarget.transform.position.x == transform.position.x)
                {
                    // 위에 위치
                    if (tracingTarget.transform.position.z > transform.position.z)
                    {
                        if (Moveable(Direction.UP))
                        {
                            BasicMove(Direction.UP);
                        }
                        else
                        {
                            int rand = Random.Range(0, 2);
                            if (rand == 0)
                            {
                                if (Moveable(Direction.LEFT))
                                {
                                    BasicMove(Direction.LEFT);
                                }
                                else
                                {
                                    if (Moveable(Direction.RIGHT))
                                    {
                                        BasicMove(Direction.RIGHT);
                                    }
                                    else
                                    {
                                        return;
                                    }
                                }
                            }
                            else
                            {
                                if (Moveable(Direction.RIGHT))
                                {
                                    BasicMove(Direction.RIGHT);
                                }
                                else
                                {
                                    if (Moveable(Direction.LEFT))
                                    {
                                        BasicMove(Direction.LEFT);
                                    }
                                    else
                                    {
                                        return;
                                    }
                                }
                            }
                        }
                    }
                    // 아래 위치
                    else
                    {
                        if (Moveable(Direction.DOWN))
                        {
                            BasicMove(Direction.DOWN);
                        }
                        else
                        {
                            int rand = Random.Range(0, 2);
                            if (rand == 0)
                            {
                                if (Moveable(Direction.LEFT))
                                {
                                    BasicMove(Direction.LEFT);
                                }
                                else
                                {
                                    BasicMove(Direction.RIGHT);
                                }
                            }
                            else
                            {
                                if (Moveable(Direction.RIGHT))
                                {
                                    BasicMove(Direction.RIGHT);
                                }
                                else
                                {
                                    BasicMove(Direction.LEFT);
                                }
                            }
                        }
                    }
                }
                // y 동일 선상 위치
                if (tracingTarget.transform.position.z == transform.position.z)
                {
                    // 우측에 위치
                    if (tracingTarget.transform.position.x > transform.position.x)
                    {
                        if (Moveable(Direction.RIGHT))
                        {
                            BasicMove(Direction.RIGHT);
                        }
                        else
                        {
                            int rand = Random.Range(0, 2);
                            if (rand == 0)
                            {
                                if (Moveable(Direction.UP))
                                {
                                    BasicMove(Direction.UP);
                                }
                                else
                                {
                                    if (Moveable(Direction.DOWN))
                                    {
                                        BasicMove(Direction.DOWN);
                                    }
                                    else
                                    {
                                        return;
                                    }
                                }
                            }
                            else
                            {
                                if (Moveable(Direction.DOWN))
                                {
                                    BasicMove(Direction.DOWN);
                                }
                                else
                                {
                                    if (Moveable(Direction.UP))
                                    {
                                        BasicMove(Direction.UP);
                                    }
                                    else
                                    {
                                        return;
                                    }
                                }
                            }
                        }
                    }
                    // 좌측에 위치
                    else
                    {
                        if (Moveable(Direction.LEFT))
                        {
                            BasicMove(Direction.LEFT);
                        }
                        else
                        {
                            int rand = Random.Range(0, 2);
                            if (rand == 0)
                            {
                                if (Moveable(Direction.UP))
                                {
                                    BasicMove(Direction.UP);
                                }
                                else
                                {
                                    if (Moveable(Direction.DOWN))
                                    {
                                        BasicMove(Direction.DOWN);
                                    }
                                    else
                                    {
                                        return;
                                    }
                                }
                            }
                            else
                            {
                                if (Moveable(Direction.DOWN))
                                {
                                    BasicMove(Direction.DOWN);
                                }
                                else
                                {
                                    if (Moveable(Direction.UP))
                                    {
                                        BasicMove(Direction.UP);
                                    }
                                    else
                                    {
                                        return;
                                    }
                                }
                            }
                        }
                    }

                }

                // 타겟이 우측 밑에 위치한 경우
                if (tracingTarget.transform.position.x > transform.position.x && tracingTarget.transform.position.z < transform.position.z)
                {
                    float xpos, zpos;
                    xpos = Mathf.Abs(tracingTarget.transform.position.x - transform.position.x);
                    zpos = Mathf.Abs(tracingTarget.transform.position.z - transform.position.z);
                    // 최적의 경로가 오른쪽
                    if (xpos > zpos)
                    {
                        if (Moveable(Direction.RIGHT))
                        {
                            BasicMove(Direction.RIGHT);
                        }
                        else if (Moveable(Direction.DOWN))
                        {
                            BasicMove(Direction.DOWN);
                        }
                        else
                        {
                            return;
                        }
                    }
                    // 최적의 경로가 아랫쪽
                    else if (xpos < zpos)
                    {
                        if (Moveable(Direction.DOWN))
                        {
                            BasicMove(Direction.DOWN);
                        }
                        else if (Moveable(Direction.RIGHT))
                        {
                            BasicMove(Direction.RIGHT);
                        }
                        else
                        {
                            return;
                        }

                    }
                    // 최적의 경로가 동일
                    else
                    {
                        int rand = Random.Range(0, 2);

                        if (rand == 0)
                        {
                            if (Moveable(Direction.RIGHT))
                            {
                                BasicMove(Direction.RIGHT);
                            }
                            else
                            {
                                if (Moveable(Direction.DOWN))
                                {
                                    BasicMove(Direction.DOWN);
                                }
                                else
                                {
                                    return;
                                }
                            }
                        }
                        else
                        {
                            if (Moveable(Direction.DOWN))
                            {
                                BasicMove(Direction.DOWN);
                            }
                            else
                            {
                                if (Moveable(Direction.RIGHT))
                                {
                                    BasicMove(Direction.RIGHT);
                                }
                                else
                                {
                                    return;
                                }
                            }
                        }
                    }
                }
                // 타겟이 우측 위에 위치한 경우
                if (tracingTarget.transform.position.x > transform.position.x && tracingTarget.transform.position.z > transform.position.z)
                {
                    float xpos, zpos;
                    xpos = Mathf.Abs(tracingTarget.transform.position.x - transform.position.x);
                    zpos = Mathf.Abs(tracingTarget.transform.position.z - transform.position.z);
                    // 최적의 경로가 오른쪽
                    if (xpos > zpos)
                    {
                        if (Moveable(Direction.RIGHT))
                        {
                            BasicMove(Direction.RIGHT);
                        }
                        else if (Moveable(Direction.UP))
                        {
                            BasicMove(Direction.UP);
                        }
                        else
                        {
                            return;
                        }
                    }
                    // 최적의 경로가 위쪽
                    else if (xpos < zpos)
                    {
                        if (Moveable(Direction.UP))
                        {
                            BasicMove(Direction.UP);
                        }
                        else if (Moveable(Direction.RIGHT))
                        {
                            BasicMove(Direction.RIGHT);
                        }
                        else
                        {
                            return;
                        }
                    }
                    // 최적의 경로가 동일
                    else
                    {
                        int rand = Random.Range(0, 2);

                        if (rand == 0)
                        {
                            if (Moveable(Direction.RIGHT))
                            {
                                BasicMove(Direction.RIGHT);
                            }
                            else
                            {
                                if (Moveable(Direction.DOWN))
                                {
                                    BasicMove(Direction.DOWN);
                                }
                                else
                                {
                                    return;
                                }
                            }
                        }
                        else
                        {
                            if (Moveable(Direction.DOWN))
                            {
                                BasicMove(Direction.DOWN);
                            }
                            else
                            {
                                if (Moveable(Direction.RIGHT))
                                {
                                    BasicMove(Direction.RIGHT);
                                }
                                else
                                {
                                    return;
                                }
                            }
                        }
                    }

                }
                // 타겟이 좌측 밑에 위치한 경우                                                                                            
                if (tracingTarget.transform.position.x < transform.position.x && tracingTarget.transform.position.z < transform.position.z)
                {
                    float xpos, zpos;
                    xpos = Mathf.Abs(tracingTarget.transform.position.x - transform.position.x);
                    zpos = Mathf.Abs(tracingTarget.transform.position.z - transform.position.z);
                    // 최적의 경로가 좌측
                    if (xpos > zpos)
                    {
                        if (Moveable(Direction.LEFT))
                        {
                            BasicMove(Direction.LEFT);
                        }
                        else if (Moveable(Direction.DOWN))
                        {
                            BasicMove(Direction.DOWN);
                        }
                        else
                        {
                            return;
                        }
                    }
                    // 최적의 경로가 아랫쪽
                    else if (xpos < zpos)
                    {
                        if (Moveable(Direction.DOWN))
                        {
                            BasicMove(Direction.DOWN);
                        }
                        else if (Moveable(Direction.LEFT))
                        {
                            BasicMove(Direction.LEFT);
                        }
                        else
                        {
                            return;
                        }

                    }
                    // 최적의 경로가 동일
                    else
                    {
                        int rand = Random.Range(0, 2);

                        if (rand == 0)
                        {
                            if (Moveable(Direction.LEFT))
                            {
                                BasicMove(Direction.LEFT);
                            }
                            else
                            {
                                if (Moveable(Direction.DOWN))
                                {
                                    BasicMove(Direction.DOWN);
                                }
                                else
                                {
                                    return;
                                }
                            }
                        }
                        else
                        {
                            if (Moveable(Direction.DOWN))
                            {
                                BasicMove(Direction.DOWN);
                            }
                            else
                            {
                                if (Moveable(Direction.LEFT))
                                {
                                    BasicMove(Direction.LEFT);
                                }
                                else
                                {
                                    return;
                                }
                            }
                        }
                    }
                }
                // 타겟이 좌측 위에 위치한 경우                                                                                            
                if (tracingTarget.transform.position.x < transform.position.x && tracingTarget.transform.position.z > transform.position.z)
                {
                    float xpos, zpos;
                    xpos = Mathf.Abs(tracingTarget.transform.position.x - transform.position.x);
                    zpos = Mathf.Abs(tracingTarget.transform.position.z - transform.position.z);
                    // 최적의 경로가 좌측
                    if (xpos > zpos)
                    {
                        if (Moveable(Direction.LEFT))
                        {
                            BasicMove(Direction.LEFT);
                        }
                        else if (Moveable(Direction.UP))
                        {
                            BasicMove(Direction.UP);
                        }
                        else
                        {
                            return;
                        }
                    }
                    // 최적의 경로가 위쪽
                    else if (xpos < zpos)
                    {
                        if (Moveable(Direction.UP))
                        {
                            BasicMove(Direction.UP);
                        }
                        else if (Moveable(Direction.LEFT))
                        {
                            BasicMove(Direction.LEFT);
                        }
                        else
                        {
                            return;
                        }

                    }
                    // 최적의 경로가 동일
                    else
                    {
                        int rand = Random.Range(0, 2);

                        if (rand == 0)
                        {
                            if (Moveable(Direction.LEFT))
                            {
                                BasicMove(Direction.LEFT);
                            }
                            else
                            {
                                if (Moveable(Direction.UP))
                                {
                                    BasicMove(Direction.UP);
                                }
                                else
                                {
                                    return;
                                }
                            }
                        }
                        else
                        {
                            if (Moveable(Direction.UP))
                            {
                                BasicMove(Direction.UP);
                            }
                            else
                            {
                                if (Moveable(Direction.LEFT))
                                {
                                    BasicMove(Direction.LEFT);
                                }
                                else
                                {
                                    return;
                                }
                            }
                        }
                    }
                }

        }
    }


    // 공격 로직
    public void Attacking()
    {
        if(prevState == State.Attack)
        {
            StartCoroutine(WaitForIt());
        }
        else
        {
            Debug.Log("공격 취소");
            atkTrigger.GetComponent<AttackTrigger>().activeMode = false;
        }
    }

    IEnumerator WaitForIt()
    {
        yield return new WaitForSeconds(0.3f);
        atkTrigger.GetComponent<AttackTrigger>().activeMode = true;
    }
}
//left: -
//right : +
//up : +
//down : -