namespace AxGame.States
{
		using AxGrid.FSM;
		using AxGrid.Model;

		[State("Idle")]
		internal class IdleState : FSMState
		{
				[Enter]
				protected void Enter()
				{
						Model.Set("BtnRollStartEnable", true);
						Model.Set("BtnRollStopEnable", false);
				}

				[Bind]
				public void OnBtn(string name)
				{
						switch (name)
						{
								case "RollStart":
										Parent.Change("Roll");
										break;
						}
				}
		}
}