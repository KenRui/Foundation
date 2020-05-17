/********************************************************************************

** 类名称：OutlineFilter

** 描述：描边效果

** 作者：徐欢

** 创建时间：2017-07-20

** 最后修改该人：徐欢

** 最后修改该时间：2017-07-20

** 版权所有 (C)：aligames

********************************************************************************/

using UnityEngine;
using System.Collections;

namespace XCore
{
    [RequireComponent(typeof(Camera))]
    public class OutlineFilter : MonoBehaviour
    {
        [Range(0, 1)]
        public float blendFactor = 0.45f;   // 混合比例
        [HideInInspector]
        public Camera effectCam;            // 绘制特效的摄像机

        private Material mMaterial; // 描边材质    
        private static Camera mCamera;

        // Use this for initialization
        void Start()
        {
            Shader shader = Resources.Load<Shader>("Art/Shaders/OutlineFilter");
            mMaterial = new Material(shader);
            mMaterial.hideFlags = HideFlags.HideAndDontSave;
            mMaterial.SetFloat("_BlendFactor", 0.3f);
            float num = 1f / Camera.main.pixelWidth;
            float num2 = 1f / Camera.main.pixelHeight;
            Vector4 vector = new Vector4(-num, 0f, num, 0f);
            Vector4 vector2 = new Vector4(0f, -num2, 0f, num2);
            mMaterial.SetVector("_TexelOffset0", vector);
            mMaterial.SetVector("_TexelOffset1", vector2);

        }

        private void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
            if (destination == null)
            {
                RenderTexture temporary = RenderTexture.GetTemporary(source.width, source.height, 0);
                source.filterMode = FilterMode.Point;
                Graphics.Blit(source, temporary, mMaterial, 0);
                DrawEffects(temporary, source);
                Graphics.Blit(temporary, destination);
                RenderTexture.ReleaseTemporary(temporary);
            }
            else
            {
                Graphics.Blit(source, destination, mMaterial, 0);
                DrawEffects(destination, source);
            }
        }

        private void Update()
        {
            if(mCamera == null || effectCam == null)
            {
                return;
            }
            effectCam.fieldOfView = mCamera.fieldOfView;
        }

        /// <summary>
        /// 画特效
        /// </summary>
        /// <param name="colorRT"></param>
        /// <param name="depthRT"></param>
        private void DrawEffects(RenderTexture colorRT, RenderTexture depthRT)
        {
            if (effectCam == null)
            {
                return;
            }

            effectCam.enabled = true;
            RenderTexture targetTexture = effectCam.targetTexture;
            effectCam.SetTargetBuffers(colorRT.colorBuffer, depthRT.depthBuffer);
            effectCam.Render();
            effectCam.targetTexture = targetTexture;
            effectCam.enabled = false;
        }

        /// <summary>
        /// 开启描边效果
        /// </summary>
        public static void EnableOutlineFilter()
        {
            int mask = LayerMask.GetMask(new string[] { "Effect" });
            mCamera = Camera.main;
            if (mCamera == null)
            {
                return;
            }
            OutlineFilter outlineFilter = mCamera.gameObject.AddComponent<OutlineFilter>();
            mCamera.cullingMask = mCamera.cullingMask & ~mask;
            GameObject go = new GameObject();
            go.name = mCamera.name + " Effect";
            go.transform.parent = mCamera.transform;
            go.transform.localPosition = Vector2.zero;
            go.transform.localRotation = Quaternion.identity;
            Camera effectCam = go.AddComponent<Camera>();
            effectCam.aspect = mCamera.aspect;
            effectCam.backgroundColor = mCamera.backgroundColor;
            effectCam.nearClipPlane = mCamera.nearClipPlane;
            effectCam.farClipPlane = mCamera.farClipPlane;
            effectCam.fieldOfView = mCamera.fieldOfView;
            effectCam.pixelRect = mCamera.pixelRect;
            effectCam.rect = mCamera.rect;
            effectCam.clearFlags = CameraClearFlags.Nothing;
            effectCam.cullingMask = mask;
            effectCam.enabled = false;
            outlineFilter.effectCam = effectCam;
        }

        /// <summary>
        /// 关闭描边效果
        /// </summary>
        public static void DisableOutlineFilter()
        {
            int mask = LayerMask.GetMask(new string[] { "Effect" });
            mCamera = Camera.main;
            if (mCamera == null)
            {
                return;
            }
            OutlineFilter filter = mCamera.GetComponent<OutlineFilter>();
            if (filter == null)
            {
                return;
            }
            Destroy(filter);
            mCamera.cullingMask = mCamera.cullingMask | mask;
            Camera[] cameras = mCamera.GetComponentsInChildren<Camera>();
            for (int i = 0; i < cameras.Length; i++)
            {
                Camera cam = cameras[i];
                if (cam != mCamera)
                {
                    GameObject go = cam.gameObject;
                    Destroy(cam);
                    Destroy(go);
                }
            }
            mCamera = null;
        }
    }
}
