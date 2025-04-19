using UnityEngine;
using UnityEngine.Splines;

public class move : MonoBehaviour
{

    [SerializeField]private SplineAnimate Anim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    float k = 0;
    float FT = 0;
    void Start()
    {
        Anim.MaxSpeed = 10;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float N = Anim.NormalizedTime;
        //FT = Anim.ElapsedTime;
        print(N);
        if (k != 1)
        k += 0.05f;
        print(Anim.MaxSpeed =Mathf.Lerp(10, 1,k));
        Anim.NormalizedTime = N;
        //print(Anim.ElapsedTime = FT * Anim.ElapsedTime);
    }
}
