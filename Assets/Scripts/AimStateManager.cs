using Unity.Cinemachine;
using UnityEngine;

public class AimStateManager : MonoBehaviour
{
    [SerializeField] Transform camFollowPos;

    CinemachineInputAxisController _xAxis, _yAxis;

    // // Start is called once before the first execution of Update after the MonoBehaviour is created
    // void Start()
    // {
    //     
    // }
    //
    // // Update is called once per frame
    // void Update()
    // {
    //   xAxis.Update(Time.deltaTime);
    //   yAxis.Update(Time.deltaTime);
    // }
    public InputAxis verticalAxis = new InputAxis { Value = 0f };
    public InputAxis horizontalAxis = new InputAxis { Value = 0f };
    public float sensitivity = 1f;

    void Update()
    {
        float xAxis = Input.GetAxis("Mouse X");
        float yAxis = Input.GetAxis("Mouse Y");
        yAxis =Mathf.Clamp(yAxis, -80f, 80f);
        horizontalAxis.Value += xAxis * sensitivity * Time.deltaTime;
        verticalAxis.Value += yAxis * sensitivity * Time.deltaTime;
    }

    private void LateUpdate()
    {
        camFollowPos.localEulerAngles = new Vector3(verticalAxis.Value, camFollowPos.localEulerAngles.y,
            camFollowPos.localEulerAngles.z);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, horizontalAxis.Value, transform.eulerAngles.z);
    }
}