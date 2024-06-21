using AxGrid;
using AxGrid.Base;
using AxGrid.FSM;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Main : MonoBehaviourExtBind
{
		[OnAwake]
		private void Init()
		{
				Settings.Fsm = new FSM();
				Settings.Fsm.Add(new IdleState());
				Settings.Fsm.Add(new RollState());
				Settings.Fsm.Add(new StopState());
				Settings.Fsm.Start("Idle");
		}

		[OnUpdate]
		private void UpdateThis()
		{
				Settings.Fsm.Update(Time.deltaTime);
		}

}
