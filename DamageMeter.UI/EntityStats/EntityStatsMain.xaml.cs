﻿using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using Data;

namespace DamageMeter.UI.EntityStats
{
    /// <summary>
    ///     Logique d'interaction pour EntityStats.xaml
    /// </summary>
    public partial class EntityStatsMain
    {
        private readonly List<EnduranceDebuff> _enduranceDebuffsList = new List<EnduranceDebuff>();

        private readonly EnduranceDebuffHeader _header;
        private readonly MainWindow _parent;

        public EntityStatsMain(MainWindow parent)
        {
            InitializeComponent();
            _parent = parent;
            _header = new EnduranceDebuffHeader();

        }

        public void SetClickThrou()
        {
            var hwnd = new WindowInteropHelper(this).Handle;
            WindowsServices.SetWindowExTransparent(hwnd);
        }

        public void UnsetClickThrou()
        {
            var hwnd = new WindowInteropHelper(this).Handle;
            WindowsServices.SetWindowExVisible(hwnd);
        }

        public void Update(Dictionary<Entity, EntityInfo> stats)
        {
            var entity = NetworkController.Instance.Encounter;
            EnduranceAbnormality.Items.Clear();
            if (entity == null)
            {
                return;
            }
            var statsAbnormalities = stats[entity];
            if (statsAbnormalities.Interval == 0)
            {
                return;
            }

            EnduranceAbnormality.Items.Add(_header);

            var count = 0;
            foreach (var abnormality in statsAbnormalities.AbnormalityTime)
            {
                EnduranceDebuff abnormalityUi;
                if (_enduranceDebuffsList.Count > count)
                {
                    abnormalityUi = _enduranceDebuffsList[count];
                }
                else
                {
                    abnormalityUi = new EnduranceDebuff();
                    _enduranceDebuffsList.Add(abnormalityUi);
                }

                abnormalityUi.Update(abnormality.Key, abnormality.Value,
                    statsAbnormalities.FirstHit/TimeSpan.TicksPerSecond,
                    statsAbnormalities.LastHit/TimeSpan.TicksPerSecond);
                EnduranceAbnormality.Items.Add(abnormalityUi);
                count++;
            }
        }
     
        private void EntityStats_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DragMove();
            }
            catch
            {
                Console.WriteLine(@"Exception move");
            }
        }

        private void CloseMeter_OnClick(object sender, RoutedEventArgs e)
        {
            _parent.CloseEntityStats();
        }
    }
}