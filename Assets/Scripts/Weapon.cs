using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Camera miCam;
    public float range = 100f;
    public GameObject balaDecal;
    public float shootRate = 15f;
    public float nextTimeToShoot = 1f;
    public float shootForce = 150f;
    public int shootgunDamage = 1;
    public bool autoShoot = false;

    public AudioSource basicShootSound;
    public AudioSource autoShootSound;

    private Vector2 centroCamara;
    private Ray rayo;
    private RaycastHit hit;
    private Quaternion rotDecal;
    private Vector3 posDecal;

    public void Awake()
    {
        miCam = gameObject.transform.GetChild(0).GetComponent<Camera>();
        centroCamara.x = Screen.width / 2;
        centroCamara.y = Screen.height / 2;
        nextTimeToShoot = Time.time;
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (!GameController.Instance.isGameOver())
        {
            if (autoShoot)
            {
                if (Input.GetKey(KeyCode.X) && Time.time >= nextTimeToShoot)
                {
                    nextTimeToShoot = Time.time + 1 / shootRate;

                    if (autoShootSound != null)
                    {
                        autoShootSound.Play();
                    }

                    shoot();
                }
            }
            else
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    nextTimeToShoot = Time.time + 1 / shootRate;

                    if (basicShootSound != null)
                    {
                        basicShootSound.Play();
                    }

                    shoot();
                }
            }
        }  
    }

    private void shoot()
    {
        rayo = miCam.ScreenPointToRay(centroCamara);
        shootRate = Time.time;

        if (Physics.Raycast(rayo, out hit, range))
        {
            rotDecal = Quaternion.FromToRotation(Vector3.forward, hit.normal);
            posDecal = hit.point + hit.normal * 0.1f;

            if (hit.collider.tag == "Caja" || hit.collider.tag == "Barril")
            {
                DestroyBox box = hit.collider.GetComponent<DestroyBox>();

                if (box != null )
                {
                    box.TakeDamage(shootgunDamage);
                }

                if (hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(-hit.normal * shootForce);
                }
            }

            GameObject.Instantiate(balaDecal, posDecal, rotDecal);
        }
    }
}
