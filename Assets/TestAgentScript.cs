using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class TestAgentScript : Agent
{   
    //Notes
    /*
    //rays geändert
    //plattform kleiner
    //imitation learning 5 runden
    //negativer reward bei mauer hit und konstanter mauer hit
    How to use the ray sensor?
    wenn i hit dirt than i need to let the dirt dissappear 
    if all objects dissappeared add reward and endepisode (for that i could implement something like an counter till 20)
    //I can make imitation learning with the agent and catch the first 5 dirt that he realizes that this is good
    //ich mache die objekte zwar nicht sichtbar aber funktionell sind sie immer noch da
    //wenn ein ray sensor trifft bekomme ich winzig kleinen reward, d
    //check if the agent dont move 
    //log if the agent doesnt move
    //ich könnte negativen reward einbauen wenn mein agent die mauer trifft
    //behavioral_cloning:
      strength: 0.5
      demo_path: Demos/Demo7.demo
    gail:
        strength: 0.5
        demo_path: Demos/Demo7.demo
    //vielleicht habe ich auch falsch durch imitation learning trainiert. mein agent hat mit rays vielleicht die ein dirt material gesichtet ich bin dann aber nicht zu diesem material sondern zu einem anderen und das irritiert den algorithmus
    wenn das komische stehen bleiben behavior durch imitation learning besteht kann ich imi auch weg lassen um zu schauen ob das komische behavior auch dann noch auftaucht
    behaviors:
  VacuumCleaner:
    trainer_type: ppo
    hyperparameters:
      batch_size: 10
      buffer_size: 100
      learning_rate: 3.0e-4
      beta: 5.0e-4
      epsilon: 0.2
      lambd: 0.99
      num_epoch: 3
      learning_rate_schedule: linear
    network_settings:
      normalize: false
      hidden_units: 128
      num_layers: 2
    reward_signals:
      extrinsic:
        gamma: 0.99
        strength: 1.0
      gail:
        strength: 1.0
        demo_path: Demos/Demo7.demo
    behavioral_cloning:
      strength: 0.5
      demo_path: Demos/Demo7.demo
    max_steps: 1500000
    time_horizon: 64
    summary_freq: 10000
    */
    public override void OnEpisodeBegin(){
        transform.localPosition = new Vector3(-6, 1, 6);
    }


    //how the agent receives the environment
    public override void CollectObservations(VectorSensor sensor){
        //agent localPosition
        sensor.AddObservation(transform.localPosition);
    }

    public override void OnActionReceived(ActionBuffers actions){
        float moveX = actions.ContinuousActions[0];
        float moveZ = actions.ContinuousActions[1];    
        float moveSpeed = 10f;
        transform.localPosition += new Vector3(moveX, 0, moveZ) * Time.deltaTime * moveSpeed;
       // AddReward(-1f / MaxStep);
    }

    private void OnTriggerEnter(Collider other){
    }

    private void OnTriggerStay(Collider other){
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Goal"){
            AddReward(1f);
            EndEpisode();
        }
        Debug.Log(collision.gameObject.name);
        
    }

    public override void Heuristic(in ActionBuffers actionsOut){
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxisRaw("Horizontal");
        continuousActions[1] = Input.GetAxisRaw("Vertical");
    }
}