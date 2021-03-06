﻿using System;
using System.Windows;
using System.Windows.Input;
using Data;

namespace DamageMeter.UI.EntityStats
{
    /// <summary>
    ///     Logique d'interaction pour EnduranceDebuff.xaml
    /// </summary>
    public partial class EnduranceDebuff
    {
        public EnduranceDebuff()
        {
            InitializeComponent();
        }

        public void Update(HotDot hotdot, AbnormalityDuration abnormalityDuration, long firstHit, long lastHit)
        {
            LabelClass.Content = abnormalityDuration.InitialPlayerClass;
            var intervalEntity = lastHit - firstHit;
            //   Console.WriteLine("f:"+firstHit+";l:"+lastHit+";i:"+intervalEntity);
            var second = abnormalityDuration.Duration(firstHit, lastHit);
            // Console.WriteLine(second);
            var interval = TimeSpan.FromSeconds(second);
            LabelAbnormalityDuration.Content = interval.ToString(@"mm\:ss");

            if (intervalEntity == 0)
            {
                LabelAbnormalityDurationPercentage.Content = "0%";
            }
            else
            {
                LabelAbnormalityDurationPercentage.Content = abnormalityDuration.Duration(firstHit, lastHit)*100/
                                                             intervalEntity + "%";
            }
            interval = TimeSpan.FromSeconds(intervalEntity);
            LabelInterval.Content = interval.ToString(@"mm\:ss");

            LabelName.Content = hotdot.Name;
            LabelId.Content = hotdot.Id;
        }

        private void UIElement_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var w = Window.GetWindow(this);
                w?.DragMove();
            }
            catch
            {
                Console.WriteLine(@"Exception move");
            }
        }
    }
}