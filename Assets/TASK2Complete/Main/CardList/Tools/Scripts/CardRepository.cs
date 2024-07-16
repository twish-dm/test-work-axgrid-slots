namespace Task3.Cards.Tools
{
		using System.Collections;
		using System.Collections.Generic;

		using UnityEngine;

		static public class CardRepository
		{
				static private Dictionary<Card, CardRenderer> m_CardsMap;
				static CardRepository()
				{
						m_CardsMap = new Dictionary<Card, CardRenderer>();
				}
				static public CardRenderer GetRenderer(this Card card, CardRenderer cardPrefab, Transform parent)
				{
						if (m_CardsMap.ContainsKey(card) && m_CardsMap[card])
						{
								m_CardsMap[card].transform.parent = parent;
								return m_CardsMap[card];
						}
						else if (m_CardsMap.ContainsKey(card) && !m_CardsMap[card])
						{
								m_CardsMap.Remove(card);
						}
						m_CardsMap.Add(card, GameObject.Instantiate(cardPrefab, parent));
						m_CardsMap[card].SetData(card);
						return m_CardsMap[card];
				}
				static public void Destroy(this Card card)
				{
						if (!m_CardsMap.ContainsKey(card) && m_CardsMap[card]) throw new System.Exception("Card not found");
						if (m_CardsMap[card])
								GameObject.Destroy(m_CardsMap[card].gameObject);
						m_CardsMap.Remove(card);
				}
		}
}