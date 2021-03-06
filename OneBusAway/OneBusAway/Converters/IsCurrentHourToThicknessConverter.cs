﻿/* Copyright 2014 Michael Braude and individual contributors.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Linq;

#if WINDOWS_PHONE
using System.Windows.Data;
using System.Windows;
using System.Globalization;
#else
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
#endif

namespace OneBusAway.Converters
{
    public class IsCurrentHourToThicknessConverter : IValueConverter
    {
#if WINDOWS_PHONE
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
#else
        public object Convert(object value, Type targetType, object parameter, string language)
#endif
        {
            DateTime[] valueTime = new DateTime[] { };
            if (value is string)
            {
                string dateString = value as string;
                if (!string.IsNullOrEmpty(dateString))
                {
                    valueTime = (from s in dateString.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                 select DateTime.Parse(s)).ToArray();
                }
            }
            else if (value is DateTime[])
            {
                valueTime = (DateTime[])value;
            }

            if (valueTime.Length == 0)
            {
                return new Thickness(0);
            }

            double thickness = 0.0;
            if (parameter != null)
            {
                double.TryParse(parameter as string, out thickness);
            }

            return (valueTime[0].Hour == DateTime.Now.Hour)
                ? new Thickness(thickness)
                : new Thickness(0);
        }

#if WINDOWS_PHONE
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
#else
        public object ConvertBack(object value, Type targetType, object parameter, string language)
#endif
        {
            throw new NotImplementedException();
        }
    }
}
