using ICities;
using ColossalFramework;
using UnityEngine;
using ColossalFramework.UI;

namespace CS_Utils {
    public class Data {
        public static int moneyToAdd = 0;
    }

    public class ModInfo : IUserMod {
        public string Name {
            get {
                return "Experimental Mod";
            }
        }
        public string Description {
            get {
                return "github.com/nailujx86";
            }
        }
    }

    public class Loading : LoadingExtensionBase {
        public override void OnLevelLoaded(LoadMode mode) {
            UIView view = UIView.GetAView();
            UIComponent component = view.AddUIComponent(typeof(CheatButton));
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
}
