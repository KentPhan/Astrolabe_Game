using UnityEngine;

/// <summary>
/// Player Entity
///
/// Ripped a lot of the camera script from some dude online
/// </summary>
public class PlayerEntity : Entity
{


    public enum ControllerScheme { Mouse = 0, Gyroscope = 1 }

    public ControllerScheme control = ControllerScheme.Mouse;

    public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
    public RotationAxes axes = RotationAxes.MouseXAndY;

    public CursorLockMode wantedMode;

    // Mouse
    public float sensitivityX = 15F;
    public float sensitivityY = 15F;
    public float minimumX = -360F;
    public float maximumX = 360F;
    public float minimumY = -60F;
    public float maximumY = 60F;
    float rotationX = 0F;
    float rotationY = 0F;
    Quaternion originalRotation;


    //Gyroscope
    private Gyroscope m_Gyro;

    void Start()
    {
        Cursor.lockState = wantedMode;

        originalRotation = transform.localRotation;

        m_Gyro = Input.gyro;
        m_Gyro.enabled = true;
    }


    void Update()
    {
        if (control == ControllerScheme.Mouse)
        {
            if (axes == RotationAxes.MouseXAndY)
            {
                // Read the mouse input axis
                rotationX += Input.GetAxis("Mouse X") * sensitivityX;
                rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
                rotationX = ClampAngle(rotationX, minimumX, maximumX);
                rotationY = ClampAngle(rotationY, minimumY, maximumY);
                Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
                Quaternion yQuaternion = Quaternion.AngleAxis(rotationY, -Vector3.right);
                transform.localRotation = originalRotation * xQuaternion * yQuaternion;
            }
            else if (axes == RotationAxes.MouseX)
            {
                rotationX += Input.GetAxis("Mouse X") * sensitivityX;
                rotationX = ClampAngle(rotationX, minimumX, maximumX);
                Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
                transform.localRotation = originalRotation * xQuaternion;
            }
            else
            {
                rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
                rotationY = ClampAngle(rotationY, minimumY, maximumY);
                Quaternion yQuaternion = Quaternion.AngleAxis(-rotationY, Vector3.right);
                transform.localRotation = originalRotation * yQuaternion;
            }
        }
        else
        {
            //Why the fuck does this work. I have no idea.
            Quaternion gyroRotFix = new Quaternion(-0.5f, 0.5f, 0.5f, 0.5f);
            Quaternion gyroRot = gyroRotFix * Input.gyro.attitude;
            gyroRot = new Quaternion(gyroRot.x, gyroRot.y, -gyroRot.z, -gyroRot.w);
            transform.rotation = gyroRot;
        }

    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}
