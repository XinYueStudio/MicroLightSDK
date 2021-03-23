using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 

namespace MicroLight
{
    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.EventSystems;
    using UnityPlugin.Pointer3D;
    public interface ICanvasRaycastTarget
    {
        Canvas canvas { get; }
        bool enabled { get; }
        bool ignoreReversedGraphics { get; }
    }

   // [ExecuteInEditMode]
    [RequireComponent(typeof(Canvas))]
    [AddComponentMenu("MicroLight/MicroLightCanvas"), DisallowMultipleComponent]
    public class MicroLightCanvas : UIBehaviour, ICanvasRaycastTarget
    {

        public bool UseCurvedUI = false;
        [Range(-360,360)]
        public float Angle = 90;
         float MAngle = 90;
        //internal system settings
        [Range(1, 100)]
        public int baseCircleSegments = 24;
        int MbaseCircleSegments = 24;
        [Range(1, 10)]
        public float Quality = 1;
        float MQuality = 1;
        private Canvas m_canvas;
        [SerializeField]
        private bool m_IgnoreReversedGraphics = true;

        public virtual Canvas canvas { get { return m_canvas ?? (m_canvas = GetComponent<Canvas>()); } }

        public bool ignoreReversedGraphics { get { return m_IgnoreReversedGraphics; } set { m_IgnoreReversedGraphics = value; } }

        protected override void OnEnable()
        {
            if (UseCurvedUI)
            {
                foreach (UnityEngine.UI.Graphic graph in (this).GetComponentsInChildren<UnityEngine.UI.Graphic>())
                {
                    if (graph.GetComponent<MicroLightUIEffect>() == null)
                    {
                        graph.gameObject.AddComponent<MicroLightUIEffect>();

                    }
                    graph.SetAllDirty();
                }
            }
            else
            {
                foreach (UnityEngine.UI.Graphic graph in (this).GetComponentsInChildren<UnityEngine.UI.Graphic>())
                {
                    MicroLightUIEffect effect= graph.GetComponent<MicroLightUIEffect>();
                    if (effect != null)
                    {
                        if(Application.isEditor)
                        {
                            DestroyImmediate(effect);
                        }
                        else
                        {
                            Destroy(effect);
                        }
                    }
                    graph.SetAllDirty();
                }
            }
            base.OnEnable();
            CanvasRaycastMethod.AddTarget(this);
        }

        protected override void OnDisable()
        {
            foreach (UnityEngine.UI.Graphic graph in (this).GetComponentsInChildren<UnityEngine.UI.Graphic>())
            {
                graph.SetAllDirty();
            }
            base.OnDisable();
            CanvasRaycastMethod.RemoveTarget(this);
        }

        public void DoUpdate()
        {
            if (MbaseCircleSegments != baseCircleSegments
            || MAngle != Angle
            || MQuality != Quality)
            {
                MbaseCircleSegments = baseCircleSegments;
                MAngle = Angle;
                MQuality = Quality;
                OnDisable();
                OnEnable();
            }
        }


      


        private Canvas[] m_canvasbuf;
        private List<MicroLightCanvas>microLightCanvasBuf; 
        public void DropDownClick()
        {
            CanvasRaycastMethod.ClearTarget();
            CanvasRaycastMethod.AddTarget(this);

            m_canvasbuf = transform.GetComponentsInChildren<Canvas>();
            for(int i=0;i< m_canvasbuf.Length; i++)
            {
                MicroLightCanvas microLightCanvas = m_canvasbuf[i].GetComponent<MicroLightCanvas>();
                if(microLightCanvas==null)
                {
                    microLightCanvas= m_canvasbuf[i].gameObject.AddComponent<MicroLightCanvas>();
                    microLightCanvas.UseCurvedUI = false;
                    CanvasRaycastMethod.AddTarget(microLightCanvas);
                    microLightCanvasBuf.Add(microLightCanvas);
                }
            }

        }

      
    }

}