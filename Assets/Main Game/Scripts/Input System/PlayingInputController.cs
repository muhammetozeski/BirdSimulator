using UnityEngine;
using UnityEngine.InputSystem;

//note: this script taken from a space ship game. so some variable names might be related with ships

public class PlayingInputController : BirdMovingController
{
    [SerializeField] private PlayerInput playerInput;

    [Tooltip("Input Action Map Name")]
    string PlayerMapName = "Player";
    [Tooltip("Pm = Player map. Input Action Name which is in the \"player\" input action map")]
    string PmMove = "Move", PmLook = "Look", PmFire = "Fire";

    InputAction Move, Look, Fire;
    InputActionMap PlayerMap;

    Vector2 MousePosition;

    Vector2 RotateTo;

    [SerializeField] private RectTransform MouseTarget;
    [SerializeField] private RectTransform ShipsTarget;
    [SerializeField] private RectTransform Rope;

    //debug purpose:

    [Tooltip("Debug purpose")]
    [SerializeField] protected bool LetMove;
    void Awake()
    {
        base.Awake();
        #region Assigns
        Move = playerInput.actions.FindAction(PlayerMapName + "/" + PmMove);
        Look = playerInput.actions.FindAction(PlayerMapName + "/" + PmLook);
        Fire = playerInput.actions.FindAction(PlayerMapName + "/" + PmFire);
        PlayerMap = playerInput.actions.FindActionMap(PlayerMapName);
        #endregion
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update()
    {
        MousePosition = Look.ReadValue<Vector2>() * spaceShipSettings.MouseSpeedMultiplier;
    }

    void FixedUpdate()
    {
        RotateTo = MouseTarget.anchoredPosition;
        RotateTargets(MousePosition);

        RotateShip(RotateTo);
        if (LetMove)
        {
            MoveShip3d(Move.ReadValue<Vector2>());
        }
    }

    
    protected void RotateTargets(Vector2 rotateDir)
    {
        Vector2 ancPos = MouseTarget.anchoredPosition;
        float l = spaceShipSettings.TargetIconLerper;
        float _speed = spaceShipSettings.ShipRotationMultiplier / l * (l - 1) -
            (spaceShipSettings.ShipRotationMultiplier / l * (l - 1) / spaceShipSettings.TargetClamp) * ancPos.magnitude +
            spaceShipSettings.ShipRotationMultiplier / l * 1;

        Vector2 pos = ancPos + rotateDir * _speed; /* (*Time.deltaTime) ???*/

        pos = Vector2.ClampMagnitude(pos, spaceShipSettings.TargetClamp);
        MouseTarget.anchoredPosition = pos;


        float angle = MainTools.Vector2toAngle180(-MouseTarget.anchoredPosition);
        Rope.localEulerAngles = new Vector3(0, 0, angle);
        Rope.sizeDelta = new Vector2(Rope.sizeDelta.x, MouseTarget.anchoredPosition.magnitude);
    }


    // are these really necessary? idk
    private void OnEnable()
    { PlayerMap.Enable(); }
    private void OnDisable()
    { PlayerMap.Disable(); }

}
