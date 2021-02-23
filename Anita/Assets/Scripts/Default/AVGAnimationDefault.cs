using System.Collections;
using UnityEngine;

namespace Anita
{
    //默认动画
    public class AVGAnimationDefault : MonoBehaviour, AnitaAnimation
    {
        public float comShakeStrength = 5f;
        [Range(0.02f, 0.2f)]
        public float comShakeTime = 0.05f;

        public float comShakeStrengthAround = 5f;
        [Range(0.02f, 0.5f)]
        public float comShakeTimeAround = 0.5f;
        [Range(0.01f, 0.1f)]
        public float comShakeFrameTimeAround = 0.04f;

        public float screenShakeStrength = 0.01f;
        [Range(0.1f, 1f)]
        public float screenShakeTime = 0.2f;
        [Range(0.01f, 0.1f)]
        public float screenShakeFrameTime = 0.04f;

        private Camera cam = null;
        private Component character = null;
        private Component background = null;

        public void Init(Component character, Component background, Camera cam)
        {
            this.cam = cam;
            this.character = character;
            this.background = background;
        }

        public void Init()
        {
            Debug.Log("Have not Implemented! at Init() at AVGAnimationDefault.cs");
        }

        //组件上下震动
        public void ShakeComponent(Component o)
        {
            //if (shakeObjCoro != null)
            //{
            //    StopCoroutine(shakeObjCoro);
            //}
            if (o == null)
            {
                Debug.LogError("Null Pointer at ShakeComponent() at AVGAnimation.cs");
            }
            StartCoroutine(ShakeComponentCoro(o));
        }

        public IEnumerator ShakeComponentCoro(Component o)
        {
            Vector3 origin = o.transform.position;
            o.transform.position += Vector3.up * comShakeStrength;
            yield return new WaitForSeconds(comShakeTime);
            o.transform.position = origin;
        }

        //组件随机震动
        public void ShakeComponentAround(Component o)
        {
            if (o == null)
            {
                Debug.LogError("Null Pointer at ShakeComponentAround() at AVGAnimation.cs");
            }
            StartCoroutine(ShakeComponentAroundCoro(o));
        }

        public IEnumerator ShakeComponentAroundCoro(Component o)
        {
            float shakeTime = comShakeTimeAround;
            float frameTime = 0f;
            Vector3 origin = o.transform.position;
            while (shakeTime > 0)
            {
                shakeTime -= Time.deltaTime;
                frameTime += Time.deltaTime;

                if (frameTime >= comShakeFrameTimeAround)
                {
                    frameTime = 0;
                    o.transform.position = origin + (Vector3.up * (Random.value * 2 - 1) +
                        Vector3.right * (Random.value * 2 - 1)) * comShakeStrengthAround;
                }
                yield return null;
            }
            o.transform.position = origin;
        }

        //相机震动
        public void ShakeScreen(Camera camera)
        {
            if (camera == null)
            {
                camera = Camera.main;
            }
            StartCoroutine(ShakeScreenCoro(camera));
        }

        public IEnumerator ShakeScreenCoro(Camera camera)
        {
            float shakeTime = screenShakeTime;
            float frameTime = 0f;
            Rect origin = camera.rect;
            while (shakeTime > 0)
            {
                shakeTime -= Time.deltaTime;
                frameTime += Time.deltaTime;

                if (frameTime >= screenShakeFrameTime)
                {
                    frameTime = 0;
                    camera.rect = new Rect((-1 + Random.value * 2) * screenShakeStrength,
                        (-1 + Random.value * 2) * screenShakeStrength, 1, 1);
                }
                yield return null;
            }
            camera.rect = origin;
        }

        public void StartAnimation(string type)
        {
            if ("ch_shake".Equals(type.ToLower()))
            {
                ShakeComponent(character);
            }
            else if ("bk_shake".Equals(type.ToLower()))
            {
                ShakeComponentAround(background);
            }
            else
            {
                UnknownAnimation(type);
            }
        }

        public void UnknownAnimation(string type)
        {
            Debug.Log("!!! UnknownAnimation: " + type + " at UnknownAnimation() at AVGAnimationDefault.cs");
        }
    }
}
