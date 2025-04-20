using UnityEngine;
using UnityEngine.Splines;
using System.Collections;

public class move : MonoBehaviour
{
    [SerializeField] private float DefaultSpeed = 10;
    float Speed;
    private float NSpeed;
    [SerializeField] float k = 0;
    float koeff = 0.05f;
    [SerializeField]private SplineAnimate Anim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //float FT = 0;

    Coroutine coroutine = null;
    void Start()
    {
        //print(Anim.Container);
        Speed = DefaultSpeed;
        Anim.MaxSpeed = Speed;
        NSpeed = Speed;
    }

    IEnumerator Slow(float sec)
    {
        SetSpeed((float)DefaultSpeed / 2);
        yield return new WaitForSeconds(sec);
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

    public void TakeSlow(float sec)
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }
        coroutine = StartCoroutine(Slow(sec));
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (coroutine == null)
            Speed = DefaultSpeed;
        float N = Anim.NormalizedTime;
        //FT = Anim.ElapsedTime;
        //print(N);
        if (k != 1)
        k += koeff;
        Anim.MaxSpeed =Mathf.Lerp(NSpeed, Speed, k);
        Anim.NormalizedTime = N;
        //print(Anim.ElapsedTime = FT * Anim.ElapsedTime);
        if (Anim.NormalizedTime == 1) Destroy(this.gameObject);
    }
}
