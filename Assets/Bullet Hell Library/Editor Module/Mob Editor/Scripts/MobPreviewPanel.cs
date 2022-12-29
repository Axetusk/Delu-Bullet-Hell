using DBH.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DBH.Editor
{
    public class MobPreviewPanel : MonoBehaviour
    {
        [SerializeField]
        private Camera m_camera;

        [SerializeField]
        private RawImage m_display;

        [SerializeField]
        //TODO: In the future, change this to whatever the runtime mob script will be
        private SpriteRenderer m_mobSprite;

        public void Initialize(MobEditor editor)
        {
            editor.onSelectedMobChanged += HandleSelectedMobChanged;
            editor.onMobDataChanged += HandleSelectedMobChanged;
        }

        // Start is called before the first frame update
        void Start()
        {
            m_display.texture = m_camera.targetTexture;
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void HandleSelectedMobChanged(MobData data)
        {
            m_mobSprite.sprite = data.sprite;
        }
    }
}