using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterCamera : MonoBehaviour
{
    [Header("ī�޶� �⺻�Ӽ�")]
    //ī�޶� ��ġ ĳ�� �غ�
    private Transform cameraTransform = null;

    //�÷��̾� Ÿ�� ���� ������Ʈ ĳ�� �غ�
    public GameObject objTarget = null;

    //�÷��̾� Ÿ�� ��ġ ĳ�� �غ�
    private Transform objTargetTransform = null;

    //ī�޶� 3���� Ÿ�� ���� : First(1��Ī), Second(2��Ī), Third(3��Ī)
    public enum CameraTypeState { First, Second, Third }

    //ī�޶� �⺻ Ÿ���� 3��Ī�̴�.
    public CameraTypeState cameraTypeState = CameraTypeState.Third;

    [Header("3��Ī ī�޶�")]
    //���� ī�޶� ��ġ���� Ÿ�����κ��� �ڷ� ������ �Ÿ�
    public float distance = 6.0f;
    //���� ī�޶� ��ġ���� Ÿ���� ��ġ���� �� �߰����� ����
    public float height = 1.75f;

    //Damp�� ī�޶� �� �ʵڿ� �������� ���� ���̴�.
    //�ǹ����� Damp�� ������ ���� ���� �ð��� �����Ѵ�.

    //ī�޶� ���̿� ���� Damp �� 
    public float heightDamping = 2.0f;
    //ī�޶� y�� ȸ�������� Damp ��
    public float rotationDamping = 3.0f;

    [Header("2��Ī ī�޶�")]
    //�÷��̾� ��ǥ �ֺ��� ���� �ӵ�
    public float rotationSpd = 10.0f;

    [Header("1��Ī ī�޶�")]
    //���콺�� ī�޶� �����ϴ� ������ ��ǥ ��
    public float detailX = 5.0f;
    public float detailY = 5.0f;

    //���콺�� ȸ���ϴ� ��
    public float rotationX = 0.0f;
    public float rotationY = 0.0f;

    //ĳ���Ϳ� ī�޶� ���� ������ �� ������Ʈ ĳ�� �غ�
    public Transform posfirstCameraTarget = null;

    // Start is called before the first frame update
    void Start()
    {
        //ī�޶� ��ġ ĳ��
        cameraTransform = GetComponent<Transform>();

        //�÷��̾� ��ǥ ������Ʈ�� ���� �ϴٸ�.
        if (objTarget != null)
        {
            //�÷��̾� ��ǥ ������Ʈ ��ġ ĳ��
            objTargetTransform = objTarget.transform;
        }
    }

    /// <summary>
    /// 3��Ī ī�޶� �Լ�
    /// </summary>
    void ThirdCamera()
    {
        //���� Ÿ���� y�� ���� ��
        float objTargetRotationAngle = objTargetTransform.eulerAngles.y;
        //���� Ÿ���� ����  + ī�޶� ��ġ������ �߰� ����
        float objHeight = objTargetTransform.position.y + height;
        //���� ī�޶��� y�� ���� ���� ���Ϸ� ������ ���
        float nowRotationAngle = cameraTransform.eulerAngles.y;
        //���� ī�޶��� ���� ��
        float nowHeight = cameraTransform.position.y;

        //���� �������� ���ϴ� ������ Damp �� ����
        nowRotationAngle = Mathf.LerpAngle(nowRotationAngle, objTargetRotationAngle, rotationDamping * Time.deltaTime);

        //���� ���̿��� ���ϴ� ���̷� Damp �� ����
        nowHeight = Mathf.Lerp(nowHeight, objHeight, heightDamping * Time.deltaTime);

        //����Ƽ ������ ���ʹϾ����� ���Ϸ� �� ����
        Quaternion nowRotation = Quaternion.Euler(0f, nowRotationAngle, 0f);

        //������ ī�޶� ȸ�� ��Ű��.

        //ī�޶� ��ġ�� �÷��̾� ���� ���������� �̵�
        cameraTransform.position = objTargetTransform.position;

        //�÷��̾� ��ǥ�� ���� �������� �������� �� ����. 
        // -1 * nowRotation * Vector3.forward(���� ����) * �Ÿ�
        cameraTransform.position -= nowRotation * Vector3.forward * distance;

        //ī�޶� ���� ���� ���� Ÿ�ٿ� ��ġ x���� �ٶ󺸴� �ݴ� z���� ��ŭ �̵��ؼ� ���ϴ� 
        //���̸� �ø�.
        cameraTransform.position = new Vector3(cameraTransform.position.x, nowHeight, cameraTransform.position.z);

        //�������� �ٶ����
        cameraTransform.LookAt(objTargetTransform);
    }

    /// <summary>
    /// 2��Ī ī�޶� ���� �Լ�
    /// </summary>
    void SecondCamera()
    {
        //�÷��̾� ��ǥ �߽����� ���� �������� ���ƶ�.
        cameraTransform.RotateAround(objTargetTransform.position, Vector3.up, rotationSpd * Time.deltaTime);

        //�������� �ٶ����
        cameraTransform.LookAt(objTargetTransform);

    }

    /// <summary>
    /// 1��Ī ī�޶� ���� �Լ�
    /// </summary>
    void FirstCamera()
    {
        //���콺 x,y �� �� ��������
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        //ī�޶��� y������ ���콺(���콺 * ������)����ŭ �����δ�. 
        //���콺�� �������� �ʾҴٸ� 0�̴�.
        rotationX = cameraTransform.localEulerAngles.y + mouseX * detailX;

        //���̳ʽ� ������ �����ϱ� ���� ������ �������ش�.
        //���� ������ �����ָ� ���̳ʽ��� ������ �ٲ�� ���� Ƣ�� ���� Ȯ�� �� �� �ִ�.
        rotationX = (rotationX > 180.0f) ? rotationX - 360.0f : rotationX;

        //���� y���� ���콺�� ������ ��(���콺 + ������)��ŭ �����ش�.
        rotationY = rotationY + mouseY * detailY;
        //���� ���̳ʽ� ���� ������ �ϱ� ���� 
        rotationY = (rotationY > 180.0f) ? rotationY - 360.0f : rotationY;

        //���콺�� x,y���� ���� x,y��� �ݴ뿩�� �ݴ�� Vector�� ����� �ش�.
        cameraTransform.localEulerAngles = new Vector3(-rotationY, rotationX, 0f);
        //���� �� ������Ʈ �� �տ� ���� ī�޶� ��ġ�Ѵ�.
        cameraTransform.position = posfirstCameraTarget.position;
    }

    //�÷��̾� ��ǥ�� ���󰡱� ���� LateUpdate ����
    private void LateUpdate()
    {
        //��ǥ �÷��̾� ���ӿ�����Ʈ�� �����Ѱ�? ������ �Լ� ���� ����.
        if (objTarget == null)
        {
            return;
        }

        //��ǥ �÷��̾� ��ġ���� ���� ���ٸ�. �ش� ���ӿ�����Ʈ ��ġ ������ �����´�.
        if (objTargetTransform == null)
        {
            objTargetTransform = objTarget.transform;
        }

        //ī�޶� Ÿ�� 
        switch (cameraTypeState)
        {
            //3��Ī
            case CameraTypeState.Third:
                ThirdCamera();
                break;
            //2��Ī
            case CameraTypeState.Second:
                SecondCamera();
                break;
            //1��Ī
            case CameraTypeState.First:
                FirstCamera();
                break;
        }
    }

}