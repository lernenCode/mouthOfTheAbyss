using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_CheckColision : MonoBehaviour
{
    [Header("CheckGround")]
    public static bool isGround;

    [Header("WallCheck")]
    public static bool isWall;
    public static bool isWallRight;
    public static bool isWallLeft;

    [Header("Em comun")]
    [SerializeField] private LayerMask whatisGround;
    [SerializeField] private LayerMask whatIsCatchable;
    [SerializeField] private BoxCollider2D boxCol2D;

    #region CheckGround

    private void Update()
    {
        isGround = isGrounded();
        isWallLeft = isWalledLeft();
        isWallRight =  isWalledRight();
        if(isWalledRight() || isWalledLeft()) { isWall = true; } else { isWall = false; }
    }
    private bool isGrounded()
    {
        RaycastHit2D isGround = Physics2D.BoxCast(boxCol2D.bounds.center, boxCol2D.bounds.size, 0f, Vector2.down, 0.1f, whatisGround | whatIsCatchable);
        return isGround.collider != null;
    }
    #endregion

    #region CheckWall
    private bool isWalledLeft()
    {
        RaycastHit2D isWallLeft = Physics2D.BoxCast(boxCol2D.bounds.center, boxCol2D.bounds.size, 0f, Vector2.left, 0.1f, whatisGround | whatIsCatchable);
        return isWallLeft.collider != null;
    }

    private bool isWalledRight()
    {
        RaycastHit2D isWallRight = Physics2D.BoxCast(boxCol2D.bounds.center, boxCol2D.bounds.size, 0f, Vector2.right, 0.1f, whatisGround | whatIsCatchable);
        return isWallRight.collider != null;
    }
    #endregion
}
