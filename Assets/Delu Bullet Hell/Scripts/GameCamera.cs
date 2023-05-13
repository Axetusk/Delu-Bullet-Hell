using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DBH.Runtime
{
    public class GameCamera : MonoBehaviour
    {
        [SerializeField]
        private PlayField m_playField;

        [SerializeField]
        private Camera m_camera;

        [SerializeField]
        private RawImage m_output;


        // Start is called before the first frame update
        void Start()
        {
            SetCameraTargetToPlayField();

            RectTransform outputTransform = m_output.transform as RectTransform;
            GenerateCameraTexture((int)outputTransform.rect.height);
            SetOutputTexture(m_camera.targetTexture);
        }

        // Update is called once per frame
        void Update()
        {

        }

        void SetCameraTargetToPlayField()
        {
            m_camera.transform.position = (Vector3)m_playField.rect.center + new Vector3(0, 0, transform.position.z);
            m_camera.orthographicSize = m_playField.rect.height / 2;
        }

        void GenerateCameraTexture(int height)
        {
            float aspectRatio = m_playField.aspectRatio;
            int textureHeight = height + (height % m_playField.aspectRatioReal.y);
            int textureWidth = (int)(textureHeight * aspectRatio);

            m_camera.targetTexture = new RenderTexture(textureWidth, textureHeight, 16, UnityEngine.Experimental.Rendering.GraphicsFormat.R8G8B8A8_UNorm);
            m_camera.targetTexture.filterMode = FilterMode.Point;
        }

        void SetOutputTexture(Texture targetTexture)
        {

            m_output.texture = targetTexture;
        }
    }
}

