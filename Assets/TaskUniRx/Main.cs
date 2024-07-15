namespace TaskUniRx
{
		using AxGrid.Base;
		using UniRx;
		using System.Collections;
		using System.Collections.Generic;

		using UnityEngine;
		using AxGrid.Model;
		using UnityEngine.SceneManagement;
		using SmartFormat;

		public class Main : MonoBehaviourExt
		{
				[OnAwake]
				private void Init()
				{
						Model.EventManager.AddAction("OnLoadSceneClick", LoadScene);
						Model.EventManager.AddAction("OnUnloadSceneClick", UnloadScene);
						Model.EventManager.AddAction("OnHeavyClick", Heavy);
				}
				[OnDestroy]
				private void Destory()
				{
						Model.EventManager.RemoveAction("OnLoadSceneClick", LoadScene);
						Model.EventManager.RemoveAction("OnUnloadSceneClick", UnloadScene);
						Model.EventManager.RemoveAction("OnHeavyClick", Heavy);
				}

				private void LoadScene()
				{
						SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive)
						.AsAsyncOperationObservable()
						.Do(x =>
						{
								Debug.Log(Smart.Format("Progress {0}%", x.progress * 100f));
						}).Subscribe(_ =>
						{
								Debug.Log("Scene loaded");
						}).AddTo(this);
				}

				private void UnloadScene()
				{
						SceneManager.UnloadSceneAsync(1)
						.AsAsyncOperationObservable()
						.Do(x =>
						{
								Debug.Log(Smart.Format("Progress {0}%", x.progress * 100f));
						}).Subscribe(_ =>
						{
								Debug.Log("Scene unloaded");
						}).AddTo(this);
				}


				private void Heavy()
				{
						Observable.Start(() =>
						{
								int result = 0;
								int[] array = new int[100000000];

								for (int i = 0; i < array.Length; i++)
								{
										result += i;
										result = result / 100;
										array[i] = result;
								}
								
								return result;
						}).ObserveOnMainThread()
					      .Subscribe(result =>
					      {
							      Debug.Log($"Heavy result {result}.");
					      }).AddTo(this);
				}
		}
}