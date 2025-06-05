using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;
    public float runspeed = 40f;

    float horizontalMove = 0f;
    bool jump = false;

    public static bool inputLocked = false; // Global flag to lock player input during quiz

    void Update()
    {
        if (inputLocked) return;

        horizontalMove = Input.GetAxisRaw("Horizontal") * runspeed;
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
    }

    private void FixedUpdate()
    {
        if (inputLocked) return;

        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
    }
}
