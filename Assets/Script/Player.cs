using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    GameObject effect;
    Animator animator = null;

    public Transform fistpose;

    public float jump = 5f;

    public float Atk = 10f;

    public float RunAtk = 0f;

    public float JumAtk = 0f;

    //캐릭터 직선 이동 속도 (걷기)
    public float walkMoveSpd = 2.0f;

    //캐릭터 직선 이동 속도 (달리기)
    public float runMoveSpd = 3.5f;

    //캐릭터 회전 이동 속도 
    public float rotateMoveSpd = 100.0f;

    //캐릭터 회전 방향으로 몸을 돌리는 속도
    public float rotateBodySpd = 2.0f;

    //캐릭터 이동 속도 증가 값
    public float moveChageSpd = 0.1f;

    //???? ĳ???? ??? ???? ?? 
    private Vector3 vecNowVelocity = Vector3.zero;

    //현재 캐릭터 이동 방향 벡터 
    private Vector3 vecMoveDirection = Vector3.zero;

    //CharacterController 캐싱 준비
    private CharacterController controllerCharacter = null;

    //캐릭터 CollisionFlags 초기값 설정
    private CollisionFlags collisionFlagsCharacter = CollisionFlags.None;

    public float spd = 0;
    public float horizontal;

    public bool isAtk = false;

    public Camera mainCam;

    // Start is called before the first frame update
    void Start()
    {
        //CharacterController 캐싱
        controllerCharacter = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //ĳ???? ??? 
        Move();
        // Debug.Log(getNowVelocityVal());
        //캐릭터 방향 변경 
        vecDirectionChangeBody();

        // animator.SetBool("forntBack_walk", false);

        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(PlayerAtk());
        }
    
        float v = Input.GetAxis("Vertical");

        if (v > 0)
        {
            spd += Input.GetAxis("Vertical") * Time.deltaTime;
        }
        else
        {
            spd = 0;
        }
        animator.SetFloat("spd", spd);
    }

    /// <summary>
    /// 이동함수 입니다 캐릭터
    /// </summary>
    void Move()
    {
        Transform CameraTransform = mainCam.transform;

        //???? ???? ???? ?????? ????? ?? ???????.
        Vector3 forward = CameraTransform.TransformDirection(Vector3.forward);
        forward.y = 0.0f;

        //forward.z, forward.x
        Vector3 right = new Vector3(forward.z, 0.0f, -forward.x);

        //키입력 
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        //케릭터가 이동하고자 하는 방향 
        Vector3 targetDirection = horizontal * right + vertical * forward;



        //???? ?????? ?????? ????? ???????? ??? 

        vecMoveDirection = Vector3.RotateTowards(vecMoveDirection, targetDirection, rotateMoveSpd * Mathf.Deg2Rad * Time.deltaTime, 1000.0f);
        vecMoveDirection = vecMoveDirection.normalized;
        //캐릭터 이동 속도
        float spd = walkMoveSpd;

        // ?????? ??? ??
        Vector3 moveAmount = (vecMoveDirection * spd * Time.deltaTime);

        collisionFlagsCharacter = controllerCharacter.Move(moveAmount);

    }


    /// <summary>
    /// 현재 내 케릭터 이동 속도 가져오는 함  
    /// </summary>
    /// <returns>float</returns>
    float getNowVelocityVal()
    {
        //현재 캐릭터가 멈춰 있다면 
        if (controllerCharacter.velocity == Vector3.zero)
        {
            //반환 속도 값은 0
            vecNowVelocity = Vector3.zero;
        }
        else
        {

            //반환 속도 값은 현재 /
            Vector3 retVelocity = controllerCharacter.velocity;
            retVelocity.y = 0.0f;

            vecNowVelocity = Vector3.Lerp(vecNowVelocity, retVelocity, moveChageSpd * Time.fixedDeltaTime);

        }
        //거리 크기
        return vecNowVelocity.magnitude;
    }

    /// <summary>
	/// GUI SKin
	/// </summary>
    //private void OnGUI()
    //{
    //    if (controllerCharacter != null && controllerCharacter.velocity != Vector3.zero)
    //    {
    //        var labelStyle = new GUIStyle();
    //        labelStyle.fontSize = 25;
    //        labelStyle.normal.textColor = Color.white;
    //        //캐릭터 현재 속도
    //        float _getVelocitySpd = getNowVelocityVal();
    //        GUILayout.Label("현재속도 : " + _getVelocitySpd.ToString(), labelStyle);

    //        //현재 캐릭터 방향 + 크기
    //        GUILayout.Label("현재벡터 : " + controllerCharacter.velocity.ToString(), labelStyle);

    //        //현재  재백터 크기 속도
    //        GUILayout.Label("현재백터 크기 속도 : " + vecNowVelocity.magnitude.ToString(), labelStyle);

    //    }
    //}
    /// <summary>
    /// 캐릭터 몸통 벡터 방향 함수
    /// </summary>
    void vecDirectionChangeBody()
    {
        //캐릭터 이동 시
        if (getNowVelocityVal() > 0.0f)
        {
            //내 몸통  바라봐야 하는 곳은 어디?
            Vector3 newForward = controllerCharacter.velocity;
            newForward.y = 0.0f;

            //내 캐릭터 전면 설정 
            transform.forward = Vector3.Lerp(transform.forward, newForward, rotateBodySpd * Time.deltaTime);

        }
    }

    IEnumerator PlayerAtk()
    {
        isAtk = true;
        GameObject Effect = Instantiate(effect, fistpose);
        Effect.transform.localPosition = new Vector3(0, 0, 0);
        animator.SetTrigger("Attack");
        RunAtk = Atk * 3;
        JumAtk = RunAtk * Atk * 2;
        yield return new WaitForSeconds(1.5f);
        Destroy(Effect, 1);
        isAtk = false;
    }

}
