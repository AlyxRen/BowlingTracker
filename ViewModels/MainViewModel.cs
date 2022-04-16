using BowlingTracker.Models;
using BowlingTracker.Commands;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Threading.Tasks;

using System.Diagnostics;

namespace BowlingTracker.ViewModels
{
    internal class MainViewModel : ViewModelBase
    {
        private ObservableCollection<BowlerViewModel> _bowlers;
        private ICommand? _ResetCommand;

        public IEnumerable<BowlerViewModel> Bowlers => _bowlers;

        public MainViewModel()
        {
            _bowlers = new ObservableCollection<BowlerViewModel>
            {
                new BowlerViewModel(new Bowler())
            };
            _reset();
        }

        private void _reset()
        {
            foreach (var bowler in Bowlers)
            {
                bowler.Reset();
            }
            // Creating a FocusNavigationDirection object and setting it to a
            // local field that contains the direction selected.
            FocusNavigationDirection focusDirection = FocusNavigationDirection.First;

            // MoveFocus takes a TraveralReqest as its argument.
            TraversalRequest request = new TraversalRequest(focusDirection);

            // Gets the element with keyboard focus.
            UIElement elementWithFocus = Keyboard.FocusedElement as UIElement;

            // Change keyboard focus.
            if (elementWithFocus != null)
            {
                elementWithFocus.MoveFocus(request);
            }
            // Creating a FocusNavigationDirection object and setting it to a
            // local field that contains the direction selected.
            focusDirection = FocusNavigationDirection.Next;

            // MoveFocus takes a TraveralReqest as its argument.
            request = new TraversalRequest(focusDirection);

            // Gets the element with keyboard focus.
            elementWithFocus = Keyboard.FocusedElement as UIElement;

            // Change keyboard focus.
            if (elementWithFocus != null)
            {
                elementWithFocus.MoveFocus(request);
            }
        }

        public ICommand ResetCommand
        {
            get
            {
                if (_ResetCommand == null)
                {
                    _ResetCommand = new RelayCommand(param => this._reset(), null);
                }
                return _ResetCommand;
            }
        }
    }
}
