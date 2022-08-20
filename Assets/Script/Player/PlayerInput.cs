using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerInput : MonoBehaviour
{
    [Header("플레이어 세팅")]
    [Range(0f, 10f)]
    public float maxSpeed = 0f;

    private Rigidbody playerRb;
    private NavMeshAgent playerNb;
    private Animator playerAnim;

    // 현재 향하는 목적지 
    private GameObject myDestinationTile;
    private enum MoveDirection { LEFT, RIGHT, UP, DOWN };

    // 플레이어 턴 확인
    public bool checkTurn;

    // 시작 타일 위치
    public GameObject StartTile;

    // 기본 타일사이 간격
    public float tile_interval;

    TurnManager myTurnManager;

    public GameObject ITEM_cellphone;

    private bool useItem;

    private void Awake()
    {  
        playerRb = GetComponent<Rigidbody>();
        playerNb = GetComponent<NavMeshAgent>();
        playerAnim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        myDestinationTile = null;

        myTurnManager = GameObject.Find("TurnManager").GetComponent<TurnManager>();

        transform.position = StartTile.transform.position;

        // 맵 이동할때 사용할 것.
        myTurnManager.SettingZombieArray();

        useItem = false;

        playerNb.updateRotation = false;

        playerNb.height = 0.5f;

        playerNb.baseOffset = 0.05f;
    }

    // Update is called once per frame
    void Update()
    {
        if (checkTurn)
        {
            PlayerMovement();
        }
        else if (!checkTurn)
        {
            if (!playerNb.pathPending)
                {
                    if(playerNb.remainingDistance<=playerNb.stoppingDistance)
                    {
                        if(!playerNb.hasPath /*|| playerNb.velocity.sqrMagnitude <= 0f*/)
                        {
                            StartCoroutine(WaitForSecond(0.3f));
                        }
                    }
                }
        }
    }
    IEnumerator WaitForSecond(float Second)
    {
        yield return new WaitForSeconds(Second);
        checkTurn = true;
        playerAnim.SetBool("walk", false);
        playerAnim.SetBool("item", false);
    }


    void PlayerMovement()
    {
        if (Input.GetKeyDown(KeyCode.A))     //Left (anim:1)
        {
            if (CheckWalkable(MoveDirection.LEFT))
            {
                transform.rotation = Quaternion.Euler(0, 450, 0);
                playerAnim.SetBool("walk", true);
                myTurnManager.SetZombieTurn(true);
                checkTurn = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.D))    //Right (anim:2)
        {
            if (CheckWalkable(MoveDirection.RIGHT))
            {
                transform.rotation = Quaternion.Euler(0, 270, 0);
                playerAnim.SetBool("walk", true);
                myTurnManager.SetZombieTurn(true);
                checkTurn = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.W))    //Up (anim:3)
        {
            if (CheckWalkable(MoveDirection.UP))
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
                playerAnim.SetBool("walk", true);
                myTurnManager.SetZombieTurn(true);
                checkTurn = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.S))    //Down (anim:4)
        {
            if (CheckWalkable(MoveDirection.DOWN))
            {
                transform.rotation = Quaternion.Euler(0, 360, 0);
                playerAnim.SetBool("walk", true);
                myTurnManager.SetZombieTurn(true);
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            if(ITEM_cellphone)
            {
                playerAnim.SetBool("item", true);
                StartCoroutine(WaitAttack());
            }
        }
    }
    IEnumerator WaitAttack()
    {
        yield return new WaitForSeconds(0.8f);
        Instantiate(ITEM_cellphone, new Vector3(transform.position.x, transform.position.y, transform.position.z + (-3 * tile_interval)), Quaternion.identity);
        checkTurn = false;
    }

    private bool CheckWalkable(MoveDirection direction)
    {
        switch (direction)
        {
            case MoveDirection.LEFT:
                {
                    RaycastHit hit;

                    Physics.Raycast(transform.position + (Vector3.right * tile_interval), Vector3.down, out hit, 5.0f);
                    if (hit.transform.gameObject != null)
                    {
                        if (hit.collider.gameObject.layer == 6)
                        {
                            BasicMovement(hit.transform.position);
                            //playerNb.destination = hit.transform.position;
                            //myDestinationTile = hit.transform.gameObject;
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            case MoveDirection.RIGHT:
                {

                    RaycastHit hit;

                    Physics.Raycast(transform.position + (Vector3.right * -tile_interval), Vector3.down, out hit, 5.0f);

                    if (hit.transform.gameObject != null)
                    {
                        if (hit.transform.gameObject.layer == 6)
                        {
                            BasicMovement(hit.transform.position);
                            //playerNb.destination = hit.transform.position;
                            //myDestinationTile = hit.transform.gameObject;
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }

                }
            case MoveDirection.UP:
                {

                    RaycastHit hit;

                    Physics.Raycast(transform.position + (Vector3.forward * -tile_interval), Vector3.down, out hit, 5.0f);

                    if (hit.transform.gameObject != null)
                    {
                        if (hit.transform.gameObject.layer == 6)
                        {
                            BasicMovement(hit.transform.position);
                            //playerNb.destination = hit.transform.position;
                            //myDestinationTile = hit.transform.gameObject;
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            case MoveDirection.DOWN:
                {

                    RaycastHit hit;

                    Physics.Raycast(transform.position + (Vector3.forward * tile_interval), Vector3.down, out hit, 5.0f);
                    if (hit.transform.gameObject != null)
                    {
                        if (hit.transform.gameObject.layer == 6)
                        {
                            BasicMovement(hit.transform.position);

                            //playerNb.destination = hit.transform.position;
                            //myDestinationTile = hit.transform.gameObject;
                            return true;
                        }
                        else
                        {
                            return false;
                        }
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

    private void BasicMovement(Vector3 destination)
    {
        playerNb.SetDestination(destination);
    }
}
