namespace AxGame.Components
{
		using AxGrid.Base;
		using AxGame.Components.Lists;
		using AxGrid.Model;

		using System.Collections;

		using UnityEngine;

		public class Slots : MonoBehaviourExtBind
		{
				public enum SlotsState { Rolling, Stopped }

				[SerializeField] private ParticleSystem m_StopEffect;
				private ListRenderer[] m_Lists;
				private IEnumerator m_RollCoroutine;

				[OnAwake]
				private void Init()
				{
						m_Lists = GetComponentsInChildren<ListRenderer>();
				}

				private IEnumerator StartRollCoroutine(ListRenderer[] listRenderers, float time, float speed, float delay)
				{
						for (int i = 0; i < m_Lists.Length; i++)
						{
								m_Lists[i].StartRoll(time - delay, speed, delay / m_Lists.Length * i);
						}

						for (int i = 0; i < m_Lists.Length; i++)
						{
								yield return new WaitUntil(() => m_Lists[i].CurrentState == ListRenderer.State.RollStartComplete);
						}
						m_RollCoroutine = null;
				}
				private IEnumerator StopRollCoroutine(ListRenderer[] listRenderers, float time, float delay)
				{
						for (int i = 0; i < m_Lists.Length; i++)
						{
								m_Lists[i].StopRoll(time - delay, delay / m_Lists.Length * i);
						}

						for (int i = 0; i < m_Lists.Length; i++)
						{
								yield return new WaitUntil(() => m_Lists[i].CurrentState == ListRenderer.State.RollStopComplete);
						}
						m_RollCoroutine = null;
						RollComplete();
				}

				private void RollComplete()
				{
						m_StopEffect.Play();
				}

				[Bind]
				private void StartRoll(float time, float speed, float delay)
				{
						if (m_RollCoroutine != null)
								StopCoroutine(m_RollCoroutine);
						m_RollCoroutine = StartRollCoroutine(m_Lists, time, speed, delay);
						StartCoroutine(m_RollCoroutine);
				}

				[Bind]
				private void StopRoll(float time, float delay)
				{
						if (m_RollCoroutine != null)
								StopCoroutine(m_RollCoroutine);
						m_RollCoroutine = StopRollCoroutine(m_Lists, time, delay);
						StartCoroutine(m_RollCoroutine);
				}
		}
}