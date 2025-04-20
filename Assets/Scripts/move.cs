using UnityEngine;
using UnityEngine.Splines;

public class move : MonoBehaviour
{
    [SerializeField] private float Speed = 10;
    private float NSpeed = 10;
    [SerializeField] float k = 0;
    float koeff = 0.05f;
    [SerializeField]private SplineAnimate Anim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    float FT = 0;
    void Start()
    {
        Anim.MaxSpeed = Speed;
        NSpeed = Speed;
    }

   void SetSpeed(float SpeedIn)
    {
        k = 0;
        Speed = SpeedIn;
    }
    void SetSpeed(float SpeedIn, float kIn)
    {
        Speed = SpeedIn;
        k = 0;
        koeff = kIn;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        float N = Anim.NormalizedTime;
        //FT = Anim.ElapsedTime;
        //print(N);
        if (k != 1)
        k += koeff;
        Anim.MaxSpeed =Mathf.Lerp(NSpeed, Speed, k);
        Anim.NormalizedTime = N;
        //print(Anim.ElapsedTime = FT * Anim.ElapsedTime);
    }
}
