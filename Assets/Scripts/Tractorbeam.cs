using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum ForceType{None, Pull, Push}
public class Tractorbeam : Singleton<Tractorbeam> {

    public Transform parentTransform;

    // public Vector3 tractorBeamTargetPoint = Vector3.zero;

    public float tractorbeamDistance = 10f;
    public float tractorbeamForce = 1f;

    LineRenderer lineRenderer;
    public Material pullMaterial;
    public Material pushMaterial;


    public override void Awake(){
        base.Awake();
        lineRenderer = GetComponent<LineRenderer>();
    }
    

    void FixedUpdate(){
        if(GameState.instance.isGameOver){
            lineRenderer.enabled = false;
            return;
        }

        if(Input.GetMouseButton(0)){
            ShootTractorBeam(ForceType.Pull);
        }
        else if(Input.GetMouseButton(1)){
            ShootTractorBeam(ForceType.Push);
        }else{
            lineRenderer.enabled = false;
        }
    }


    void ShootTractorBeam(ForceType forceType){
        RaycastHit[] hits;

        hits = Physics.RaycastAll(parentTransform.position, parentTransform.up * tractorbeamDistance);
        float closestPointDistance = 0f;
        Vector3 closestPoint = parentTransform.up * tractorbeamDistance;
        foreach(RaycastHit hit in hits){
            Asteroid asteroid = hit.transform.GetComponent<Asteroid>();
            if(asteroid != null){
                Vector3 force = hit.point - parentTransform.position; 
                force *= tractorbeamForce;
                if(forceType == ForceType.Pull){
                    force *= -1;
                }
                asteroid.asteroidRigidbody.AddForceAtPosition(force, hit.point);
                float currentDistance = Vector3.Distance(parentTransform.position, hit.point);
                if(currentDistance < closestPointDistance){
                    closestPointDistance = currentDistance;
                    closestPoint = hit.point;
                }
            }
        }
        DrawBeam(parentTransform.position, closestPoint, forceType);
    }

    void DrawBeam(Vector3 from, Vector3 to, ForceType forceType){
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, from);
        lineRenderer.SetPosition(1, from + to);
        if(forceType == ForceType.Pull){
            lineRenderer.material = pullMaterial;
        }else{
            lineRenderer.material = pushMaterial;
        }
    }


    void OnDrawGizmos() {
        Debug.DrawLine(parentTransform.position, parentTransform.position + parentTransform.up * tractorbeamDistance, Color.blue);
    }
}
