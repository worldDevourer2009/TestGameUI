using System.Globalization;
using Controllers;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;
using Zenject;

namespace Views
{
    public class PlayerView : MonoBehaviour, IInitializable
    {
        [SerializeField] private Image hpImage;
        [SerializeField] private float fillSpeed;
        [SerializeField] private TextMeshProUGUI armorHead;
        [SerializeField] private TextMeshProUGUI armorBody;
        
        private IPlayerUIController _playerUIController;
        private Tween _tween;

        [Inject]
        public void Construct(IPlayerUIController playerUIController)
        {
            _playerUIController = playerUIController;
            Debug.Log("Construct view");
        }
        
        public void Initialize()
        {
            _playerUIController.Heal += UpdateHpFill;
            _playerUIController.TakenDamage += UpdateHpFill;

            _playerUIController.Armor += UpdateArmorHead;
            _playerUIController.Armor += UpdateArmorBody;
            
            Debug.Log("init view");
        }

        private void UpdateHpFill(float fillAmount)
        {
            Debug.Log($"Fill {fillAmount}");
            
            _tween?.Kill();
            
            hpImage
                .DOFillAmount(fillAmount, fillSpeed)
                .SetEase(Ease.Linear);
        }

        private void UpdateArmorHead(float armorAmount)
        {
            Debug.Log($"Armor {armorAmount}");
            armorHead.text = armorAmount.ToString(CultureInfo.CurrentCulture);
        }

        private void UpdateArmorBody(float armorAmount)
        {
            Debug.Log($"Armor {armorAmount}");
            armorBody.text = armorAmount.ToString(CultureInfo.CurrentCulture);
        }
    }
}