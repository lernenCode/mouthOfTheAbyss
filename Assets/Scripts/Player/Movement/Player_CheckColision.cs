using UnityEngine;

public class Player_CheckColision : MonoBehaviour
{
    [Header("CheckRoof")]
    public static bool isRoof;

    [Header("CheckGround")]
    public static bool isGround;

    [Header("CheckPlatform")]
    public static bool isPlatform;
    public static bool isPlatformGrounded;
    public static bool isPlatformRight;
    public static bool isPlatformLeft;

    [Header("WallCheck")]
    public static bool isWall;
    public static bool isWallRight;
    public static bool isWallLeft;

    [Header("Npc")]
    public static bool inNpcRange;

    [Header("Em comun")]
    [SerializeField] private LayerMask whatisGround;
    [SerializeField] private LayerMask whatIsCatchable;
    [SerializeField] private LayerMask whatIsPlatform;
    [SerializeField] private LayerMask whatIsNPC;
    [SerializeField] private BoxCollider2D boxCol2D;

    private void Update()
    {
        // Npc
        inNpcRange = npcTalkRange();

        // Ground
        isGround = isGrounded();

        // Roof
        isRoof = isRoofed();

        // Wall
        isWallLeft = isWalledLeft();
        isWallRight = isWalledRight();

        // platform
        isPlatformGrounded = isPlatformed();
        isPlatformLeft = isPlatformedLeft();
        isPlatformRight = isPlatformedRight();

        if (isPlatformedRight() || isPlatformedLeft() || isPlatformed()) { isPlatform = true; } else { isPlatform = false; }
        if (isWalledRight() || isWalledLeft()) { isWall = true; } else { isWall = false; }
    }

    #region CheckRoof
    private bool isRoofed()
    {
        RaycastHit2D isRoof = Physics2D.BoxCast(boxCol2D.bounds.center, boxCol2D.bounds.size, 0f, Vector2.up, 0.1f, whatisGround | whatIsCatchable | whatIsPlatform);
        return isRoof.collider != null;
    }
    #endregion

    #region CheckGround
    private bool isGrounded()
    {
        RaycastHit2D isGround = Physics2D.BoxCast(boxCol2D.bounds.center, boxCol2D.bounds.size, 0f, Vector2.down, 0.1f, whatisGround | whatIsCatchable | whatIsPlatform);
        return isGround.collider != null;
    }
    #endregion

    #region CheckNPC

    private bool npcTalkRange()
    {
        RaycastHit2D isNpc = Physics2D.BoxCast(boxCol2D.bounds.center, boxCol2D.bounds.size, 0f, Vector2.zero, 0.1f, whatIsNPC);
        return isNpc.collider != null;
    }
    #endregion


    #region CheckWall

    private bool isWalledLeft()
    {
        RaycastHit2D isWallLeft = Physics2D.BoxCast(boxCol2D.bounds.center, boxCol2D.bounds.size, 0f, Vector2.left, 0.1f, whatisGround | whatIsCatchable | whatIsPlatform);
        return isWallLeft.collider != null;
    }

    private bool isWalledRight()
    {
        RaycastHit2D isWallRight = Physics2D.BoxCast(boxCol2D.bounds.center, boxCol2D.bounds.size, 0f, Vector2.right, 0.1f, whatisGround | whatIsCatchable | whatIsPlatform);
        return isWallRight.collider != null;
    }
    #endregion

    #region CheckPlatform
    private bool isPlatformed()
    {
        RaycastHit2D isPlatformGrounded = Physics2D.BoxCast(boxCol2D.bounds.center, boxCol2D.bounds.size, 0f, Vector2.down, 0.1f, whatIsPlatform);
        return isPlatformGrounded.collider != null;
    }

    private bool isPlatformedLeft()
    {
        RaycastHit2D isPlatformLeft = Physics2D.BoxCast(boxCol2D.bounds.center, boxCol2D.bounds.size, 0f, Vector2.left, 0.1f, whatIsPlatform);
        return isPlatformLeft.collider != null;
    }

    private bool isPlatformedRight()
    {
        RaycastHit2D isPlatformRight = Physics2D.BoxCast(boxCol2D.bounds.center, boxCol2D.bounds.size, 0f, Vector2.right, 0.1f, whatIsPlatform);
        return isPlatformRight.collider != null;
    }
    #endregion
}
