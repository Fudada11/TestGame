using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //玩家移动速度//
    public float Speed = 6f;
    private Rigidbody rb;
    private Animator anim;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Move(horizontal, vertical);
        Turning();
        Animating(horizontal,vertical);
    }
    void Move(float horizontal, float vertical)
    {
        Vector3 movementvector = new Vector3(horizontal, 0, vertical);
        movementvector = movementvector.normalized * Speed * Time.deltaTime;
        rb.MovePosition(transform.position + movementvector);
    }
    void Turning()
    {
        //获取射线：主相机（鼠标位置）
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        //射线监测
        RaycastHit floorhit;
        int floorLayer = LayerMask.GetMask("Floor");
        bool isTouchFloor=Physics.Raycast(cameraRay, out floorhit,100,floorLayer);
        if(isTouchFloor)
        {
            Vector3 turnV3 = floorhit.point - transform.position;
            turnV3.y = 0;
            Quaternion turnQuaternion = Quaternion.LookRotation(turnV3);
            rb.MoveRotation(turnQuaternion);
        }
    }
    void Animating(float horizontal, float vertical)
    {
        bool isW = false;
        if (horizontal != 0 || vertical != 0)
            isW = true;
        anim.SetBool("isWalking",isW);
    }
}
