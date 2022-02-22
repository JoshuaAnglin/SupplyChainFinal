using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour,idamage
{
    public CharacterController characterController;
    public float speed = 6;
    private float gravity = 9.8f;
    private float VerticalSpeed = 0;
    public Transform cameraHolder;
    public float mouseSensitivity = 2f;
    public float upLimit = -50;
    public float downLimit = 50;

    [SerializeField]
    Component healthbar;
    [SerializeField]
    int health = 70;
    [SerializeField]
    int minhealth = 0;
    [SerializeField]
    int maxhealth = 100;
    // Start is called before the first frame update

    RaycastHit obj;
    [SerializeField]
    float hitrange = 6.0f;
    float lasthit = 0.0f;
    [SerializeField]
    int damage = -20;
    [SerializeField]
    float hitcooldown = 1.0f;
    void Start()
    {
        updatehealth();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > 10.0f && Time.time < 11.0f)
        {
            maxhealth = 400;
            health = 250;
            damage = -30;
        }
        Move();
        Rotate();
        if (Input.GetMouseButton(1))
        {
            if (Physics.Raycast(cameraHolder.position, transform.forward, out obj, hitrange))
            {
                if (obj.transform.GetComponent<idamage>() != null)
                {
                    idamage att = obj.transform.GetComponent<idamage>();
                    if (Time.time > lasthit + hitcooldown)
                    {
                        att.addhealth(damage);
                        lasthit = Time.time;
                    }
                }
            }
        }
        if (Input.GetKey(KeyCode.Space))
        {
            List<RaycastHit> names = new List<RaycastHit>();
            for (int n = 0; n < 19; n++)
            {
                if (n < 9)
                {
                    Vector3 place = transform.forward * n + transform.right * (9 - n);
                    float dist = Vector3.Distance(transform.position, transform.position + place);
                    //Debug.DrawRay(transform.position, (place) * (5 / dist), Color.yellow, 5.0f);
                    if (Physics.Raycast(transform.position, place, out obj, 7 / dist))
                    {
                        if (obj.transform.GetComponent<idamage>() != null)
                        {
                            idamage att = obj.transform.GetComponent<idamage>();
                            att.addhealth(5);
                        }
                    }
                }
                if (n > 9)
                {
                    Vector3 place = transform.forward * (n - 10) + -transform.right * (9 - (n - 10));
                    float dist = Vector3.Distance(transform.position, transform.position + place);
                    //Debug.DrawRay(transform.position, (place) * (5 / dist), Color.yellow, 5.0f);
                    if (Physics.Raycast(transform.position, place, out obj, 7 / dist))
                    {
                        if (obj.transform.GetComponent<idamage>() != null)
                        {
                            idamage att = obj.transform.GetComponent<idamage>();
                            att.addhealth(5);
                        }
                    }
                }
            }
            lasthit = Time.time;
        }
    }
    private void Move()
    {
        float horizontalMove = Input.GetAxis("Horizontal");
        float verticalMove = Input.GetAxis("Vertical");
        if (characterController.isGrounded) VerticalSpeed = 0;
        else VerticalSpeed -= gravity * Time.deltaTime;
        Vector3 gravityMove = new Vector3(0, VerticalSpeed, 0);

        Vector3 move = transform.forward * verticalMove + transform.right * horizontalMove;
        characterController.Move(speed * Time.deltaTime * move);
    }
    public void Rotate()
    {
        float horizontalRotation = Input.GetAxis("Mouse X");
        float verticalRotation = Input.GetAxis("Mouse Y");
        transform.Rotate(0, horizontalRotation * mouseSensitivity, 0);
        cameraHolder.Rotate(-verticalRotation * mouseSensitivity, 0, 0);
        Vector3 currentRotation = cameraHolder.localEulerAngles;
        if (currentRotation.x > 180) currentRotation.x -= 360;
        currentRotation.x = Mathf.Clamp(currentRotation.x, upLimit, downLimit);
        cameraHolder.localRotation = Quaternion.Euler(currentRotation);
    }
    void updatehealth()
    {
        healthbar.transform.localScale = new Vector3((float)(health - minhealth) / (maxhealth - minhealth), 1.0f);
    }
    public void addhealth(int amount)
    {
        health += amount;
        if (health < minhealth) health = minhealth;
        if (health > maxhealth) health = maxhealth;
        updatehealth();
    }
}