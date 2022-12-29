
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DBH.Editor
{
    public class MobListEntityContextMenu : MonoBehaviour
    {
        [SerializeField]
        private Button m_deleteButton;

        [HideInInspector]
        public MobListEntity entity;

        private void Awake()
        {
            m_deleteButton.onClick.AddListener(HandleDeleteButtonClicked);
            Close();
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
            {
                StartCoroutine(ClickClose());
            }
        }

        IEnumerator ClickClose()
        {
            yield return new WaitForSeconds(0.2f);
            Close();
        }

        private void HandleDeleteButtonClicked()
        {
            MobDataEditorUtility.Delete(entity.data);
            Close();
        }

        private void Close()
        {
            gameObject.SetActive(false);
            entity = null;
        }
    }
}