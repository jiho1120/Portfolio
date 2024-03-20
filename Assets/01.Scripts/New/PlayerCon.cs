using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCon : MonoBehaviour
{
    public Transform characterBody;
    public Transform cameraArm;
    public Transform fist;
    PlayerAnimator playerAnimator;
    public int PlayerLayer { get; private set; }


    private bool isRun = false;
    protected bool isDead = false;

    public float Speed { get; private set; }
    private float speed;

    public void Init()
    {
        if (playerAnimator == null)
        {
            playerAnimator = GetComponent<PlayerAnimator>();
        }
        speed = DataManager.Instance.gameData.playerData.playerStat.movementSpeed;
        playerAnimator.Init();
        PlayerLayer = 1 << LayerMask.NameToLayer("Enemy");

    }
    void FixedUpdate()
    {
        Move();
    }
    private void Move()
    {
        speed = (isRun) ? (Speed * 1.5f) : Speed;
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        float percent = ((isRun) ? 1 : 0.5f) * moveInput.magnitude;
        playerAnimator.WalkOrRun(percent);

        Vector3 lookForward = new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized;
        Vector3 lookRight = new Vector3(cameraArm.right.x, 0f, cameraArm.right.z).normalized;
        Vector3 moveDir = lookForward * moveInput.y + lookRight * moveInput.x;
        playerAnimator.MoveAnim(moveInput.y, moveInput.x);

        characterBody.forward = lookForward;
        transform.position += moveDir * speed * Time.deltaTime;
    }
}
