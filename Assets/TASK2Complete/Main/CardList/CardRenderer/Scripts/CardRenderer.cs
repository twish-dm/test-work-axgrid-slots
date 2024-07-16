namespace Task3.Cards
{
		using AxGrid.Base;
		using AxGrid.Path;
		using TMPro;

		using UnityEngine;
		using UnityEngine.UI;

		[RequireComponent(typeof(Button))]
		public class CardRenderer : MonoBehaviourExt
		{
				[SerializeField] private TextMeshProUGUI m_LabelField;
				[SerializeField] private Image m_Icon;

				protected RectTransform rectTransform;
				private Card m_Card;
				[OnAwake]
				private void Init()
				{
						rectTransform = (RectTransform)transform;
				}
				
				public void ClickHandler()
				{
						GetComponentInParent<CardList>()?.OnPointerClick(m_Card);
						
				}

				public void SetData(Card card)
				{
						m_LabelField.text = card.cardName.ToString();
						m_Icon.sprite = Resources.Load<Sprite>($"Sprites/{card.spriteName}");
						m_Card = card;
				}

				public void MoveTo(Vector2 target)
				{
						Path = CPath.Create()
						.EasingLinear(1f, 0, 1, (x) => rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, target, x));
				}

				
		}
}
