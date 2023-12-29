using Unity.Jobs;
using UnityEngine;
using LitMotion;
using LitMotion.Adapters;

[assembly: RegisterGenericJobType(typeof(MotionUpdateJob<float, ShakeOptions, FloatShakeMotionAdapter>))]
[assembly: RegisterGenericJobType(typeof(MotionUpdateJob<Vector2, ShakeOptions, Vector2ShakeMotionAdapter>))]
[assembly: RegisterGenericJobType(typeof(MotionUpdateJob<Vector3, ShakeOptions, Vector3ShakeMotionAdapter>))]

namespace LitMotion.Adapters
{
    // Note: Shake motion uses startValue as offset and endValue as vibration strength.

    public readonly struct FloatShakeMotionAdapter : IMotionAdapter<float, ShakeOptions>
    {
        public float Evaluate(in float startValue, in float endValue, in ShakeOptions options, in MotionEvaluationContext context)
        {
            VibrationHelper.EvaluateStrength(endValue, options.Frequency, options.DampingRatio, context.Progress, out var s);
            float multipliar;
            if (options.RandomState.state == 0)
            {
                multipliar = SharedRandom.Random.Data.NextFloat(-1f, 1f);
            }
            else
            {
                // Currently RandomState is defensively copied and the state is not changed. This requires changing the Adapter's option argument to ref.
                multipliar = options.RandomState.NextFloat(-1f, 1f);
            }
            return startValue + s * multipliar;
        }
    }

    public readonly struct Vector2ShakeMotionAdapter : IMotionAdapter<Vector2, ShakeOptions>
    {
        public Vector2 Evaluate(in Vector2 startValue, in Vector2 endValue, in ShakeOptions options, in MotionEvaluationContext context)
        {
            VibrationHelper.EvaluateStrength(endValue, options.Frequency, options.DampingRatio, context.Progress, out var s);
            Vector2 multipliar;
            if (options.RandomState.state == 0)
            {
                multipliar = new Vector2(SharedRandom.Random.Data.NextFloat(-1f, 1f), SharedRandom.Random.Data.NextFloat(-1f, 1f));
            }
            else
            {
                multipliar = new Vector2(options.RandomState.NextFloat(-1f, 1f), options.RandomState.NextFloat(-1f, 1f));
            }
            return startValue + new Vector2(s.x * multipliar.x, s.y * multipliar.y);
        }
    }

    public readonly struct Vector3ShakeMotionAdapter : IMotionAdapter<Vector3, ShakeOptions>
    {
        public Vector3 Evaluate(in Vector3 startValue, in Vector3 endValue, in ShakeOptions options, in MotionEvaluationContext context)
        {
            VibrationHelper.EvaluateStrength(endValue, options.Frequency, options.DampingRatio, context.Progress, out var s);
            Vector3 multipliar;
            if (options.RandomState.state == 0)
            {
                multipliar = new Vector3(SharedRandom.Random.Data.NextFloat(-1f, 1f), SharedRandom.Random.Data.NextFloat(-1f, 1f), SharedRandom.Random.Data.NextFloat(-1f, 1f));
            }
            else
            {
                multipliar = new Vector3(options.RandomState.NextFloat(-1f, 1f), options.RandomState.NextFloat(-1f, 1f), options.RandomState.NextFloat(-1f, 1f));
            }
            return startValue + new Vector3(s.x * multipliar.x, s.y * multipliar.y, s.z * multipliar.z);
        }
    }
}