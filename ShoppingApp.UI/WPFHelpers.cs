using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;

namespace ShoppingApp.UI
{
	public class PoppingQueue<T>
	{
		private class QueueItem
		{
			public QueueItem(T item, int index)
			{
				Item = item;
				Index = index;
			}

			public T Item;
			public int Index;
		}

		private List<QueueItem> _queueItems { get; } = new List<QueueItem>();
		public int Count => _queueItems.Count;
		public T Top => _queueItems[Count - 1].Item;

		public Action Push(T item)
		{
			var queueItem = new QueueItem
			(
				item,
				_queueItems.Count
			);

			_queueItems.Add
			(
				queueItem
			);

			return () =>
			{
				int popIndex;

				popIndex = _queueItems.Count > queueItem.Index &&
							_queueItems[queueItem.Index].Item.Equals(item) ?
								queueItem.Index
								: _queueItems.FindIndex(x => x.Item.Equals(item));

				if (popIndex >= 0)
				{
					Pop(popIndex);
				}
			};
		}

		public T Pop() => Pop(Count - 1);

		public T Pop(int index)
		{
			var removing = _queueItems[index];

			_queueItems.RemoveAt(index);

			for (var i = index; i < _queueItems.Count; i++)
			{
				_queueItems[i].Index--;
			}

			return removing.Item;
		}
	}

	public class WindowContext
	{
		private class SetsWindowState : IDisposable
		{
			private readonly WindowContext _wCtx;

			public SetsWindowState(WindowContext wCtx)
			{
				_wCtx = wCtx;
				_wCtx.UpdateWindow = false;
			}

			public void Dispose() => _wCtx.UpdateWindow = true;
		}

		public static WindowContext State { get; } = new WindowContext(App.Current.MainWindow);

		private bool _updateWindow = true;

		public bool UpdateWindow
		{
			get => _updateWindow;
			set
			{
				_updateWindow = value;
				UpdateWindowState();
			}
		}

		public IDisposable DisableWindowUpdates() => new SetsWindowState(this);

		public WindowContext(Window main)
		{
			_poppingQueue = new PoppingQueue<Window>();
			_poppingQueue.Push(main);
		}

		private readonly PoppingQueue<Window> _poppingQueue;

		private void UpdateWindowState()
		{
			if (UpdateWindow &&
				_poppingQueue.Count > 0)
			{
				var top = _poppingQueue.Top;

				if (top != App.Current.MainWindow)
				{
					App.Current.MainWindow?.Hide();
					App.Current.MainWindow = top;
				}

				top.Show();
			}
		}

		public void Push(Window window, Action onDeath = null)
		{
			var dequeue = _poppingQueue.Push(window);

			window.Closed += (sender, e) =>
			{
				dequeue();
				UpdateWindowState();

				onDeath?.Invoke();
			};

			UpdateWindowState();
		}

		public void SwapTop(Window window)
		{
			using (var _ = DisableWindowUpdates())
			{
				var popped = _poppingQueue.Pop();
				Push(window);

				popped.Close();
			}
		}
	}

	public static class WPFHelpers
	{
		public static void OpenChildWindow(this WindowContext wCtx, Window newWindow) => wCtx.Push(newWindow);

		public static void ChangeWindow(this WindowContext wCtx, Window newWindow) => wCtx.SwapTop(newWindow);

		public static Task WaitUntilChildWindowCloses(this WindowContext wCtx, Window newWindow)
		{
			var tcs = new TaskCompletionSource<bool>();

			wCtx.Push
			(
				newWindow,
				() =>
				{
					tcs.SetResult(true);
				}
			);

			return tcs.Task;
		}
	}
}