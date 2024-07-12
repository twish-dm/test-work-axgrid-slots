namespace AxGame.States
{
		using AxGrid.FSM;

		[State("Stop")]
		internal class StopState : FSMState
		{
				[Enter]
				protected void Enter()
				{
						Model.Set("BtnRollStopEnable", false);
						Model.EventManager.Invoke("StopRoll", 3f, 1.5f);

				}
				[Exit]
				private void Exit()
				{
						Model.EventManager.RemoveAction(GoToIdleState);
				}
				[One(3)]
				protected void GoToIdleState()
				{
						Parent.Change("Idle");
				}
		}
}