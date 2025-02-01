using UnityEngine;
using UnityEngine.UI;
using Controllers;
using DG.Tweening;
using Zenject;

namespace Views
{
    public class EnemyView : MonoBehaviour, IInitializable
    {
        [SerializeField] private Image hpImage;
        [SerializeField] private float fillSpeed;
        
        private IEnemyUIController _enemyUIController;
        private Tween _tween;

        [Inject]
        public void Construct(IEnemyUIController enemyUIController)
        {
            _enemyUIController = enemyUIController;
            Debug.Log("Enemy View Construct");
        }
        
        public void Initialize()
        {
            _enemyUIController.TakenDamage += UpdateHpFill;
            _enemyUIController.Heal += UpdateHpFill;
            
            Debug.Log(" Enemy View initialized");
        }

        private void UpdateHpFill(float fillAmount)
        {
            Debug.Log($" Enemy hp fill: {fillAmount}");
            
            _tween?.Kill();
            
            hpImage
                .DOFillAmount(fillAmount, fillSpeed)
                .SetEase(Ease.Linear);
        }

        private void OnDestroy()
        {
            _enemyUIController.TakenDamage -= UpdateHpFill;
            _enemyUIController.Heal -= UpdateHpFill;
        }
    }
}