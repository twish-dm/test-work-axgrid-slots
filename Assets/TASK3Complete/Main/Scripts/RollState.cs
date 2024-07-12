namespace AxGame.States
{
		using AxGrid.FSM;
		using AxGrid.Model;

		[State("Roll")]
		internal class RollState : FSMState
		{

				[Enter]
				protected void Enter()
				{
						Model.Set("BtnRollStartEnable", false);
						Model.EventManager.Invoke("StartRoll", 3f, 10f, 1.5f);
				}


				[Bind]
				public void OnBtn(string name)
				{
						switch (name)
						{
								case "RollStop":
										Parent.Change("Stop");
										break;
						}
				}

				[One(3)]
				private void ApplyStopEnable()
				{
						Model.Set("BtnRollStopEnable", true);
				}

		}
}