using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerInputKoo : MonoBehaviour
{
    [Header("�÷��̾� ����")]
    [Range(0f, 10f)]
    public float maxSpeed = 0f;

    private Rigidbody playerRb;
    private NavMeshAgent playerNb;
    private Animator playerAnim;

    // ���� ���ϴ� ������ 
    private GameObject myDestinationTile;
    private enum MoveDirection { LEFT, RIGHT, UP, DOWN };

    // �÷��̾� �� Ȯ��
    public bool checkTurn;


    // ���� Ÿ�� ��ġ
    public GameObject StartTile;

    // �⺻ Ÿ�ϻ��� ����
    public float tile_interval;

    TurnManager myTurnManager;

    private NavMeshAgent navMeshAgent;
    private void Awake()
    {
        playerRb = GetComponent<Rigidbody>();
        playerNb = GetComponent<NavMeshAgent>();
        playerAnim = GetComponent<Animator>();

        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        myTurnManager = GameObject.Find("TurnManager").GetComponent<TurnManager>();
        transform.position = StartTile.transform.position;

        // �� �̵��Ҷ� ����� ��.
        myTurnManager.SettingZombieArray();
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
            //Debug.Log("Moving");

            if (new Vector3(transform.position.x, 0, transform.position.z) == new Vector3(myDestinationTile.transform.position.x, 0, myDestinationTile.transform.position.z))
            {
                Debug.Log("arrive");
                playerAnim.SetBool("walk", false);
                checkTurn = true;
            }
        }

       

    }

    //������ ���� �κ� �ӽ÷� ����

    void PlayerMovement()
    {
        if (Input.GetKey(KeyCode.A))     //Left (anim:1)
        {
            if (CheckWalkable(MoveDirection.LEFT))
            {

                transform.rotation = Quaternion.Euler(0, 450, 0);
                playerAnim.SetBool("walk", true);
                myTurnManager.SetZombieTurn(true);
                checkTurn = false;
            }
        }
        if (Input.GetKey(KeyCode.D))    //Right (anim:2)
        {
            if (CheckWalkable(MoveDirection.RIGHT))
            {
                transform.rotation = Quaternion.Euler(0, 270, 0);
                playerAnim.SetBool("walk", true);

                myTurnManager.SetZombieTurn(true);
                checkTurn = false;
            }
        }
        if (Input.GetKey(KeyCode.W))    //Up (anim:3)
        {
            if (CheckWalkable(MoveDirection.UP))
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
                playerAnim.SetBool("walk", true);

                myTurnManager.SetZombieTurn(true);
                checkTurn = false;
            }
        }
        if (Input.GetKey(KeyCode.S))    //Down (anim:4)
        {
            if (CheckWalkable(MoveDirection.DOWN))
            {
                transform.rotation = Quaternion.Euler(0, 360, 0);
                playerAnim.SetBool("walk", true);

                myTurnManager.SetZombieTurn(true);
                checkTurn = false;
            }
        }

        if (Input.GetKey(KeyCode.Space))
        {
           
                playerAnim.SetBool("item", true);
                
            
        }
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
                            playerNb.destination = hit.transform.position;
                            myDestinationTile = hit.transform.gameObject;
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
                            playerNb.destination = hit.transform.position;
                            myDestinationTile = hit.transform.gameObject;
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
                            playerNb.destination = hit.transform.position;
                            myDestinationTile = hit.transform.gameObject;
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
                            playerNb.destination = hit.transform.position;
                            myDestinationTile = hit.transform.gameObject;
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


}
