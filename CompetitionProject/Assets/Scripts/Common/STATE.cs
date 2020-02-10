public partial class CameraController
{
    //Camera's state machine settings
    enum STATE
    {
        SLIDE = 0,
        TOUCH = 1,
        MOVE = 2,
        ZOOM = 3,
        MOVE_ZOOM = 4,
        IDLE = 5
    }
}
