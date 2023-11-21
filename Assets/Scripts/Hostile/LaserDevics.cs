using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserDevics : MonoBehaviour
{
    [SerializeField] Transform m_LaserDestinationsParent;
    [SerializeField] float m_MovementSpeed;
    [SerializeField] float m_RotationSpeed;
    [SerializeField] float m_Proximity;
    Transform[] destinations;
    int targetIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        // get all the destinations or points to which a laser has to visit.
        destinations = m_LaserDestinationsParent.GetComponentsInChildren<Transform>();
        // A future upgrade will be using curves to control laser path.


        transform.position = destinations[0].position;
        transform.rotation = destinations[0].rotation;
        targetIndex++;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Angle(transform.forward, destinations[targetIndex].transform.forward) > 10f)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, destinations[targetIndex].localRotation, m_RotationSpeed * Time.deltaTime);
            return;
        }
        transform.rotation = destinations[targetIndex].localRotation;
        Vector3 _direction = (destinations[targetIndex].position - transform.position).normalized;
        transform.position += _direction * m_MovementSpeed * Time.deltaTime;
        if(Vector3.Distance(transform.position, destinations[targetIndex].position) < m_Proximity)
        {
            transform.position = destinations[targetIndex].position;
            targetIndex ++;
            if(targetIndex >= destinations.Length)
            {
                targetIndex = 0;
            }
        }
    }
}
