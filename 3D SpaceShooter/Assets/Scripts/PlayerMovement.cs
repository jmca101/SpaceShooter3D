using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerMovement : MonoBehaviour {

    public float movementSpeed;
    public float turnSpeed;
    public float rollSpeed;
    public GameObject shot;
    public GameObject shot2;
    public Transform shotSpawn;
    public float fireRate;
    private float nextFire;
    public GameObject Player;
    public float shotForward;
    public AudioClip[] clips;

    void update()
    {
        
        //SphereDetect();
    }

    void FixedUpdate()
    {
        Thrust();
        Turn();
Fire();
    }
    //function for accelertion
    void Thrust()
    {
        if(Input.GetAxis("LTrigger") > 0.0f)
        {
            transform.position += transform.forward * movementSpeed * Time.deltaTime * Input.GetAxis("LTrigger"); // Vertical thrust for player
        }
    }
    //Function to fire
    void Fire()
    {
        if (Input.GetAxis("RTrigger") > 0.0f && Time.time > nextFire)
        {
            shot = Instantiate(shot2) as GameObject;
            nextFire = Time.time + fireRate;
            GameObject temporaryShot;
            temporaryShot = Instantiate(shot, shotSpawn.transform.position, shotSpawn.transform.rotation) as GameObject;
            Rigidbody temporaryRigidBody;
            temporaryRigidBody = temporaryShot.GetComponent<Rigidbody>();
            temporaryRigidBody.AddForce(transform.forward * shotForward );
            playAudio();
            Destroy(temporaryShot, 20.0f);
        }
    }

    //function for turning
    void Turn()
    {
        float yaw = turnSpeed * Time.deltaTime * Input.GetAxis("LJoyHor"); //Turns player when Left stick is moved right or left
        float pitch = turnSpeed * Time.deltaTime * Input.GetAxis("LJoyVer"); //Turn player when left stick is moved up of down
        float roll = rollSpeed * Time.deltaTime * Input.GetAxis("LJoyHor"); // rolls player when left stick is moved right or left
        transform.Rotate(-pitch, yaw, -roll);
    }
    //function to detect player outside play area
    void SphereDetect()
    {
        float radius = 150.0f; //radius of Map
        Vector3 centerPosition = transform.localPosition; //center of Map
        Vector3 playerLocation = Player.transform.position; //player location
        float distance = Vector3.Distance(playerLocation, centerPosition); //distance from Player to center of Map

        if (distance > radius) //If the distance is less than the radius, it is already within the circle.
        {
            Vector3 fromOriginToObject = playerLocation - centerPosition; //Player - Map
            fromOriginToObject *= radius / distance; //Multiply by radius //Divide by Distance
            playerLocation = centerPosition + fromOriginToObject; //Map + all that Math
        }

       
    }

    void playAudio()
    {
        int randomClip = Random.Range(0, clips.Length);
        AudioSource source = gameObject.AddComponent < AudioSource >();
        source.clip = clips[randomClip];
        source.Play();
        Destroy(source, clips[randomClip].length);


    }



}
