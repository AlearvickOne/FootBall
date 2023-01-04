using UnityEngine;
using Photon.Pun;

public class PlayerController : MonoBehaviour
{
    [SerializeField] protected internal float _speedPlayer;
    [SerializeField] private float _inputX;
    [SerializeField] private float _inputY;

    private Rigidbody _playerRb;
    private PhotonView _photonView;

    private void Awake()
    {
        _photonView = this.GetComponent<PhotonView>();
        _playerRb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (_photonView.IsMine)
        {
            GetAxisButtons();
            if(this.gameObject.transform.position.y < 0)
               this.gameObject.transform.position = new Vector3(transform.position.x, 2, transform.position.z);

            if (this.gameObject.transform.position.x > 15)
                this.gameObject.transform.position = new Vector3(15, transform.position.y, transform.position.z);

            if (this.gameObject.transform.position.x < -20)
                this.gameObject.transform.position = new Vector3(-20, transform.position.y, transform.position.z);

            if (this.gameObject.transform.position.z > 21)
                this.gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, 21);

            if (this.gameObject.transform.position.z < -6)
                this.gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, -6);


        }
    }

    private void FixedUpdate()
    {
        if (_photonView.IsMine)
        {
            PlayerMoving();
        }
    }

    private void GetAxisButtons()
    {
        _inputX = Input.GetAxis("Horizontal") * _speedPlayer;
        _inputY = Input.GetAxis("Vertical") * _speedPlayer;
    }

    private void PlayerMoving()
    {
        if (_inputX < 0 || _inputX > 0)
        {
            _playerRb.AddForce(Vector3.left * _inputX * Time.fixedDeltaTime, ForceMode.Impulse);
        }

        if (_inputY < 0 || _inputY > 0)
        {
            _playerRb.AddForce(-Vector3.forward * _inputY * Time.fixedDeltaTime, ForceMode.Impulse);
        }

    }
}
