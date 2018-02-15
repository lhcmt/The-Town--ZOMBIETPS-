using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInput : MonoBehaviour {

    public CharacterMovement characterMove{get;protected set;}
    public WeaponHandler weaponHandler { get; protected set; }


    [System.Serializable]
    public class InputSettings
    {
        public string verticalAxis = "Vertical";
        public string horizontalAxis = "Horizontal";
        public string jumpButton = "Jump";
        public string reloadButton = "Reload";
        public string aimButton = "Fire2";
        public string fireButton = "Fire1";
        public string dropWeaponButton = "DropWeapon";
        public string switchWeaponButton = "SwitchWeapon";
        public string SprintButton = "Fire3";
        public string TaskButtun = "TaskMenu";

    }
    [SerializeField]
    public InputSettings inputs;

    [System.Serializable]
    public class OtherSettings
    {
        public float lookSpeed = 5.0f;
        public float lookDistance = 10.0f;
        public bool requireInputForTurn = true;
        public LayerMask aimDetectionLayers;
    }
    [SerializeField]
    public OtherSettings other;

    public Camera TPSCamera;
    
    public bool debugAim;

    public Transform spine;
    public bool aiming { get; set; }

    //修复跳跃问题
    bool canJump = true;

    void Start()
    {
        characterMove = GetComponent<CharacterMovement>();

        weaponHandler = GetComponent<WeaponHandler>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        CharacterLogic();
        CameraLookLogic();
        WeaponLogic();
    }

    void LateUpdate()
    {
        if(weaponHandler)
        {
            if(weaponHandler.currentWeapon)
            {
                if (aiming)
                    PositionSpine();
            }
        }
    }

    //处理角色移动的逻辑
    void CharacterLogic()
    {
        if ((!characterMove))
            return;
        if (Input.GetButtonDown(inputs.jumpButton) && canJump)
        {
            characterMove.Jump();
            canJump = false;
            StartCoroutine(CanJump());
        }
        characterMove.isSprint = Input.GetButton(inputs.SprintButton);
        characterMove.Animate(Input.GetAxis(inputs.verticalAxis), Input.GetAxis(inputs.horizontalAxis));
        //获取跳跃按键

        
    }
    IEnumerator CanJump()
    {
        yield return new WaitForSeconds(characterMove.movement.jumpTime+0.1f);
        canJump = true;
    }
    //处理摄像机逻辑
     void CameraLookLogic()
    {
        if (!TPSCamera)
            return;

        if (other.requireInputForTurn)
        {
            if (Input.GetAxis(inputs.horizontalAxis) != 0 || Input.GetAxis(inputs.verticalAxis) != 0)
            {
                CharacterLook();
            }
        }
        else
        {
            CharacterLook();
        }

    }
    //处理所有武器的逻辑
    void WeaponLogic()
     {
         if (!weaponHandler)
             return;

        aiming = (Input.GetButton(inputs.aimButton) || debugAim) && !characterMove.isSprint && weaponHandler.currentWeapon;
        if(weaponHandler.currentWeapon)
        {
           
            weaponHandler.Aim(aiming);
            other.requireInputForTurn = !aiming;
            //开火按钮
            weaponHandler.FIngerOnTriger(Input.GetButton(inputs.fireButton));

            if (Input.GetButtonDown(inputs.reloadButton))
                weaponHandler.Reload();

            if (Input.GetButtonDown(inputs.dropWeaponButton))
                weaponHandler.DropCurWeapon();


        }
        if (Input.GetButtonDown(inputs.switchWeaponButton))
            weaponHandler.SwitchWeapons();

        if (!weaponHandler.currentWeapon)
            return;
        //武器瞄准射线，从Camera向屏幕中央发射
        Vector2 v = new Vector2(Screen.width / 2, Screen.height / 2);
        weaponHandler.currentWeapon.shootRay = Camera.main.ScreenPointToRay(v);//new Ray(TPSCamera.transform.position, TPSCamera.transform.forward);
        
     }

    //make the character look at a forward from the camera
    void CharacterLook()
    {
        Transform mainCamT = TPSCamera.transform;
        Transform pivotT = mainCamT.parent;
        Vector3 pivotPos = pivotT.position;
        Vector3 lookTarget = pivotPos + (pivotT.forward * other.lookDistance);
        Vector3 thisPos = transform.position;
        Vector3 lookDir = lookTarget - thisPos;
        Quaternion lookRot = Quaternion.LookRotation(lookDir);

        lookRot.x = 0;
        lookRot.z = 0;

        Quaternion newRotation = Quaternion.Lerp(transform.rotation, lookRot, Time.deltaTime * other.lookSpeed);
        transform.rotation = newRotation;

    }

    //瞄准时候使身体朝向有瞄准的摄像方向
    void PositionSpine()
    {
        if (!spine || !weaponHandler.currentWeapon || !TPSCamera)
            return;

        //RaycastHit hit;
        Transform mainCamT = TPSCamera.transform;
        Vector3 mainCamPos = mainCamT.position;
        Vector3 dir = mainCamT.forward;
        Ray ray = new Ray(mainCamPos, dir);

         //由于Spine的朝向和rootjni不一样，需要做个旋转，这模型很奇怪
        spine.LookAt(ray.GetPoint(400f));
        spine.localEulerAngles = spine.localEulerAngles + new Vector3(0, 0, -90f);

        Vector3 eulerAngleOffset = weaponHandler.currentWeapon.userSettings.spineRotation;
        spine.Rotate(eulerAngleOffset);
    }
}
