using UnityEngine;

public class Dust : MonoBehaviour
{
    private CustomPlayerController sc;
    private ParticleSystem ps;
    private bool LastEmitState = true;

    private void Awake()
    {
        // Use InParent and InChildren to be more robust
        sc = GetComponentInParent<CustomPlayerController>();
        ps = GetComponentInChildren<ParticleSystem>();
    }

    void Update()
    {
        // Prevent errors if references are missing
        if (sc == null || ps == null) return;

        bool shouldEmit = sc.IsGround && (sc.ForwardInput != 0 || sc.TurnInput != 0);

        if (shouldEmit != LastEmitState)
        {
            if (shouldEmit)
                ps.Play();
            else
                ps.Stop();

            LastEmitState = shouldEmit;
        }
    }
}
