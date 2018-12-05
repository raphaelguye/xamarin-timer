﻿// This file has been autogenerated from a class added in the UI designer.

using System;
using System.Collections.Generic;
using Foundation;
using Mvvmicro;
using ServiceContracts.Interfaces;
using ServiceContracts.Model;
using UIKit;
using VIewModels.Interfaces;

namespace Timer
{
    internal class TimersTableViewSource : UITableViewSource
    {
        List<float> timers;
        private TimersTableViewController timersTableViewController;
        const string TimerCellIdentifier = "TimerCell";

        public TimersTableViewSource(TimersTableViewController timersTableViewController)
        {
            this.timersTableViewController = timersTableViewController;

            timers = new List<float>() { 0.3f, 0.25f, 2.0f };
        }

        ITimersViewModel ViewModel => AppDelegate.Locator.TimersViewModel;
        IContextService ContextService => Container.Default.Get<IContextService>();

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return timers.Count;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            UITableViewCell cell = tableView.DequeueReusableCell(TimerCellIdentifier);

            string cellTitle = $"Phase {indexPath.Row}";
            float time = timers[indexPath.Row];

            if (cell == null)
            {
                cell = new UITableViewCell(UITableViewCellStyle.Subtitle, TimerCellIdentifier);
            }

            cell.TextLabel.Text = cellTitle;
            cell.DetailTextLabel.Text = $"{time} mn";

            return cell;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            //Is it really there to set the context ? Not in the VM ?
            ContextService.TimerSelected = new MyTimer() { Name = $"Row {indexPath.Row}", Duration = new TimeSpan(0,0,DateTime.Now.Second) };

            ViewModel.SelectTimerCommand.Execute(null);
        }
    }
}