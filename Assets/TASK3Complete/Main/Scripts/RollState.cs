using AxGrid.FSM;

[State("Roll")]
internal class RollState : FSMState
{
		[Enter]
		protected void Enter()
		{
				Model.EventManager.AddAction("OnRollStopClick", GoToStopState);
				Model.Set("BtnRollStartEnable", false);
				Model.EventManager.Invoke("StartRoll");
		}


		public void GoToStopState()
		{
				Parent.Change("Stop");

		}

		[One(3)]
		private void ApplyStopEnable()
		{
				Model.Set("BtnRollStopEnable", true);
		}

}