using AxGrid.FSM;

[State("Stop")]
internal class StopState : FSMState
{
		[Enter]
		protected void Enter()
		{
				Model.Set("BtnRollStopEnable", false);
				Model.EventManager.Invoke("StopRoll");
		}

		[One(3)]
		protected void GoToIdleState()
		{
				Parent.Change("Idle");
		}
}