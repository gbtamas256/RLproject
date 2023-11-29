using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class MyAgent : Agent
{
    Rigidbody m_rigidbody;
    public float m_speed = 20;

    public GameObject Spawner;
    public GameObject Spawner2;
    public GameObject Spawner3;

    private Vector3 startingPosition = new Vector3(0.0f, 1.05f, 0.0f);

    private float boundXLeft = -4.25f;
    private float boundZFront = 4.25f;
    private float boundXRight = 4.25f;
    private float boundZBack = -4.25f;

    private enum ACTIONS
    {
        LEFT = 0,
        FORWARDS = 1,
        RIGHT = 2,
        BACKWARDS = 3,
        NOTHING = 4
    }

    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
    }

    public override void OnEpisodeBegin()
    {
        // We reset the agent's position
        transform.localPosition = startingPosition;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // We don't need this function now because we use the RayPerceptionSensor
        // Note however that we could add additional observations here, if we wanted to, like the speed & velocity of the agent etc.
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<int> actions = actionsOut.DiscreteActions;

        var horizontal = Input.GetAxisRaw("Horizontal");
        var vertical = Input.GetAxisRaw("Vertical");

        if (horizontal == -1)
        {
            actions[0] = (int)ACTIONS.LEFT;
        }
        else if (vertical == +1)
        {
            actions[0] = (int)ACTIONS.FORWARDS;
        }
        else if (horizontal == +1)
        {
            actions[0] = (int)ACTIONS.RIGHT;
        }
        else if (vertical == -1)
        {
            actions[0] = (int)ACTIONS.BACKWARDS;
        }
        else
        {
            actions[0] = (int)ACTIONS.NOTHING;
        }

    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        var actionTaken = actions.DiscreteActions[0];

        switch (actionTaken)
        {
            case (int)ACTIONS.NOTHING:
                break;
            case (int)ACTIONS.LEFT:
                // We translate the agent's body to the left if it can move left
                if (transform.localPosition.x > boundXLeft)
                    transform.Translate(-Vector3.right * m_speed * Time.fixedDeltaTime);
                break;
            case (int)ACTIONS.FORWARDS:
                if (transform.localPosition.z < boundZFront)
                    transform.Translate(Vector3.forward * m_speed * Time.fixedDeltaTime);
                break;
            case (int)ACTIONS.RIGHT:
                // We translate the agent's body to the right if it can move right
                if (transform.localPosition.x < boundXRight)
                    transform.Translate(Vector3.right * m_speed * Time.fixedDeltaTime);
                break;
            case (int)ACTIONS.BACKWARDS:
                if (transform.localPosition.z > boundZBack)
                    transform.Translate(-Vector3.forward * m_speed * Time.fixedDeltaTime);
                break;
        }

        AddReward(0.1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        // If the agent collided with a bullet, we delete the Bullets & end the episode
        if (other.tag == "Bullet")
        {
            // We delete each Bullet object that we have spawned so far 
            var parent = Spawner.transform;
            int numberOfChildren = parent.childCount;
            var parent2 = Spawner2.transform;
            int numberOfChildren2 = parent2.childCount;
            var parent3 = Spawner3.transform;
            int numberOfChildren3 = parent3.childCount;

            for (int i = 0; i < numberOfChildren; i++)
            {
                if (parent.GetChild(i).tag == "Bullet")
                {
                    Destroy(parent.GetChild(i).gameObject);
                }
            }
            for (int i = 0; i < numberOfChildren2; i++)
            {
                if (parent2.GetChild(i).tag == "Bullet")
                {
                    Destroy(parent2.GetChild(i).gameObject);
                }
            }
            for (int i = 0; i < numberOfChildren3; i++)
            {
                if (parent3.GetChild(i).tag == "Bullet")
                {
                    Destroy(parent3.GetChild(i).gameObject);
                }
            }

            EndEpisode();
        }
    }

}