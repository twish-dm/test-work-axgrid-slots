namespace Task3.Cards
{
		using AxGrid.Base;
		using System.Linq;
		using System.Collections.Generic;

		using UnityEngine;
		using Task3.Cards.Tools;
		using AxGrid;

		public class CardList : MonoBehaviourExt
		{
				/// <summary>
				/// Имя поля списка в модели (если пустое берется из имени объекта)
				/// </summary>
				[Tooltip("Имя поля списка в модели (если пустое берется из имени объекта)")]
				[SerializeField] private string m_FieldName = "";

				[SerializeField] private CardRenderer m_CardPrefab;
				protected RectTransform rectTransform;
				[SerializeField] private Bounds m_Bounds, m_CardBounds;

				
				private List<Card> m_LocalCards;

				[OnAwake]
				private void Init()
				{
						m_FieldName = string.IsNullOrEmpty(m_FieldName) ? name : m_FieldName;
						m_LocalCards = new List<Card>();
						rectTransform = (RectTransform)transform;
						m_Bounds = RectTransformUtility.CalculateRelativeRectTransformBounds(transform.parent, transform);
						m_CardBounds = RectTransformUtility.CalculateRelativeRectTransformBounds(m_CardPrefab.transform);
				}

				

				[OnStart]
				private void Run()
				{
						SyncList();
						Model.EventManager.AddAction($"On{m_FieldName}Changed", SyncList);
				}
				[OnDestroy]
				private void Destroy()
				{
						Model.EventManager.RemoveAction($"On{m_FieldName}Changed", SyncList);
						m_LocalCards.ForEach(card =>
						{
								card.Destroy();
						});
				}

				private void SyncList()
				{
						List<Card> cards = Model.GetList<Card>(m_FieldName);
						List<Card> missingCards = null;
						if (!Model.ContainsKey(m_FieldName)) return;
						Card[] array = new Card[cards.Count];

						if (m_LocalCards.Count > cards.Count)
								missingCards = m_LocalCards.Where(x => !cards.Any(y => y == x)).ToList();
						else if (cards.Count > m_LocalCards.Count)
								missingCards = cards.Where(x => !m_LocalCards.Any(y => y == x)).ToList();

						cards.CopyTo(array);
						m_LocalCards = new List<Card>(array);

						if (missingCards != null)
								UpdatePositions();
				}
				private void UpdatePositions()
				{
						for (int i = 0; i < m_LocalCards.Count; i++)
						{
								CardRenderer card = m_LocalCards[i].GetRenderer(m_CardPrefab, transform);
								card.transform.SetAsLastSibling();
								card.MoveTo(GetCardPosition(i, m_LocalCards.Count));
						}
				}
				private Vector2 GetCardPosition(int index, int count)
				{
						float boundsSizeX = Mathf.Clamp(m_CardBounds.size.x*count, 0, m_Bounds.size.x) - m_CardBounds.size.x;
						float positionX = ((float)index) / ((float)count-1)* (boundsSizeX);
						positionX = float.IsNaN(positionX) ? 0 : positionX;
						Vector2 newPosition = new Vector2(-boundsSizeX / 2+ (positionX), 0);
						return newPosition;
				}

				public void OnPointerClick(Card card)
				{
						Settings.Invoke("OnCardSelectd", m_FieldName, card);
				}
		}
}