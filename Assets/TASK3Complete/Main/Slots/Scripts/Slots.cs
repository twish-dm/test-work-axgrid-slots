using AxGrid;
using AxGrid.Base;
using AxGrid.FSM;
using AxGrid.Model;
using AxGrid.Path;
using AxGrid.Utils;

using System.Linq;
using UnityEngine;

public class Slots : MonoBehaviourExtBind
{
		[SerializeField] private ParticleSystem m_StopEffect;
		private ListRenderer[] m_Lists;
		[OnAwake]
		private void Init()
		{
				m_Lists = GetComponentsInChildren<ListRenderer>();
		}

		[Bind]
		private void StartRoll()
		{
				Path = new CPath().EasingLinear(3f, 0, 1f, (value) =>
				{
						for (int i = 0; i < m_Lists.Length; i++)
						{
								m_Lists[i].Speed = Mathf.Pow(value, i + 1) * 10;
						}
				});
		}

		[Bind]
		private void StopRoll()
		{
				Path = CPath.Create().EasingLinear(3, m_Lists.Length, 0, (value) =>
				{
						m_Lists[Mathf.FloorToInt(value% m_Lists.Length)].Speed = value % 1 * 10;
						
				}).Action(()=>
				{
						m_StopEffect.Play();
				});
		}
}
