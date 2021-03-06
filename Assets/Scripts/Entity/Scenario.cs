﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Baron.Entity
{
	public class Scenario
	{
		[JsonProperty(PropertyName = "branches")]
		private List<TrackBranch> _branches;

		[JsonIgnore]
		public List<TrackBranch> Branches
		{
			get { return _branches; }
			set { _branches = value; }
		}

		[JsonProperty(PropertyName = "cid")]
		private string _cid;

		[JsonIgnore]
		public string Cid
		{
			get { return _cid; }
			set { _cid = value; }
		}

		[JsonIgnore]
		private float _duration;
		[JsonIgnore]
		public int Duration
		{
			get { return (int)_duration; }
			set { _duration = value; }
		}

		[JsonProperty(PropertyName = "isCompleted")]
		private bool _isCompleted;

		[JsonIgnore]
		public bool IsCompleted
		{
			get { return _isCompleted; }
			set { _isCompleted = value; }
		}

		[JsonProperty(PropertyName = "progress")]
		private int _progress;
		[JsonIgnore]
		public int Progress
		{
			get { return _progress; }
			set { _progress = value; }
		}

		[JsonProperty(PropertyName = "currentBranch")]
		private TrackBranch _currentTrackBranch;

		[JsonIgnore]
		public TrackBranch CurrentTrackBranch
		{
			get { return _currentTrackBranch; }
			set { _currentTrackBranch = value; }
		}

		[JsonIgnore]
		private bool _isInit;


		public Scenario()
		{
			_branches = new List<TrackBranch>();
		}

		public bool IsValid()
		{
			return _isInit && Branches.Count > 0
					&& _progress >= 0 && _duration >= 0
					&& _currentTrackBranch != null;
		}

		public void Init()
		{
			int offset = 0;
			int size = Branches.Count;
			_duration = 0;

			for (int i = 0; i < size; i++)
			{
				TrackBranch current = _branches[i];

				TrackBranch next = null;
				TrackBranch prev = null;
				if (size > 1)
				{
					if (i == 0)
					{
						next = _branches[i + 1];
					}
					else if (i == size - 1)
					{
						prev = _branches[i - 1];
					}
					else
					{
						next = _branches[i + 1];
						prev = _branches[i - 1];
					}
				}

				current.Previous = prev;
				current.Next = next;

				current.StartsAt = offset;
				current.FinishesAt = offset += current.Duration;

				_duration += current.Duration;
			}

			_isInit = true;

			Update();
		}

		public TrackBranch First()
		{
			if (_branches.Count == 0) return null;

			return _branches[0];
		}

		public TrackBranch Last()
		{
			if (_branches.Count == 0) return null;

			return _branches[_branches.Count - 1];
		}

		public bool Update(int progress, int duration)
		{

			if (progress > duration)
			{
				progress = duration;
			}

			_progress = progress;
			_duration = duration;

			_isCompleted = progress >= duration;

			return Update();
		}

		public void Unlock()
		{
			foreach (TrackBranch trackBranch in _branches)
			{
				trackBranch.IsLocked = false;
			}
		}

		public bool Update()
		{
			string prevBranchId = _currentTrackBranch != null ? _currentTrackBranch.Id : null;

			_currentTrackBranch = null;

			if (_progress == 0)
			{
				_currentTrackBranch = First();
			}
			else if (_progress == _duration)
			{
				_currentTrackBranch = Last();
			}

			foreach (TrackBranch trackBranch in _branches)
			{

				trackBranch.Progress = _progress;

				if (trackBranch.StartsAt <= _progress && _progress < trackBranch.FinishesAt)
				{
					trackBranch.IsCompleted = false;
					trackBranch.IsLocked = true;

					_currentTrackBranch = trackBranch;

				}
				else if (_progress < trackBranch.StartsAt)
				{
					trackBranch.IsCompleted = false;
					trackBranch.IsLocked = false;
				}
				else if (trackBranch.FinishesAt <= _progress)
				{
					trackBranch.IsCompleted = true;
					trackBranch.IsLocked = false;
				}
			}

			return _currentTrackBranch != null && !_currentTrackBranch.Id.Equals(prevBranchId);
		}
	}
}
