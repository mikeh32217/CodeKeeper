﻿using CodeKeeper.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;

namespace CodeKeeper.ViewModel
{
    public class ViewModelBase : DependencyObject, INotifyPropertyChanged
    {
        public Window ParentWindow;

        private int _windowWidth;
        private int _windowHeight;

        public int WindowHeight
        {
            get { return _windowHeight; }
            set { _windowHeight = value; }
        }

        public int WindowWidth
        {
            get { return _windowWidth; }
            set
            {
                _windowWidth = value;
            }
        }

        public ViewModelBase(Window win = null)
        {
            ParentWindow = win;

            if (win != null)
            {
                int width = 0;
                int height = 0;

                string w = ConfigMgr.Instance.settingProvider.GetSingleValue(win.Name, "width");
                string h = ConfigMgr.Instance.settingProvider.GetSingleValue(win.Name, "height");

                w = (w == string.Empty) ? "700" : w;
                h = (h == string.Empty) ? "550" : h;

                int.TryParse(w, out width);
                int.TryParse(h, out height);

                _windowWidth = width;
                _windowHeight = height;
            }
        }

        public bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor
                    .FromProperty(prop, typeof(FrameworkElement))
                    .Metadata.DefaultValue;
            }
        }

        #region INotifyPropertyChanged methods

        /// <summary>
        /// Multicast event for property change notifications.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string propertyName)
        {
            checkIfPropertyNameExists(propertyName);

            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        // Minor check, however in cuttting and pasting a new property we can make errors that pass this test
        // Oops, forget where I found these.. Suppose Sacha Barber introduced this test or at least blogged about it
        [Conditional("DEBUG")]
        private void checkIfPropertyNameExists(String propertyName)
        {
            Type type = this.GetType();
            Debug.Assert(
              type.GetProperty(propertyName) != null,
              propertyName + "property does not exist on object of type : " + type.FullName);
        }

        // From codeproject article Performance-and-Ideas-from-Anders-Hejlsberg-INotify (long link; google)
        // Note that we can use a text utility that checks/corrects field/propName on string basis
        public bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (!EqualityComparer<T>.Default.Equals(field, value))
            {
                field = value;

                checkIfPropertyNameExists(propertyName);

                var handler = this.PropertyChanged;
                if (handler != null)
                {
                    handler(this, new PropertyChangedEventArgs(propertyName));
                }
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    #endregion
}
