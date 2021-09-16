using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageStacker : MonoBehaviour
{

    [Header("Packages")]
    public List<Transform> Packages;
    private Vector3[] PackageVelocity;
    public Transform StackStart;
    [SerializeField]
    private int MaxPackages;

    [Header("Physics")]
    [SerializeField]
    private float friction;
    [SerializeField]
    private float drag;
    [SerializeField]
    private float maxPosDeviation;

    [Header("Locals")]
    private Vector3 lastPosition;
    private Vector3 lastVelocity;
    private Vector3 acceleration;

    public static PackageStacker instance;

    void Start()
    {
        instance = this;
        if(Packages == null) Packages = new List<Transform>();
        if(PackageVelocity == null) PackageVelocity = new Vector3[MaxPackages];

        lastPosition = StackStart.transform.position;

        for(int i = 0; i < Packages.Count; i++)
        {
            Packages[i].transform.position = StackStart.position + StackStart.up * i * 0.5f;
            Packages[i].transform.SetParent(i > 0 ? Packages[i-1].transform : StackStart);
        }
    }

    void FixedUpdate()
    {
        UpdateAcceleration();
        UpdatePhysics();
    }

    //Call this to put package onto stack. Returns false if stack is full
    public bool StackPackage(Transform packageObject)
    {
        if(Packages.Count >= MaxPackages) return false;

        packageObject.SetParent(Packages.Count > 0 ? Packages[Packages.Count - 1] : StackStart);
        packageObject.localPosition = new Vector3(0, Packages.Count > 0 ? Packages[Packages.Count - 1].lossyScale.y : 0, 0);
        packageObject.localPosition = new Vector3(0, 0, 0);
        Packages.Add(packageObject);
        return true;
    }

    //Call this to get top stack package
    public Transform UnstackPackage()
    {
        if(Packages.Count == 0) return null;
        Transform ret = Packages[Packages.Count - 1];
        ret.SetParent(null);
        Packages.RemoveAt(Packages.Count - 1);
        return ret;
    }


    private void UpdateAcceleration()
    {
        Vector3 velocity = StackStart.transform.position - lastPosition;
        lastPosition = StackStart.transform.position;

        acceleration = (velocity - lastVelocity)/Time.deltaTime;
        acceleration.y = 0;

        lastVelocity = velocity;

        Debug.DrawRay(transform.position, acceleration, Color.red);
    }

    private void UpdatePhysics()
    { 
        for(int i = 1; i < Packages.Count; i++)
        {
            float effectiveFriction = friction * (1-(Packages.Count - i)/6);
            Debug.Log(effectiveFriction);
            PackageVelocity[i] = PackageVelocity[i] * drag - acceleration * Random.Range(0.9f, 1) * effectiveFriction;
            Debug.DrawRay(Packages[i].transform.position, PackageVelocity[i], Color.green);
            Packages[i].transform.localPosition = Vector3.ClampMagnitude( new Vector3(Packages[i].transform.localPosition.x, 0, Packages[i].transform.localPosition.z) + PackageVelocity[i] * Time.deltaTime, maxPosDeviation) + Packages[i].transform.up;
        }
    }
}
