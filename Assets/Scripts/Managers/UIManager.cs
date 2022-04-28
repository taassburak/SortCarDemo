using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;

namespace Scripts.Manager
{
    public class UIManager : CustomBehaviour
    {
        public MainMenuPanel MainMenuPanel => _mainMenuPanel;
        public FinishPanel FinishPanel => _finishPanel;
        [SerializeField] private MainMenuPanel _mainMenuPanel;
        [SerializeField] private FinishPanel _finishPanel;

        private List<UIPanel> UIPanels;

        public override void Initialize(GameManager gameManager)
        {
            base.Initialize(gameManager);
            UIPanels = new List<UIPanel> { _mainMenuPanel, _finishPanel};

            UIPanels.ForEach(x =>
            {
                x.Initialize(this);
                x.gameObject.SetActive(false);
            });
            

        }

    }

}
