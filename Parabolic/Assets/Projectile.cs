using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public Rigidbody projectile;
    public GameObject cursor;
    public Transform shootPoint;
    public LayerMask layer;
    public LineRenderer lineVisual;
    public int lineSegment;


    public Camera cam;


    // Start is called before the first frame update
    void Start()
    {
        
        lineVisual.positionCount = lineSegment;
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        LaunchProjectile();
        Ray camRay = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        
    }
    void LaunchProjectile()
    {
        Ray camRay = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(camRay, out hit, 100f)) //layer koymak istersen koy 
        {


            
            

            Vector3 vo = CalculateVelocty(hit.point , shootPoint.position , 1f);

            if (vo.x > 0) //eðer vectör cismin arkasýna düþüyorsa dönme ve line çizme iþlemlerini yapma demiþ olduk. Çünkü cannon tam x == 0 noktasýnda nokta biraz geri düþse negatif olur.
            {
                cursor.SetActive(true);
                cursor.transform.position = hit.point + Vector3.up * 0.1f;

                Debug.Log("HitPoint : " + hit.point);

                Visualize(vo);

                Debug.Log("y : " + vo);


                transform.rotation = Quaternion.LookRotation(vo); //Nice method. Burada silahýmýza line'ýmýzýn vektörüne bak demiþ olduk.

                if (Input.GetMouseButtonDown(0))
                {
                    Rigidbody obj = Instantiate(projectile, shootPoint.position, Quaternion.identity);
                    obj.velocity = vo;
                }
            }
        }

            

    }


    Vector3 CalculateVelocty(Vector3 target , Vector3 origin , float time)
    {
        Vector3 distance = target - origin;
        Vector3 distanceXz = distance;
        distanceXz.y = 0f;

        float sY = distance.y;
        float sXz = distanceXz.magnitude;

        float Vxz = sXz * time;
        float Vy = (sY / time) + (0.5f * Mathf.Abs(Physics.gravity.y) * time); //senin gravity'in oyuncunun parmaðý çekilirken ekranýn sol yarýsýndaysa sola doðru saðdaysa saða doðru olmalý ((Physics.gravity.x) 

        Vector3 result = distanceXz.normalized;
        result *= Vxz;
        result.y = Vy;

        return result;
    }

    void Visualize(Vector3 vo)
    {
        for(int i = 0; i< lineSegment; i++)
        {
            Vector3 pos = CalculatePosInTime(vo, i / (float)lineSegment);
            lineVisual.SetPosition(i, pos);
        }
    }

    Vector3 CalculatePosInTime(Vector3 vo, float time)
    {
        Vector3 Vxz = vo;
        Vxz.y = 0f;
        //vo.y = 0

        Vector3 result = shootPoint.position + vo * time;

        float sY = (-0.5f * Mathf.Abs(Physics.gravity.y) * (time * time)) + (vo.y * time) + shootPoint.position.y;

        result.y = sY;

        return result;
    }
}
