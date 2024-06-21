using AxGrid.FSM;
using AxGrid.Model;

using UnityEngine;

[State("Idle")]
internal class IdleState : FSMState
{
		[Enter]
		protected void Enter()
		{
				Model.EventManager.AddAction("OnRollStartClick", GoToRollState);
				Model.Set("BtnRollStartEnable", true);
				Model.Set("BtnRollStopEnable", false);
				
		}

		[Exit]
		protected void Exit()
		{
				
		}

		public void GoToRollState()
		{
				Debug.Log("Roll");
				Parent.Change("Roll");

		}
}