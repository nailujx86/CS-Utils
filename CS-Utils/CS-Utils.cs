using ICities;
using ColossalFramework;
using UnityEngine;
using ColossalFramework.UI;
using System.Collections;

namespace CS_Utils {
    public class Data {
        public static int moneyToAdd = 0;
    }

    public class ModInfo : IUserMod {
        public string Name {
            get {
                return "CS-Utils";
            }
        }
        public string Description {
            get {
                return "github.com/nailujx86/CS-Utils Ctrl+Shift+L to view Limits, Ctrl+Shift+M to get 100k";
            }
        }
    }

    public class Loading : LoadingExtensionBase {
        public override void OnLevelLoaded(LoadMode mode) {
            /**
            UIView view = UIView.GetAView();
             UIComponent component = view.AddUIComponent(typeof(CheatButton));
            **/
        }
    }

    public class Threading : ThreadingExtensionBase {
        ArrayList pressedKeys = new ArrayList();
        private bool processed = false;
        public override void OnUpdate(float realTimeDelta, float simulationTimeDelta) {
            if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)) pressedKeys.Add("Shift");
            if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl)) pressedKeys.Add("Control");
            if (Input.GetKeyDown(KeyCode.L)) pressedKeys.Add("L");
            if (Input.GetKeyDown(KeyCode.M)) pressedKeys.Add("M");

            if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift)) pressedKeys.Remove("Shift");
            if (Input.GetKeyUp(KeyCode.LeftControl) || Input.GetKeyUp(KeyCode.RightControl)) pressedKeys.Remove("Control");
            if (Input.GetKeyUp(KeyCode.L)) pressedKeys.Remove("L");
            if (Input.GetKeyUp(KeyCode.M)) pressedKeys.Remove("M");

            if (pressedKeys.Contains("Shift") && pressedKeys.Contains("Control") && pressedKeys.Contains("L")) {
                if (processed) return;
                processed = true;
                SimulationManager simmgr = Singleton<SimulationManager>.instance;
                TreeManager treemgr = Singleton<TreeManager>.instance;
                BuildingManager buildmgr = Singleton<BuildingManager>.instance;
                int treeC = treemgr.m_treeCount;
                int maxTreeC = TreeManager.MAX_TREE_COUNT;
                int buildigC = buildmgr.m_buildingCount;
                int maxBuildingC = BuildingManager.MAX_BUILDING_COUNT;
                string text = $"Trees: {treeC} of {maxTreeC}\nBuildings: {buildigC} of {maxBuildingC}";
                new ChirperMessage("CS-Utils", text, 0).show();
                pressedKeys.Remove("L");
            } else {
                processed = false;
            }
            if (pressedKeys.Contains("Shift") && pressedKeys.Contains("Control") && pressedKeys.Contains("M")) {
                if (processed) return;
                Singleton<EconomyManager>.instance.AddResource(EconomyManager.Resource.RewardAmount, 100000 * 100, ItemClass.Service.None, ItemClass.SubService.None, ItemClass.Level.None);
                new ChirperMessage("CS-Utils", "Added 100k to your funds. ;)", 0).show();
                pressedKeys.Remove("M");
            } else {
                processed = false;
            }
        }
    }

    public class CheatButton : UIButton {
        public override void Start() {
            this.text = "Get some cash..";
            this.width = 100;
            this.height = 30;
            this.normalBgSprite = "ButtonMenu";
            this.disabledBgSprite = "ButtonMenuDisabled";
            this.hoveredBgSprite = "ButtonMenuHovered";
            this.focusedBgSprite = "ButtonMenuFocused";
            this.pressedBgSprite = "ButtonMenuPressed";
            this.textColor = new Color32(255, 255, 255, 255);
            this.disabledTextColor = new Color32(7, 7, 7, 255);
            this.hoveredTextColor = new Color32(7, 132, 255, 255);
            this.focusedTextColor = new Color32(255, 255, 255, 255);
            this.pressedTextColor = new Color32(30, 30, 44, 255);
            this.playAudioEvents = true;
            this.transformPosition = new Vector3(-1.65f, 0.97f);
            this.eventClick += CheatButtonClick;
        }

        private void CheatButtonClick(UIComponent component, UIMouseEventParameter eventParam) {
            Singleton<EconomyManager>.instance.AddResource(EconomyManager.Resource.RewardAmount, 100000 * 100, ItemClass.Service.None, ItemClass.SubService.None, ItemClass.Level.None);
        }
    }

    public class ChirperMessage : IChirperMessage {
        string _sender;
        string _message;
        int _id = 0;

        public ChirperMessage(string sender, string message, int id) {
            this._sender = sender;
            this._message = message;
            this._id = id;
        }

        public string senderName => _sender;

        public string text => _message;

        public uint senderID => (uint)_id;

        public void show() {
            Singleton<ChirpPanel>.instance.AddMessage(this, true);
        }
    }
}
