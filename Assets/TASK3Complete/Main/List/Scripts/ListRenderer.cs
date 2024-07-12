namespace AxGame.Components.Lists
{
		using AxGrid.Base;
		using System.Collections.Generic;
		using UnityEngine;
		using AxGrid.Path;

		public class ListRenderer : MonoBehaviourExt
		{
				public enum State { RollStart, RollStartComplete, RollStop, RollStopComplete }
				[SerializeField] private ListItemRenderer m_ItemRenderer;
				[SerializeField] private int m_Rows = 1;
				[SerializeField] private Vector2 m_Size;
				[field: SerializeField] public float Speed { get; set; } = 0;
				private List<ListItemRenderer> m_ItemRenderers;
				private ListItemRenderer m_TempItemRenderer;
				private bool m_IsInited;
				private int m_Index;
				private int m_DataProviderIndex => Mathf.RoundToInt(m_ScrollIndex);

				private float m_ScrollIndex;
				private float m_Offset;

				public State CurrentState { get; protected set; }

				public float ScrollIndex
				{
						get
						{
								return m_ScrollIndex;
						}
						set
						{
								m_ScrollIndex = value;

								if (!m_IsInited) return;

								int tempIndex = 0;

								for (m_Index = 0; m_Index < m_ItemRenderers.Count; m_Index++)
								{
										tempIndex = (m_DataProviderIndex - Mathf.RoundToInt(m_ItemRenderers.Count / 2) + m_Index) % DataProvider.Count;
										tempIndex = tempIndex < 0 ? DataProvider.Count + tempIndex : tempIndex;
										m_ItemRenderers[m_Index].rectTransform.anchoredPosition = Vector3.up * ((m_DataProviderIndex) * m_Size.y) + Vector3.down * (m_Offset + (m_ScrollIndex - m_Index) * m_Size.y);
										m_ItemRenderers[m_Index].SetData(DataProvider[Mathf.Clamp(tempIndex, 0, DataProvider.Count)]);
								}
						}
				}

				[SerializeField] private List<ListItemData> m_DataProvider;
				public List<ListItemData> DataProvider
				{
						get
						{
								return m_DataProvider;
						}
						set
						{
								m_DataProvider = value;
						}
				}

				[OnAwake]
				private void Init()
				{
						m_IsInited = true;
						m_ItemRenderers = new List<ListItemRenderer>(new ListItemRenderer[m_Rows + 2]);
						for (m_Index = 0; m_Index < m_ItemRenderers.Count; m_Index++)
						{
								m_ItemRenderers[m_Index] = Instantiate(m_ItemRenderer, transform);
								m_ItemRenderers[m_Index].rectTransform.sizeDelta = m_Size;
						}
						m_Offset = (Mathf.Round(m_ItemRenderers.Count / 2) * m_Size.y);
						ScrollIndex = 0;
				}

				[OnUpdate]
				private void InfinityScroll()
				{
						if (Speed > 1f)
						{
								ScrollIndex += Speed * Time.deltaTime;
						}
						else
						{
								ScrollIndex = Mathf.MoveTowards(ScrollIndex, Mathf.CeilToInt(ScrollIndex), Time.deltaTime);
								Speed = 0;
						}
				}

				public void StartRoll(float time, float speed, float delay)
				{
						CurrentState = State.RollStart;

						Path = CPath.Create().Wait(delay).EasingLinear(time, 0, speed, (value) =>
						{
								Speed = value;
						}).Action(() =>
						{
								CurrentState = State.RollStartComplete;
						});
				}

				public void StopRoll(float time, float delay)
				{
						CurrentState = State.RollStop;
						Path = CPath.Create().Wait(delay).EasingLinear(time, Speed, 0, (value) =>
						{
								Speed = value;

						}).Action(() =>
						{
								CurrentState = State.RollStopComplete;
						});
				}
		}
}